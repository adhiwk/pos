Imports DevExpress.XtraLayout

Public Class frmPembayaran
    Friend nTotal As Decimal
    Friend cJenisPembayaran As String = ""

    Private Sub frmPembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cboBankName.Properties.Items.Clear()
        'cboCCCardType.Properties.Items.Clear()

        'With cboBankName
        '    .Properties.Items.Add("BCA")
        '    .Properties.Items.Add("BNI")
        '    .Properties.Items.Add("BNI SYARIAH")
        '    .Properties.Items.Add("BRI")
        '    .Properties.Items.Add("BTN")
        '    .Properties.Items.Add("BUKOPIN")
        '    .Properties.Items.Add("CIMB")
        '    .Properties.Items.Add("DANAMON")
        '    .Properties.Items.Add("MEGA")
        '    .Properties.Items.Add("MANDIRI")
        '    .Properties.Items.Add("MUAMALAT INDONESIA")
        '    .Properties.Items.Add("SYARIAH MANDIRI")
        'End With

        With cboJenisKartu
            .Properties.Items.Add("Mandiri Debet")
            .Properties.Items.Add("Mandiri Credit")
            .Properties.Items.Add("BCA Debet")
            .Properties.Items.Add("BCA Credit")
        End With

        'With cboCCCardType
        '    .Properties.Items.Add("AMERICAN EXPRESS")
        '    .Properties.Items.Add("DISCOVER CARD")
        '    .Properties.Items.Add("MASTERCARD")
        '    .Properties.Items.Add("VISA")
        'End With

        ClearScreen()
        cJenisPembayaran = "Tunai"
        rgJenisPembayaran.Focus()

        LayoutControlGroup2.HideToCustomization()
        LayoutControlGroup3.HideToCustomization()
        LayoutControlGroup4.HideToCustomization()
        LayoutControlGroup2.RestoreFromCustomization()

        txtTotal.Text = Val(nTotal)

    End Sub

    Private Sub ClearScreen()
        txtBayar.Text = ""
        txtKembali.Text = ""

        'cboBankName.Text = ""
        cboJenisKartu.Text = ""
        txtApprCode.Text = ""

        'cboCCCardType.Text = ""
        txtCardNumber.Text = ""
        txtExpDate.Text = ""
    End Sub

    Private Sub txtKembali_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKembali.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.Dispose()
        End If
    End Sub

    'Private Sub txtTIDDebitCard_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Asc(e.KeyChar) = 13 Then
    '        Me.Dispose()
    '    End If
    'End Sub

    Private Sub txtApprCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApprCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtApprCode.Text.Trim = "" Then
                MsgBox("Anda harus memasukkan approval code terlebih dahulu", MsgBoxStyle.Critical, "Error")
                Exit Sub

            Else
                txtBayar.Text = Val(nTotal)
                txtKembali.Text = 0
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub rgJenisPembayaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rgJenisPembayaran.SelectedIndexChanged
        If rgJenisPembayaran.SelectedIndex = 0 Then
            LayoutControlGroup2.HideToCustomization()
            LayoutControlGroup3.HideToCustomization()
            LayoutControlGroup4.HideToCustomization()
            LayoutControlGroup2.RestoreFromCustomization()
            'TabbedControlGroup1.SelectedTabPage = LayoutControlGroup2
            cJenisPembayaran = "Tunai"
            txtBayar.Focus()
        ElseIf rgJenisPembayaran.SelectedIndex = 1 Then
            LayoutControlGroup2.HideToCustomization()
            LayoutControlGroup3.HideToCustomization()
            LayoutControlGroup4.HideToCustomization()
            LayoutControlGroup3.RestoreFromCustomization()
            'TabbedControlGroup1.SelectedTabPage = LayoutControlGroup3
            cJenisPembayaran = "Voucher"
            'cboBankName.Focus()
        ElseIf rgJenisPembayaran.SelectedIndex = 2 Then
            LayoutControlGroup2.HideToCustomization()
            LayoutControlGroup3.HideToCustomization()
            LayoutControlGroup4.HideToCustomization()
            LayoutControlGroup4.RestoreFromCustomization()
            'TabbedControlGroup1.SelectedTabPage = LayoutControlGroup4
            cJenisPembayaran = "Kartu"
            cboJenisKartu.Focus()
        End If
    End Sub

    'Private Sub txtBayar_EditValueChanged(sender As Object, e As EventArgs) Handles txtBayar.EditValueChanged
    '    txtKembali.Text = Val(txtBayar.Text.Trim) - Val(nTotal)
    'End Sub

    Private Sub txtBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBayar.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Val(txtBayar.Text.Trim) < Val(nTotal) Then
                MsgBox("Kurang bayar", MsgBoxStyle.Critical, "Error")
                txtBayar.Focus()
                Exit Sub
            End If
            txtKembali.Text = Val(clsPublic.ConvertNumeric(txtBayar.Text.Trim)) - Val(nTotal)
        ElseIf asc(e.KeyChar) = 32 Then
            txtBayar.Text = nTotal.ToString.Trim
        End If
    End Sub


    Private Sub TabbedControlGroup1_SelectedPageChanging(sender As Object, e As LayoutTabPageChangingEventArgs) Handles TabbedControlGroup1.SelectedPageChanging
        If TabbedControlGroup1.SelectedTabPageIndex = 2 Then
            rgJenisPembayaran.SelectedIndex = 0
            cJenisPembayaran = "Tunai"
            txtBayar.Focus()
        ElseIf TabbedControlGroup1.SelectedTabPageIndex = 0 Then
            rgJenisPembayaran.SelectedIndex = 1
            cJenisPembayaran = "Voucher"
            ' cboBankName.Focus()
        ElseIf TabbedControlGroup1.SelectedTabPageIndex = 1 Then
            rgJenisPembayaran.SelectedIndex = 2
            cJenisPembayaran = "Kartu"
            cboJenisKartu.Focus()
        End If
    End Sub

    Private Sub frmPembayaran_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyValue.ToString.Trim = "27" Then
                Me.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPembayaran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtBayar.Focus()
    End Sub

End Class