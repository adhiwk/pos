Imports System.Data.SqlClient
Public Class frmGudang
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
            txtKodeGudang.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeGudang.Focus()
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
    Private Sub ClearScreen()
        txtKodeGudang.Text = ""
        txtNamaGudang.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeGudang.Properties.ReadOnly = True
        txtNamaGudang.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeGudang.Properties.ReadOnly = False
        txtNamaGudang.Properties.ReadOnly = False
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
                        .CommandText = "delete_gudang"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_gudang", SqlDbType.NChar, 3)).Value = txtKodeGudang.Text.Trim
                        .ExecuteNonQuery()

                        grdGudang.Rows.RemoveAt(grdGudang.CurrentCell.RowIndex)

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
        If txtKodeGudang.Text.Trim = "" Then
            MsgBox("kode gudang tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeGudang.Focus()
            Exit Sub
        End If

        If txtNamaGudang.Text.Trim = "" Then
            MsgBox("nama gudang tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaGudang.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeGudang.Text.Trim, "select [kode gudang] " &
                                "from barang_gudang where [kode gudang] = '" & txtKodeGudang.Text.Trim & "'") Then
            MsgBox("kode kategori tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeGudang.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_gudang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_gudang", SqlDbType.NChar, 3)).Value = txtKodeGudang.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_gudang", SqlDbType.NChar, 50)).Value = txtNamaGudang.Text.Trim
                    .ExecuteNonQuery()

                    grdGudang.Rows.Add(New Object() {txtKodeGudang.Text.Trim, txtNamaGudang.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeGudang.Text.Trim = "" Then
            MsgBox("kode gudang tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeGudang.Focus()
            Exit Sub
        End If

        If txtNamaGudang.Text.Trim = "" Then
            MsgBox("nama gudang tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaGudang.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_gudang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_gudang", SqlDbType.NChar, 3)).Value = txtKodeGudang.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_gudang", SqlDbType.NChar, 50)).Value = txtNamaGudang.Text.Trim
                    .ExecuteNonQuery()

                    grdGudang.Rows.Add(New Object() {txtKodeGudang.Text.Trim, txtNamaGudang.Text.Trim})

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
                    .CommandText = "select [kode gudang],[nama gudang] from barang_gudang order by [kode gudang] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdGudang.Rows.Clear()

                    While rDr.Read
                        grdGudang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                         rDr.Item(1).ToString.Trim})
                    End While
                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
    End Sub

    Private Sub grdGudang_Click(sender As Object, e As EventArgs) Handles grdGudang.Click
        Try
            txtKodeGudang.Text = grdGudang.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNamaGudang.Text = grdGudang.CurrentRow.Cells.Item(1).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmGudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub
End Class