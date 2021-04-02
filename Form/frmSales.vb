﻿Imports System.Data.SqlClient
Public Class frmSales
    Dim lNew As Boolean
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                        .CommandText = "delete_sales"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_sales", SqlDbType.NChar, 5)).Value = txtKode.Text.Trim
                        .ExecuteNonQuery()

                        grdSales.Rows.RemoveAt(grdSales.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode sales],[nama]," &
                                   "[alamat],[no hp] from sales order by [kode sales] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    grdSales.Rows.Clear()

                    While rDr.Read
                        grdSales.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
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

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()
        If txtKode.Text.Trim = "" Then
            MsgBox("Kode sales tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKode.Text.Trim, "select [kode sales] from sales " &
                                "where [kode sales] = '" & txtKode.Text.Trim & "'") Then
            MsgBox("kode sales tersebut sudah tersimpan dalam database, silahkan isikan kode lainnya", MsgBoxStyle.Exclamation, "Error")
            txtKode.Focus()
            Exit Sub
        End If


        If txtNama.Text.Trim = "" Then
            MsgBox("nama sales tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNama.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_sales"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_sales", SqlDbType.NChar, 5)).Value = txtKode.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama", SqlDbType.NChar, 50)).Value = txtNama.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 15)).Value = txtHandPhone.Text.Trim
                    .ExecuteNonQuery()

                    grdSales.Rows.Add(New Object() {txtKode.Text.Trim,
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
            MsgBox("kode sales tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtKode.Focus()
            Exit Sub
        End If

        If txtNama.Text.Trim = "" Then
            MsgBox("nama sales tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtNama.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_sales"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_sales", SqlDbType.NChar, 5)).Value = txtKode.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama", SqlDbType.NChar, 50)).Value = txtNama.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat", SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_hp", SqlDbType.NChar, 15)).Value = txtHandPhone.Text.Trim
                    .ExecuteNonQuery()

                    grdSales.CurrentRow.Cells.Item(0).Value = txtKode.Text.Trim
                    grdSales.CurrentRow.Cells.Item(1).Value = txtNama.Text.Trim
                    grdSales.CurrentRow.Cells.Item(2).Value = txtAlamat.Text.Trim
                    grdSales.CurrentRow.Cells.Item(3).Value = txtHandPhone.Text.Trim
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub grdPasien_Click(sender As Object, e As EventArgs) Handles grdSales.Click
        Try
            txtKode.Text = grdSales.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNama.Text = grdSales.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtAlamat.Text = grdSales.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtHandPhone.Text = grdSales.CurrentRow.Cells.Item(3).Value.ToString.Trim

            btnTambah.Text = "&Ubah"
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class