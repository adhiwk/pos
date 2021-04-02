Imports System.Data.SqlClient
Public Class frmSuplier
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmSuplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtKodeSuplier.Text = ""
        txtNamaSuplier.Text = ""
        txtAlamatSuplier.Text = ""
        txtSuplierKenaPajak.Text = ""
        txtNPWP.Text = ""
        txtEmailKontak.Text = ""
        txtTelponKontak.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeSuplier.Properties.ReadOnly = True
        txtNamaSuplier.Properties.ReadOnly = True
        txtAlamatSuplier.Properties.ReadOnly = True
        txtSuplierKenaPajak.Properties.ReadOnly = True
        txtNPWP.Properties.ReadOnly = True
        txtEmailKontak.Properties.ReadOnly = True
        txtTelponKontak.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeSuplier.Properties.ReadOnly = False
        txtNamaSuplier.Properties.ReadOnly = False
        txtAlamatSuplier.Properties.ReadOnly = False
        txtSuplierKenaPajak.Properties.ReadOnly = False
        txtNPWP.Properties.ReadOnly = False
        txtEmailKontak.Properties.ReadOnly = False
        txtTelponKontak.Properties.ReadOnly = False
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
                        .CommandText = "delete_suplier"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 3)).Value = txtKodeSuplier.Text.Trim
                        .ExecuteNonQuery()

                        grdSuplier.Rows.RemoveAt(grdSuplier.CurrentCell.RowIndex)

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
        If txtKodeSuplier.Text.Trim = "" Then
            MsgBox("kode suplier tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeSuplier.Focus()
            Exit Sub
        End If

        If txtNamaSuplier.Text.Trim = "" Then
            MsgBox("nama suplier tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaSuplier.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeSuplier.Text.Trim, "select [kode suplier] " &
                                "from suplier where [kode suplier] = '" & txtKodeSuplier.Text.Trim & "'") Then
            MsgBox("kode suplier tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeSuplier.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_suplier"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 3)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_suplier", SqlDbType.NChar, 50)).Value = txtNamaSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamatSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@suplier_kena_pajak", SqlDbType.NChar, 1)).Value = txtSuplierKenaPajak.Text.Trim
                    .Parameters.Add(New SqlParameter("@npwp", SqlDbType.NChar, 50)).Value = txtNPWP.Text.Trim
                    .Parameters.Add(New SqlParameter("@email_kontak", SqlDbType.NChar, 50)).Value = txtEmailKontak.Text.Trim
                    .Parameters.Add(New SqlParameter("@telpon_kontak", SqlDbType.NChar, 50)).Value = txtTelponKontak.Text.Trim

                    .ExecuteNonQuery()

                    grdSuplier.Rows.Add(New Object() {txtKodeSuplier.Text.Trim,
                                                      txtNamaSuplier.Text.Trim,
                                                      txtAlamatSuplier.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeSuplier.Text.Trim = "" Then
            MsgBox("kode suplier tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeSuplier.Focus()
            Exit Sub
        End If

        If txtNamaSuplier.Text.Trim = "" Then
            MsgBox("nama suplier tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaSuplier.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_suplier"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 3)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_suplier", SqlDbType.NChar, 50)).Value = txtNamaSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamatSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@suplier_kena_pajak", SqlDbType.NChar, 1)).Value = txtSuplierKenaPajak.Text.Trim
                    .Parameters.Add(New SqlParameter("@npwp", SqlDbType.NChar, 50)).Value = txtNPWP.Text.Trim
                    .Parameters.Add(New SqlParameter("@email_kontak", SqlDbType.NChar, 50)).Value = txtEmailKontak.Text.Trim
                    .Parameters.Add(New SqlParameter("@telpon_kontak", SqlDbType.NChar, 50)).Value = txtTelponKontak.Text.Trim
                    .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdSuplier.CurrentRow.Cells.Item(3).Value.ToString.Trim
                    .ExecuteNonQuery()

                    grdSuplier.CurrentRow.Cells.Item(0).Value = txtKodeSuplier.Text.Trim
                    grdSuplier.CurrentRow.Cells.Item(1).Value = txtNamaSuplier.Text.Trim
                    grdSuplier.CurrentRow.Cells.Item(2).Value = txtAlamatSuplier.Text.Trim

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub grdSuplier_Click(sender As Object, e As EventArgs) Handles grdSuplier.Click
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try

                txtKodeSuplier.Text = grdSuplier.CurrentRow.Cells.Item(0).Value.ToString.Trim
                txtNamaSuplier.Text = grdSuplier.CurrentRow.Cells.Item(1).Value.ToString.Trim
                txtAlamatSuplier.Text = grdSuplier.CurrentRow.Cells.Item(2).Value.ToString.Trim

                With Cmd
                    .Connection = Conn
                    .CommandText = "select [suplier kena pajak],[npwp],[email kontak]," &
                                   "[phone kontak] from suplier " &
                                   "where  [kode suplier] = '" & txtKodeSuplier.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        txtSuplierKenaPajak.Text = rDr.Item(0).ToString.Trim
                        txtNPWP.Text = rDr.Item(1).ToString.Trim
                        txtEmailKontak.Text = rDr.Item(2).ToString.Trim
                        txtTelponKontak.Text = rDr.Item(3).ToString.Trim
                    End While

                    rDr.Close()
                End With

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using

        Conn.Close()

        btnTambah.Text = "&Ubah"
        btnHapus.Enabled = True
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeSuplier.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeSuplier.Focus()
            lNew = False
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode suplier],[nama suplier]," &
                                   "[alamat],[id] from suplier order by [id] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdSuplier.Rows.Clear()

                    While rDr.Read
                        grdSuplier.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
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
    End Sub
End Class