Imports System.Data.SqlClient
Public Class frmDokter
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmDokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtKodeDokter.Text = ""
        txtNamaDokter.Text = ""
        txtSpesialisasi.Text = ""
        txtNomorHandphone.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeDokter.Properties.ReadOnly = True
        txtNamaDokter.Properties.ReadOnly = True
        txtSpesialisasi.Properties.ReadOnly = True
        txtNomorHandphone.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeDokter.Properties.ReadOnly = False
        txtNamaDokter.Properties.ReadOnly = False
        txtSpesialisasi.Properties.ReadOnly = False
        txtNomorHandphone.Properties.ReadOnly = False
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeDokter.Focus()
            lNew = True
        ElseIf btnTambah.Text = "&Ubah" Then
            EnableText()
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnRefresh.Enabled = False
            txtKodeDokter.Focus()
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
                        .CommandText = "delete_dokter"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_dokter", SqlDbType.NChar, 3)).Value = txtKodeDokter.Text.Trim
                        .ExecuteNonQuery()

                        grdDokter.Rows.RemoveAt(grdDokter.CurrentCell.RowIndex)

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
        If txtKodeDokter.Text.Trim = "" Then
            MsgBox("kode dokter tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeDokter.Focus()
            Exit Sub
        End If

        If txtNamaDokter.Text.Trim = "" Then
            MsgBox("nama dokter tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaDokter.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeDokter.Text.Trim, "select [kode dokter] from dokter " &
                                "where [kode dokter] = '" & txtKodeDokter.Text.Trim & "'") Then
            MsgBox("kode dokter tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKodeDokter.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_dokter"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_dokter", SqlDbType.NChar, 5)).Value = txtKodeDokter.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_dokter", SqlDbType.NChar, 50)).Value = txtNamaDokter.Text.Trim
                    .Parameters.Add(New SqlParameter("@spesialisasi", SqlDbType.NChar, 30)).Value = txtSpesialisasi.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 50)).Value = txtNomorHandphone.Text.Trim
                    .ExecuteNonQuery()

                    grdDokter.Rows.Add(New Object() {txtKodeDokter.Text.Trim,
                                                     txtNamaDokter.Text.Trim,
                                                     txtSpesialisasi.Text.Trim,
                                                     txtNomorHandphone.Text.Trim})

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeDokter.Text.Trim = "" Then
            MsgBox("kode dokter tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKodeDokter.Focus()
            Exit Sub
        End If

        If txtNamaDokter.Text.Trim = "" Then
            MsgBox("nama dokter tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNamaDokter.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_dokter"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_dokter", SqlDbType.NChar, 5)).Value = txtKodeDokter.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_dokter", SqlDbType.NChar, 50)).Value = txtNamaDokter.Text.Trim
                    .Parameters.Add(New SqlParameter("@spesialisasi", SqlDbType.NChar, 30)).Value = txtSpesialisasi.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 50)).Value = txtNomorHandphone.Text.Trim
                    .ExecuteNonQuery()

                    grdDokter.CurrentRow.Cells.Item(0).Value = txtKodeDokter.Text.Trim
                    grdDokter.CurrentRow.Cells.Item(1).Value = txtNamaDokter.Text.Trim
                    grdDokter.CurrentRow.Cells.Item(2).Value = txtSpesialisasi.Text.Trim
                    grdDokter.CurrentRow.Cells.Item(3).Value = txtNomorHandphone.Text.Trim

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
                    .CommandText = "select [kode dokter],[nama dokter]," &
                                   "[spesialisasi],[no hp] from dokter " &
                                   "order by [kode dokter] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdDokter.Rows.Clear()

                    While rDr.Read
                        grdDokter.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
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

    Private Sub grdDokter_Click(sender As Object, e As EventArgs) Handles grdDokter.Click
        Try
            txtKodeDokter.Text = grdDokter.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNamaDokter.Text = grdDokter.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtSpesialisasi.Text = grdDokter.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtNomorHandphone.Text = grdDokter.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class