Imports System.Data.SqlClient
Public Class frmSubKategori
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
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

        If txtKodeSubKategori.Text.Trim = "" Then
            MsgBox("kode  sub kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeSubKategori.Focus()
            Exit Sub
        End If

        If txtNamaSubKategori.Text.Trim = "" Then
            MsgBox("nama sub kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaSubKategori.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeSubKategori.Text.Trim, "select [kode sub kategori] from sub_kategori where [kode sub kategori] = '" & txtKodeSubKategori.Text.Trim & "'") Then
            MsgBox("kode sub kategori tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeSubKategori.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_sub_kategori"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 5)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_sub_kategori", SqlDbType.NChar, 10)).Value = txtKodeSubKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_sub_kategori", SqlDbType.NChar, 50)).Value = txtNamaSubKategori.Text.Trim

                    .ExecuteNonQuery()

                    grdSubKategori.Rows.Add(New Object() {txtNamaSubKategori.Text.Trim,
                                                          txtKodeSubKategori.Text.Trim,
                                                          txtKodeKategori.Text.Trim})

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

        If txtKodeSubKategori.Text.Trim = "" Then
            MsgBox("kode  sub kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeSubKategori.Focus()
            Exit Sub
        End If

        If txtNamaSubKategori.Text.Trim = "" Then
            MsgBox("nama sub kategori tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaSubKategori.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_sub_kategori"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 5)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_sub_kategori", SqlDbType.NChar, 10)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_sub_kategori", SqlDbType.NChar, 50)).Value = txtNamaSubKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdSubKategori.CurrentRow.Cells.Item(3).Value.ToString.Trim
                    .ExecuteNonQuery()

                    grdSubKategori.CurrentRow.Cells.Item(0).Value = txtNamaSubKategori.Text.Trim
                    grdSubKategori.CurrentRow.Cells.Item(1).Value = txtKodeSubKategori.Text.Trim
                    grdSubKategori.CurrentRow.Cells.Item(2).Value = txtKodeKategori.Text.Trim

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using

        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub frmSubKategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
        LoadDataKategori
    End Sub

    Private Sub LoadDataKategori()

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "select [nama kategori] from kategori order by [id] asc"
                        Dim rDr As SqlDataReader = .ExecuteReader
                        cboKategori.Properties.Items.Clear()
                        While rDr.Read
                            cboKategori.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                        End While
                        rDr.Close()
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
        Conn.Close()
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
        cboKategori.Text = ""
        txtKodeKategori.Text = ""
        txtNamaSubKategori.Text = ""
        txtKodeSubKategori.Text = ""
    End Sub

    Private Sub DisableText()
        cboKategori.Properties.ReadOnly = True
        txtKodeKategori.Properties.ReadOnly = True
        txtNamaSubKategori.Properties.ReadOnly = True
        txtKodeSubKategori.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        cboKategori.Properties.ReadOnly = False
        txtKodeKategori.Properties.ReadOnly = True
        txtNamaSubKategori.Properties.ReadOnly = False
        txtKodeSubKategori.Properties.ReadOnly = False
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
                        .CommandText = "delete_sub_kategori"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_sub_kategori", SqlDbType.NChar, 6)).Value = txtKodeSubKategori.Text.Trim
                        .ExecuteNonQuery()

                        grdSubKategori.Rows.RemoveAt(grdSubKategori.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
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
            cboKategori.Focus()
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
                    .CommandText = "select [nama sub kategori],[kode sub kategori],[kode kategori],[id] from sub_kategori order by [id] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdSubKategori.Rows.Clear()

                    While rDr.Read
                        grdSubKategori.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
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

    Private Sub cboKategori_EditValueChanged(sender As Object, e As EventArgs) Handles cboKategori.EditValueChanged
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "select [kode kategori] from kategori where [nama kategori] = '" & cboKategori.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        txtKodeKategori.Text = rDr.Item(0).ToString.Trim
                        txtKodeSubKategori.Text = rDr.Item(0).ToString.Trim & "."
                    End While

                    rDr.Close()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
    End Sub

    Private Sub grdSubKategori_Click(sender As Object, e As EventArgs) Handles grdSubKategori.Click
        Try
            txtNamaSubKategori.Text = grdSubKategori.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtKodeSubKategori.Text = grdSubKategori.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtKodeKategori.Text = grdSubKategori.CurrentRow.Cells.Item(2).Value.ToString.Trim

            LoadKategori()

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadKategori()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama kategori] from kategori where [kode kategori] = '" & txtKodeKategori.Text.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        cboKategori.Text = rDr.Item(0).ToString.Trim
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