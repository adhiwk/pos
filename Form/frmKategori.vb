Imports System.Data.SqlClient
Public Class frmKategori
    Dim lNew As Boolean
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "delete_kategori"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 3)).Value = txtKodeKategori.Text.Trim
                        .ExecuteNonQuery()

                        grdKategori.Rows.RemoveAt(grdKategori.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmKategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub

    Private Sub ClearScreen()
        txtKodeKategori.Text = ""
        txtNamaKategori.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeKategori.Properties.ReadOnly = True
        txtNamaKategori.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeKategori.Properties.ReadOnly = False
        txtNamaKategori.Properties.ReadOnly = False
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeKategori.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeKategori.Focus()
            lNew = False
        Else
            DefaultSetting()
        End If
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

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData
        Else
            UpdateData
        End If
    End Sub

    Private Sub SaveData()
        If txtKodeKategori.Text.Trim = "" Then
            MsgBox("kode kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeKategori.Focus()
            Exit Sub
        End If

        If txtNamaKategori.Text.Trim = "" Then
            MsgBox("nama kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaKategori.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeKategori.Text.Trim, "select [kode kategori] from kategori where [kode kategori] = '" & txtKodeKategori.Text.Trim & "'") Then
            MsgBox("kode kategori tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeKategori.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_kategori"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 5)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_kategori", SqlDbType.NChar, 50)).Value = txtNamaKategori.Text.Trim
                    .ExecuteNonQuery()

                    grdKategori.Rows.Add(New Object() {txtKodeKategori.Text.Trim, txtNamaKategori.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeKategori.Text.Trim = "" Then
            MsgBox("kode kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeKategori.Focus()
            Exit Sub
        End If

        If txtNamaKategori.Text.Trim = "" Then
            MsgBox("nama kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaKategori.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_kategori"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 5)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_kategori", SqlDbType.NChar, 50)).Value = txtNamaKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdKategori.CurrentRow.Cells.Item(2).Value.ToString.Trim
                    .ExecuteNonQuery()

                    grdKategori.CurrentRow.Cells.Item(0).Value = txtKodeKategori.Text.Trim
                    grdKategori.CurrentRow.Cells.Item(1).Value = txtNamaKategori.Text.Trim
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
                        .CommandText = "select [kode kategori],[nama kategori],[id] from kategori order by [id] asc"
                        Dim rDr As SqlDataReader = .ExecuteReader

                        grdKategori.Rows.Clear()

                        While rDr.Read
                            grdKategori.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                               rDr.Item(1).ToString.Trim,
                                                               rDr.Item(2).ToString.Trim})
                        End While
                        rDr.Close()
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()

    End Sub

    Private Sub grdKategori_Click(sender As Object, e As EventArgs) Handles grdKategori.Click
        Try
            txtKodeKategori.Text = grdKategori.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNamaKategori.Text = grdKategori.CurrentRow.Cells.Item(1).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class