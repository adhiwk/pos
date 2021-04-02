Imports System.Data.SqlClient
Public Class frmCustomer
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmPasien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True
        btnClose.Enabled = True
    End Sub

    Private Sub ClearScreen()
        txtKode.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        txtHandPhone.Text = ""
    End Sub

    Private Sub DisableText()
        txtKode.Properties.ReadOnly = True
        txtNama.Properties.ReadOnly = True
        txtAlamat.Properties.ReadOnly = True
        txtHandPhone.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKode.Properties.ReadOnly = False
        txtNama.Properties.ReadOnly = False
        txtAlamat.Properties.ReadOnly = False
        txtHandPhone.Properties.ReadOnly = False
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKode.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKode.Focus()
            lNew = False
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "delete_customer"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_cust", SqlDbType.NChar, 10)).Value = txtKode.Text.Trim
                        .ExecuteNonQuery()

                        grdCust.Rows.RemoveAt(grdCust.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()
        If txtKode.Text.Trim = "" Then
            MsgBox("Kode Customer tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKode.Text.Trim, "select [kode cust] from customer " &
                                "where [kode cust] = '" & txtKode.Text.Trim & "'") Then
            MsgBox("kode customer tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKode.Focus()
            Exit Sub
        End If


        If txtNama.Text.Trim = "" Then
            MsgBox("nama customer tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNama.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_customer"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_cust", SqlDbType.NChar, 10)).Value = txtKode.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama", SqlDbType.NChar, 50)).Value = txtNama.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 15)).Value = txtHandPhone.Text.Trim
                    .ExecuteNonQuery()

                    grdCust.Rows.Add(New Object() {txtKode.Text.Trim,
                                                     txtNama.Text.Trim,
                                                     txtAlamat.Text.Trim,
                                                     txtHandPhone.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKode.Text.Trim = "" Then
            MsgBox("kode pasien tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKode.Focus()
            Exit Sub
        End If

        If txtNama.Text.Trim = "" Then
            MsgBox("nama pasirn tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNama.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_customer"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_cust", SqlDbType.NChar, 10)).Value = txtKode.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama", SqlDbType.NChar, 50)).Value = txtNama.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 15)).Value = txtHandPhone.Text.Trim
                    .ExecuteNonQuery()

                    grdCust.CurrentRow.Cells.Item(0).Value = txtKode.Text.Trim
                    grdCust.CurrentRow.Cells.Item(1).Value = txtNama.Text.Trim
                    grdCust.CurrentRow.Cells.Item(2).Value = txtAlamat.Text.Trim
                    grdCust.CurrentRow.Cells.Item(3).Value = txtHandPhone.Text.Trim
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode cust],[nama]," &
                                   "[alamat],[no hp] from customer order by [kode cust] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdCust.Rows.Clear()

                    While rDr.Read
                        grdCust.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                         rDr.Item(1).ToString.Trim,
                                                         rDr.Item(2).ToString.Trim,
                                                         rDr.Item(3).ToString.Trim})
                    End While
                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        lblRecord.Text = "Total Record : " & FormatNumber(grdCust.Rows.Count, 0)
    End Sub

    Private Sub grdPasien_Click(sender As Object, e As EventArgs) Handles grdCust.Click
        Try
            txtKode.Text = grdCust.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdCust.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdCust.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdCust.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnCari.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "select [kode cust],[nama]," &
                                       "[alamat],[no hp] from customer where [kode cust] = '" & btnCari.Text.Trim & "'"
                        Dim rDr As SqlDataReader = .ExecuteReader
                        grdCust.Rows.Clear()
                        While rDr.Read
                            grdCust.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                             rDr.Item(1).ToString.Trim,
                                                             rDr.Item(2).ToString.Trim,
                                                             rDr.Item(3).ToString.Trim})
                        End While
                        rDr.Close()


                        .CommandText = "select [kode cust],[nama]," &
                                      "[alamat],[no hp] from customer where [nama] like '%" & btnCari.Text.Trim & "%'"
                        rDr = .ExecuteReader
                        grdCust.Rows.Clear()
                        While rDr.Read
                            grdCust.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                             rDr.Item(1).ToString.Trim,
                                                             rDr.Item(2).ToString.Trim,
                                                             rDr.Item(3).ToString.Trim})
                        End While
                        rDr.Close()
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
            lblRecord.Text = "Total Record : " & FormatNumber(grdCust.Rows.Count, 0)
        End If
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        Try
            grdCust.GridNavigator.SelectFirstRow()
            txtKode.Text = grdCust.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdCust.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdCust.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdCust.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            grdCust.GridNavigator.SelectPreviousRow(1)
            txtKode.Text = grdCust.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdCust.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdCust.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdCust.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            grdCust.GridNavigator.SelectNextRow(1)
            txtKode.Text = grdCust.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdCust.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdCust.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdCust.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Try
            grdCust.GridNavigator.SelectLastRow()
            txtKode.Text = grdCust.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdCust.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdCust.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdCust.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class