Public Class frmMenuUtama
    Friend cUserId As String
    Friend cNamaKasir As String
    Friend cBulan As String
    Friend cTahun As String

    Dim cctrlEntryPersedian As New ctrlEntryPersediaan
    Dim cctrlPenjualan As New ctrlPenjualan
    Dim cctrlLaporanPenjualan As New ctrlLaporanPenjualan
    Dim cctrlPembelian As New ctrlPembelian
    Dim cctrlLaporanPembelian As New ctrlLaporanPembelian
    Dim cctrlLaporanStok As New ctrlLaporanStok
    Dim cctrlUsers As New ctrlUsers
    Dim cctrlPembayaranPiutang As New ctrlPembayaranPiutang

    Private Function CheckControl(ByVal cControlName As String) As Boolean
        Dim lFound As Boolean = False
        For Each ctrl As UserControl In pnlApp.Controls
            If ctrl.Name = cControlName.Trim Then
                lFound = True
            Else
                lFound = False
            End If
        Next
        Return lFound
    End Function
    Private Sub btnPersediaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPersediaan.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "persediaan", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlEntryPersediaan") Then
            tmMenu.StartTransition(pnlApp)
            cctrlEntryPersedian.Show()
            cctrlEntryPersedian.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlEntryPersedian)
            cctrlEntryPersedian.Show()
            cctrlEntryPersedian.BringToFront()
            tmMenu.EndTransition()
        End If

    End Sub

    Private Sub btnPenjualan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPenjualan.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlPenjualan") Then
            tmMenu.StartTransition(pnlApp)

            cctrlPenjualan.Show()
            cctrlPenjualan.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlPenjualan)
            cctrlPenjualan.Show()
            cctrlPenjualan.BringToFront()
            tmMenu.EndTransition()
        End If

        'frmPenjualan.cBulan = cBulan.Trim
        'frmPenjualan.cTahun = cTahun.Trim
        'frmPenjualan.cUserId = cUserId.Trim
        'frmPenjualan.cNamaKasir = cNamaKasir.Trim
        'frmPenjualan.Show()
        'frmPenjualan.BringToFront()
    End Sub

    Private Sub btnPembelian_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPembelian.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "pembelian", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlPembelian") Then
            tmMenu.StartTransition(pnlApp)
            cctrlPembelian.Show()
            cctrlPembelian.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlPembelian)
            cctrlPembelian.Show()
            cctrlPembelian.BringToFront()
            tmMenu.EndTransition()
        End If

    End Sub

    Private Sub frmMenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DefaultLookAndFeel1.LookAndFeel.SetFlatStyle()
        MDITest.SetBevel(Me, False)
        txtUserLogin.Caption = "User Login : " & frmLogin.txtUserId.Text.Trim
        pnlApp.BackgroundImage = Image.FromFile(Application.StartupPath & "\background.jpg")
        pnlApp.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub frmMenuUtama_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Me.Dispose()
        frmLogin.Show()
    End Sub

    Private Sub btnLaporanPenjualan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLaporanPenjualan.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "report", "view_penjualan_rpt") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlLaporanPenjualan") Then
            tmMenu.StartTransition(pnlApp)
            cctrlLaporanPenjualan.Show()
            cctrlLaporanPenjualan.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlLaporanPenjualan)
            cctrlLaporanPenjualan.Show()
            cctrlLaporanPenjualan.BringToFront()
            tmMenu.EndTransition()
        End If
        'frmLaporanPenjualan.ShowDialog()
        'frmLaporanPenjualan.BringToFront()
    End Sub

    Private Sub btnLaporanPembelian_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLaporanPembelian.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "report", "view_pembelian_rpt") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlLaporanPembelian") Then
            tmMenu.StartTransition(pnlApp)
            cctrlLaporanPembelian.Show()
            cctrlLaporanPembelian.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlLaporanPembelian)
            cctrlLaporanPembelian.Show()
            cctrlLaporanPembelian.BringToFront()
            tmMenu.EndTransition()
        End If
        'frmLaporanPembelian.ShowDialog()
        'frmLaporanPembelian.BringToFront()
    End Sub

    Private Sub btnLaporanStok_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLaporanStok.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "report", "view_stok_rpt") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("cctrlLaporanStok") Then
            tmMenu.StartTransition(pnlApp)
            cctrlLaporanStok.Show()
            cctrlLaporanStok.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlLaporanStok)
            cctrlLaporanStok.Show()
            cctrlLaporanStok.BringToFront()
            tmMenu.EndTransition()
        End If
    End Sub

    Private Sub btnLaporanStokMinimum_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLaporanStokMinimum.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "report", "view_min_stok_rpt") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If
        frmLaporanStokMinimum.ShowDialog()
        frmLaporanStokMinimum.BringToFront()
    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub

    Private Sub btnEntryUsers_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEntryUsers.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "users", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If
        If CheckControl("cctrlUsers") Then
            tmMenu.StartTransition(pnlApp)
            cctrlUsers.Show()
            cctrlUsers.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlUsers)
            cctrlUsers.Show()
            cctrlUsers.BringToFront()
            tmMenu.EndTransition()
        End If
    End Sub

    Private Sub btnPembayaranPiutang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPembayaranPiutang.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "bayar_piutang", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If CheckControl("ctrlPembayaranPiutang") Then
            tmMenu.StartTransition(pnlApp)
            cctrlPembayaranPiutang.Show()
            cctrlPembayaranPiutang.BringToFront()
            tmMenu.EndTransition()
        Else
            tmMenu.StartTransition(pnlApp)
            pnlApp.Controls.Add(cctrlPembayaranPiutang)
            cctrlPembayaranPiutang.Show()
            cctrlPembayaranPiutang.BringToFront()
            tmMenu.EndTransition()
        End If
    End Sub
End Class