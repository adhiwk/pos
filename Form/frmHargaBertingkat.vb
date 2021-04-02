Imports System.Data.SqlClient
Public Class frmHargaBertingkat
    Dim lNew As Boolean
    Dim nNo As Integer = 1

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub frmHargaBertingkat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nNo = 1
        btnTambah.Enabled = True
        btnUbah.Enabled = False
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnTutup.Enabled = True

        ClearScreen()
        ClearDetail()
        txtBarcode.Text = ""
    End Sub

    Private Sub ClearScreen()
        btnPencairanItem.Text = ""
        txtPLU.Text = ""
        txtNamaBarang.Text = ""
        txtSatuan.Text = ""
        txtKodeSuplier.Text = ""
        txtNamaSuplier.Text = ""
        txtHargaBeli.Text = ""
        txtPPN.Text = ""
        txtMargin.Text = ""
        txtHargaJual.Text = ""
    End Sub

    Private Sub ClearDetail()
        'txtBarcode.Text = ""
        txtQtyMin.Text = ""
        txtQtyMax.Text = ""
        txtHargaJualPerItem.Text = ""
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            grdHargaBertingkat.Rows.Add(New Object() {nNo.ToString.Trim,
                                        txtBarcode.Text.Trim,
                                        Val(txtQtyMin.Text.Trim),
                                        Val(txtQtyMax.Text.Trim),
                                        txtHargaJualPerItem.Text.Trim})

            nNo = nNo + 1
            ClearDetail()
            txtQtyMin.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPencairanItem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPencairanItem.KeyPress
        If Asc(e.KeyChar) = 13 Then

            'Using cfrmCariBarang As New frmCariAndEntryBarang
            '    Try
            '        ClearScreen()
            '        grdHargaBertingkat.Rows.Clear()

            '        cfrmCariBarang.ShowDialog()
            '        txtPLU.Text = cfrmCariBarang.grdCariBarang.CurrentRow.Cells.Item(3).Value.ToString.Trim
            '        txtBarcode.Text = cfrmCariBarang.grdCariBarang.CurrentRow.Cells.Item(2).Value.ToString.Trim
            '        txtNamaBarang.Text = cfrmCariBarang.grdCariBarang.CurrentRow.Cells.Item(0).Value.ToString.Trim

            '    Catch ex As Exception

            '    End Try
            'End Using

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            Try
                Dim Conn As SqlConnection = clsPublic.KoneksiSQL
                Conn.Open()
                Using Cmd As New SqlCommand()
                    With Cmd
                        .Connection = Conn

                        .CommandText = "select [plu],[kode barang],[nama barang]," &
                                       "[satuan terkecil],[kode suplier]," &
                                       "[harga pokok pembelian],[ppn masuk],[harga jual toko]  from barang " &
                                       "WHERE barang.[kode barang] = '" & btnPencairanItem.Text.Trim & "'"

                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            txtPLU.Text = rDr.Item(0).ToString.Trim
                            txtBarcode.Text = rDr.Item(1).ToString.Trim
                            txtNamaBarang.Text = rDr.Item(2).ToString.Trim
                            txtSatuan.Text = rDr.Item(3).ToString.Trim
                            txtKodeSuplier.Text = rDr.Item(4).ToString.Trim
                            txtHargaBeli.Text = Decimal.Parse(rDr.Item(5).ToString.Trim)
                            txtPPN.Text = Decimal.Parse(rDr.Item(6).ToString.Trim)
                            txtMargin.Text = 0
                            txtHargaJual.Text = Decimal.Parse(rDr.Item(7).ToString.Trim)
                        End While
                        rDr.Close()

                        .CommandText = "select [barcode],[qty min],[qty max],[harga jual] from barang_harga_bertingkat " &
                                       "where [barcode] = '" & txtBarcode.Text.Trim & "'"
                        rDr = .ExecuteReader
                        grdHargaBertingkat.Rows.Clear()
                        nNo = 1
                        While rDr.Read
                            grdHargaBertingkat.Rows.Add(New Object() {nNo.ToString.Trim,
                                                        rDr.Item(0).ToString.Trim,
                                                        Val(rDr.Item(1).ToString.Trim),
                                                        Val(rDr.Item(2).ToString.Trim),
                                                        FormatNumber(Val(rDr.Item(3).ToString.Trim), 2)})

                        End While

                        If rDr.HasRows = False Then
                            btnTambah.Enabled = True
                            btnUbah.Enabled = False
                            btnHapus.Enabled = False
                            btnSimpan.Enabled = False
                        Else
                            btnTambah.Enabled = False
                            btnUbah.Enabled = True
                            btnHapus.Enabled = True
                            btnSimpan.Enabled = False
                        End If
                        rDr.Close()

                    End With
                End Using
                Conn.Close()

                txtQtyMin.Focus()
            Catch ex As Exception

            End Try
            xForm.Dispose()
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            lNew = True
            btnTambah.Text = "&Batal"
            btnUbah.Text = "&Ubah"
            btnUbah.Enabled = False
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
        Else
            btnTambah.Text = "&Tambah"
            btnUbah.Text = "&Ubah"
            btnUbah.Enabled = False
            btnHapus.Enabled = False
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If btnUbah.Text = "&Ubah" Then
            btnTambah.Text = "&Tambah"
            btnTambah.Enabled = False
            btnUbah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            lNew = False
        Else
            btnTambah.Text = "&Tambah"
            btnTambah.Enabled = True
            btnUbah.Text = "&Ubah"
            btnUbah.Enabled = False
            btnSimpan.Enabled = False
            btnHapus.Enabled = False
        End If
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData
        Else
            UpdateData
        End If
    End Sub

    Private Sub SaveData()
        If txtPLU.Text.Trim = "" Then
            MsgBox("nomor PLU tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtPLU.Focus()
            Exit Sub
        End If

        If grdHargaBertingkat.RowCount = 0 Then
            MsgBox("record harga bertingkat belum diisi", MsgBoxStyle.Exclamation, "Error")
            txtQtyMin.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_harga_bertingkat"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"

                    Dim dtHarga As New DataTable()
                    dtHarga.Columns.Add("plu", GetType([String])).MaxLength = 7
                    dtHarga.Columns.Add("kode barang", GetType([String])).MaxLength = 25
                    dtHarga.Columns.Add("qty min", GetType(Decimal))
                    dtHarga.Columns.Add("qty max", GetType(Decimal))
                    dtHarga.Columns.Add("harga jual", GetType(Decimal))

                    For Each DataRows In grdHargaBertingkat.Rows
                        dtHarga.Rows.Add(New Object() {
                            txtPLU.Text.Trim,
                            DataRows.Cells.Item(1).Value.ToString.Trim,
                            Val(DataRows.Cells.Item(2).Value.ToString.Trim),
                            Val(DataRows.Cells.Item(3).Value.ToString.Trim),
                            Val(clsPublic.ConvertNumeric(DataRows.Cells.Item(4).Value.ToString.Trim))
                         })
                    Next
                    .Parameters.Add("@HargaBarangBertingkatTVP", SqlDbType.Structured).Value = dtHarga

                    .ExecuteNonQuery()


                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        MsgBox("Data berhasil disimpan")

        nNo = 1
        btnTambah.Enabled = True
        btnTambah.Text = "&Tambah"
        btnUbah.Enabled = False
        btnUbah.Text = "&Ubah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnTutup.Enabled = True

        ClearScreen()
        ClearDetail()
        txtBarcode.Text = ""
        grdHargaBertingkat.Rows.Clear()
    End Sub

    Private Sub UpdateData()
        If txtPLU.Text.Trim = "" Then
            MsgBox("nomor PLU tidak boleh dalam keadaan kosong", MsgBoxStyle.Exclamation, "Error")
            txtPLU.Focus()
            Exit Sub
        End If

        If grdHargaBertingkat.RowCount = 0 Then
            MsgBox("record harga bertingkat belum diisi", MsgBoxStyle.Exclamation, "Error")
            txtQtyMin.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_harga_bertingkat"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add("@plu", SqlDbType.NChar, 7).Value = txtPLU.Text.Trim
                    .Parameters.Add("@kode_barang", SqlDbType.NChar, 25).Value = txtBarcode.Text.Trim

                    Dim dtHarga As New DataTable()
                    dtHarga.Columns.Add("plu", GetType([String])).MaxLength = 7
                    dtHarga.Columns.Add("kode barang", GetType([String])).MaxLength = 25
                    dtHarga.Columns.Add("qty min", GetType(Decimal))
                    dtHarga.Columns.Add("qty max", GetType(Decimal))
                    dtHarga.Columns.Add("harga jual", GetType(Decimal))

                    For Each DataRows In grdHargaBertingkat.Rows
                        dtHarga.Rows.Add(New Object() {
                            txtPLU.Text.Trim,
                            DataRows.Cells.Item(1).Value.ToString.Trim,
                            Val(DataRows.Cells.Item(2).Value.ToString.Trim),
                            Val(DataRows.Cells.Item(3).Value.ToString.Trim),
                            Val(clsPublic.ConvertNumeric(DataRows.Cells.Item(4).Value.ToString.Trim))
                         })
                    Next
                    .Parameters.Add("@HargaBarangBertingkatTVP", SqlDbType.Structured).Value = dtHarga
                    .ExecuteNonQuery()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        MsgBox("Data berhasil diupdate")

        nNo = 1
        btnTambah.Enabled = True
        btnTambah.Text = "&Tambah"
        btnUbah.Enabled = False
        btnUbah.Text = "&Ubah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnTutup.Enabled = True

        ClearScreen()
        ClearDetail()
        txtBarcode.Text = ""
        grdHargaBertingkat.Rows.Clear()
    End Sub

    Private Sub grdHargaBertingkat_Click(sender As Object, e As EventArgs) Handles grdHargaBertingkat.Click
        Try
            txtBarcode.Text = grdHargaBertingkat.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtQtyMin.Text = grdHargaBertingkat.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtQtyMax.Text = grdHargaBertingkat.CurrentRow.Cells.Item(3).Value.ToString.Trim
            txtHargaJualPerItem.Text = grdHargaBertingkat.CurrentRow.Cells.Item(4).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            grdHargaBertingkat.Rows.RemoveAt(grdHargaBertingkat.CurrentCell.RowIndex)
            ClearDetail()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "Konfirmasi")
        If cMsg = vbOK Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "delete_harga_bertingkat"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add("@plu", SqlDbType.NChar, 7).Value = txtPLU.Text.Trim
                        .Parameters.Add("@kode_barang", SqlDbType.NChar, 25).Value = txtBarcode.Text.Trim

                        .ExecuteNonQuery()


                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using

            Conn.Close()

            MsgBox("Data berhasil dihapus")
            ClearScreen()
            ClearDetail()
            grdHargaBertingkat.Rows.Clear()

            btnHapus.Enabled = False
            btnSimpan.Enabled = False
            btnUbah.Enabled = False
            btnTambah.Enabled = True
            btnTambah.Text = "&Tambah"
            btnUbah.Text = "&Ubah"
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            grdHargaBertingkat.CurrentRow.Cells.Item(1).Value = txtBarcode.Text.Trim
            grdHargaBertingkat.CurrentRow.Cells.Item(2).Value = Val(txtQtyMin.Text.Trim)
            grdHargaBertingkat.CurrentRow.Cells.Item(3).Value = Val(txtQtyMax.Text.Trim)
            grdHargaBertingkat.CurrentRow.Cells.Item(4).Value = Val(clsPublic.ConvertNumeric(txtHargaJualPerItem.Text.Trim))
        Catch ex As Exception

        End Try
    End Sub
End Class