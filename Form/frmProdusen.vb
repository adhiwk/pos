Imports System.Data.SqlClient
Public Class frmProdusen
    Dim lNew As Boolean
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeProdusen.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeProdusen.Focus()
            lNew = False
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub ClearScreen()
        txtKodeProdusen.Text = ""
        txtNamaProdusen.Text = ""
        txtAlamatProdusen.Text = ""
        txtNPWP.Text = ""
        txtEmail.Text = ""
        txtTelpon.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeProdusen.Properties.ReadOnly = True
        txtNamaProdusen.Properties.ReadOnly = True
        txtAlamatProdusen.Properties.ReadOnly = True
        txtNPWP.Properties.ReadOnly = True
        txtEmail.Properties.ReadOnly = True
        txtTelpon.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeProdusen.Properties.ReadOnly = False
        txtNamaProdusen.Properties.ReadOnly = False
        txtAlamatProdusen.Properties.ReadOnly = False
        txtNPWP.Properties.ReadOnly = False
        txtEmail.Properties.ReadOnly = False
        txtTelpon.Properties.ReadOnly = False
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode produsen],[nama produsen]," &
                                   "[alamat],[id] from produsen order by [id] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdProdusen.Rows.Clear()

                    While rDr.Read
                        grdProdusen.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
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

    Private Sub frmProdusen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
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
                        .CommandText = "delete_produsen"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_produsen", SqlDbType.NChar, 3)).Value = txtKodeProdusen.Text.Trim
                        .ExecuteNonQuery()

                        grdProdusen.Rows.RemoveAt(grdProdusen.CurrentCell.RowIndex)

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
            SaveData
        Else
            UpdateData
        End If
    End Sub

    Private Sub SaveData()
        If txtKodeProdusen.Text.Trim = "" Then
            MsgBox("kode produsen tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeProdusen.Focus()
            Exit Sub
        End If

        If txtNamaProdusen.Text.Trim = "" Then
            MsgBox("nama produsen tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaProdusen.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeProdusen.Text.Trim, "select [kode produsen] " &
                                "from produsen where [kode produsen] = '" & txtKodeProdusen.Text.Trim & "'") Then
            MsgBox("kode produsen tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeProdusen.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_produsen"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_produsen", SqlDbType.NChar, 5)).Value = txtKodeProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_produsen", SqlDbType.NChar, 50)).Value = txtNamaProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamatProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@npwp", SqlDbType.NChar, 50)).Value = txtNPWP.Text.Trim
                    .Parameters.Add(New SqlParameter("@email_kontak", SqlDbType.NChar, 50)).Value = txtEmail.Text.Trim
                    .Parameters.Add(New SqlParameter("@telpon_kontak", SqlDbType.NChar, 50)).Value = txtTelpon.Text.Trim

                    .ExecuteNonQuery()

                    grdProdusen.Rows.Add(New Object() {txtKodeProdusen.Text.Trim,
                                                       txtNamaProdusen.Text.Trim,
                                                       txtAlamatProdusen.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeProdusen.Text.Trim = "" Then
            MsgBox("kode produsen tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeProdusen.Focus()
            Exit Sub
        End If

        If txtNamaProdusen.Text.Trim = "" Then
            MsgBox("nama produsen tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaProdusen.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeProdusen.Text.Trim, "select [kode produsen] " &
                                "from produsen where [kode produsen] = '" & txtKodeProdusen.Text.Trim & "'") Then
            MsgBox("kode produsen tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeProdusen.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_produsen"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_produsen", SqlDbType.NChar, 5)).Value = txtKodeProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_produsen", SqlDbType.NChar, 50)).Value = txtNamaProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamatProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@npwp", SqlDbType.NChar, 50)).Value = txtNPWP.Text.Trim
                    .Parameters.Add(New SqlParameter("@email_kontak", SqlDbType.NChar, 50)).Value = txtEmail.Text.Trim
                    .Parameters.Add(New SqlParameter("@telpon_kontak", SqlDbType.NChar, 50)).Value = txtTelpon.Text.Trim
                    .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdProdusen.CurrentRow.Cells.Item(3).Value.ToString.Trim
                    .ExecuteNonQuery()

                    grdProdusen.CurrentRow.Cells.Item(0).Value = txtKodeProdusen.Text.Trim
                    grdProdusen.CurrentRow.Cells.Item(1).Value = txtNamaProdusen.Text.Trim
                    grdProdusen.CurrentRow.Cells.Item(2).Value = txtAlamatProdusen.Text.Trim

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub grdProdusen_Click(sender As Object, e As EventArgs) Handles grdProdusen.Click

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try

                txtKodeProdusen.Text = grdProdusen.CurrentRow.Cells.Item(0).Value.ToString.Trim
                txtNamaProdusen.Text = grdProdusen.CurrentRow.Cells.Item(1).Value.ToString.Trim
                txtAlamatProdusen.Text = grdProdusen.CurrentRow.Cells.Item(2).Value.ToString.Trim

                With Cmd
                    .Connection = Conn
                    .CommandText = "select [npwp],[email kontak]," &
                                   "[telpon kontak] from produsen " &
                                   "where  [kode produsen] = '" & txtKodeProdusen.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        txtNPWP.Text = rDr.Item(0).ToString.Trim
                        txtEmail.Text = rDr.Item(1).ToString.Trim
                        txtTelpon.Text = rDr.Item(2).ToString.Trim
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
End Class