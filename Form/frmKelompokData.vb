Imports System.Data.SqlClient
Public Class frmKelompokData
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
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
            txtJenisData.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtJenisData.Focus()
            lNew = False
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub frmKelompokData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtJenisData.Text = ""
        txtKeteranganData.Text = ""
    End Sub

    Private Sub DisableText()
        txtJenisData.Properties.ReadOnly = True
        txtKeteranganData.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtJenisData.Properties.ReadOnly = False
        txtKeteranganData.Properties.ReadOnly = False
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
                        .CommandText = "delete_tabel_bantuan"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdJenisData.CurrentRow.Cells.Item(0).Value.ToString.Trim
                        .ExecuteNonQuery()

                        grdJenisData.Rows.RemoveAt(grdJenisData.CurrentCell.RowIndex)

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
        If txtJenisData.Text.Trim = "" Then
            MsgBox("jenis data tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtJenisData.Focus()
            Exit Sub
        End If

        If txtKeteranganData.Text.Trim = "" Then
            MsgBox("keterangan data tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKeteranganData.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKeteranganData.Text.Trim, "select [keterangan] " &
                                "from tabel_bantuan where [keterangan] = '" &
                                txtKeteranganData.Text.Trim & "' and [jenis data] = '" &
                                txtJenisData.Text.Trim & "'") Then
            MsgBox("grup data tersebut sudah tersimpan dalam database, silahkan isikan grup lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKeteranganData.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_tabel_bantuan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@jenis_data", SqlDbType.NChar, 50)).Value = txtJenisData.Text.Trim
                    .Parameters.Add(New SqlParameter("@keterangan", SqlDbType.NChar, 50)).Value = txtKeteranganData.Text.Trim
                    .ExecuteNonQuery()

                    grdJenisData.Rows.Add(New Object() {txtJenisData.Text.Trim, txtKeteranganData.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtJenisData.Text.Trim = "" Then
            MsgBox("jenis data tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtJenisData.Focus()
            Exit Sub
        End If

        If txtKeteranganData.Text.Trim = "" Then
            MsgBox("keterangan data tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKeteranganData.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKeteranganData.Text.Trim, "select [keterangan] " &
                                "from tabel_bantuan where [keterangan] = '" &
                                txtKeteranganData.Text.Trim & "' and [jenis data] = '" &
                                txtJenisData.Text.Trim & "'") Then
            MsgBox("grup data tersebut sudah tersimpan dalam database, silahkan isikan grup lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKeteranganData.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_tabel_bantuan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@jenis_data", SqlDbType.NChar, 50)).Value = txtJenisData.Text.Trim
                    .Parameters.Add(New SqlParameter("@keterangan", SqlDbType.NChar, 50)).Value = txtKeteranganData.Text.Trim
                    .Parameters.Add(New SqlParameter("@rowid", SqlDbType.Int)).Value = grdJenisData.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    .ExecuteNonQuery()

                    grdJenisData.Rows.Add(New Object() {txtJenisData.Text.Trim, txtKeteranganData.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub
End Class