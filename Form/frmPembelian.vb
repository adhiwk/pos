Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmPembelian
    Friend cUserId As String
    Friend cCrudAction As String
    Friend cNomorFaktur As String

    Dim lNew As Boolean
    Dim nQtyAwal As Decimal = 0
    Dim nPersenHargaToko As Decimal = 0
    Dim nPersenHargaGrosir As Decimal = 0

    Private Sub frmPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmMenuUtama
        DefaultSetting()
        LoadSettingHarga
    End Sub

    Private Sub LoadSettingHarga()
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [persen harga toko],[persen harga grosir] " &
                                   "from barang_pengaturan_markup"


                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        nPersenHargaToko = Val(rDr.Item(0).ToString.Trim)
                        nPersenHargaGrosir = Val(rDr.Item(1).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub btnBarangBaru_Click(sender As Object, e As EventArgs) Handles btnBarangBaru.Click
    '    frmEntryPersediaan.cUserId = "admin"
    '    frmEntryPersediaan.lNew = True
    '    frmEntryPersediaan.ShowDialog()
    '    txtNamaObat.Focus()
    'End Sub

    Private Sub DefaultSetting()
        DisableText()
        ClearHeader()
        ClearDetail()
        ClearFooter()
        grdPembelian.Rows.Clear()
        btnBatal.Text = "&Baru"
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If btnBatal.Text = "&Baru" Then
            lNew = True
            btnBatal.Text = "&Batal"
            ClearHeader()
            ClearDetail()
            ClearFooter()
            EnableText()
            chkTunai.Checked = True
            dtTglFaktur.DateTime = Now.Date
            dtTerima.DateTime = Now.Date
            dtJatuhTempo.DateTime = Now.Date
            grdPembelian.Rows.Clear()
            txtNamaSuplier.Focus()
        ElseIf btnBatal.Text = "&Batal" Then
            lNew = False
            btnBatal.Text = "&Baru"
            ClearHeader()
            ClearDetail()
            ClearFooter()
            DisableText()
            grdPembelian.Rows.Clear()
        End If
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub ClearDetail()
        txtNamaBarang.Text = ""
        txtKodeBarang.Text = ""
    End Sub

    Private Sub ClearFooter()
        dtTerima.Text = ""
        dtJatuhTempo.Text = ""
        txtBiayaLain.Text = ""
        txtTotalPajak.Text = ""
        txtTotalSetelahPajak.Text = ""
        txtDibayarDimuka.Text = ""
        txtSaldoTerhutang.Text = ""
    End Sub

    Private Sub ClearHeader()
        txtNamaSuplier.Text = ""
        txtKodeSuplier.Text = ""
        txtNomorFaktur.Text = ""
        dtTglFaktur.Text = ""
        chkInvoice.Checked = False
        chkTunai.Checked = False
    End Sub
    Private Sub DisableText()
        txtNamaSuplier.Properties.ReadOnly = True
        txtKodeSuplier.Properties.ReadOnly = True
        txtNomorFaktur.Properties.ReadOnly = True
        dtTglFaktur.Properties.ReadOnly = True
        chkInvoice.Properties.ReadOnly = True
        chkTunai.Properties.ReadOnly = True
        txtNamaBarang.Properties.ReadOnly = True
        txtKodeBarang.Properties.ReadOnly = True
        dtTerima.Properties.ReadOnly = True
        dtJatuhTempo.Properties.ReadOnly = True

        txtBiayaLain.Properties.ReadOnly = True
        txtTotalPajak.Properties.ReadOnly = True
        txtTotalSetelahPajak.Properties.ReadOnly = True
        txtDibayarDimuka.Properties.ReadOnly = True
        txtSaldoTerhutang.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtNamaSuplier.Properties.ReadOnly = False
        txtKodeSuplier.Properties.ReadOnly = True
        txtNomorFaktur.Properties.ReadOnly = False
        dtTglFaktur.Properties.ReadOnly = False
        chkInvoice.Properties.ReadOnly = False
        chkTunai.Properties.ReadOnly = False
        txtNamaBarang.Properties.ReadOnly = False
        txtKodeBarang.Properties.ReadOnly = True
        dtTerima.Properties.ReadOnly = False
        dtJatuhTempo.Properties.ReadOnly = False

        txtBiayaLain.Properties.ReadOnly = False
        txtTotalPajak.Properties.ReadOnly = True
        txtTotalSetelahPajak.Properties.ReadOnly = True
        txtDibayarDimuka.Properties.ReadOnly = False
        txtSaldoTerhutang.Properties.ReadOnly = True
    End Sub

    Private Sub txtNamaSuplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaSuplier.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariSuplier As New frmCariSuplier
                Try

                    cfrmCariSuplier.ShowDialog()
                    txtNamaSuplier.Text = cfrmCariSuplier.grdCariSuplier.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeSuplier.Text = cfrmCariSuplier.grdCariSuplier.CurrentRow.Cells.Item(1).Value.ToString.Trim
                    txtNomorFaktur.Focus()

                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub txtNamaBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariBarang As New frmCariAndEntryBarang
                Try

                    cfrmCariBarang.ShowDialog()
                    txtNamaBarang.Text = cfrmCariBarang.grdCariBarang.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeBarang.Text = cfrmCariBarang.grdCariBarang.CurrentRow.Cells.Item(2).Value.ToString.Trim

                    'cari data obat 
                    'and add Data to grid
                    CariDataBarang(txtKodeBarang.Text.Trim)
                    ClearDetail()
                    txtNamaBarang.Focus()

                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub CariDataBarang(ByVal cKodeBarang As String)
        Dim nPajak As Decimal = 0
        Dim cPajak As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Try
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode barang],[nama barang],[satuan terkecil],[harga pokok pembelian],[kena pajak] " &
                                   "from barang where [kode barang] = '" & cKodeBarang.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read

                        If rDr.Item(4).ToString.Trim = "Y" Then
                            cPajak = "Y"
                            nPajak = Val(rDr.Item(3).ToString.Trim) * 10 / 100
                        Else
                            cPajak = "T"
                            nPajak = 0
                        End If

                        grdPembelian.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                       rDr.Item(1).ToString.Trim, 1,
                                                       rDr.Item(2).ToString.Trim,
                                                       FormatNumber(Val(rDr.Item(3).ToString.Trim), 2),
                                                       0, 0,
                                                       FormatNumber(Val(rDr.Item(3).ToString.Trim), 2),
                                                       cPajak.Trim,
                                                       FormatNumber(Val(nPajak), 2)})
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
        HitungTotal()
    End Sub

    Private Sub HitungSubTotal()

        grdPembelian.CurrentRow.Cells.Item(6).Value = FormatNumber((Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value)) *
                                                          Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(4).Value))) *
                                                          Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(5).Value)) / 100, 2)

        grdPembelian.CurrentRow.Cells.Item(7).Value = FormatNumber((Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value)) *
                                                                       Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(4).Value))) -
                                                                       Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(6).Value)), 2)
        If grdPembelian.CurrentRow.Cells.Item(8).Value = "Y" Then
            grdPembelian.CurrentRow.Cells.Item(9).Value = FormatNumber(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(7).Value) * 10 / 100, 2)
        Else
            grdPembelian.CurrentRow.Cells.Item(9).Value = FormatNumber(0, 2)
        End If
    End Sub
    Private Sub grdPembelian_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles grdPembelian.CellEndEdit
        If e.ColumnIndex = 2 Then

            If Val(grdPembelian.CurrentRow.Cells.Item(2).Value) = 0 Then
                grdPembelian.CancelEdit()
                grdPembelian.CurrentRow.Cells.Item(2).Value = Val(nQtyAwal)
            End If

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_log_pembelian"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = txtNomorFaktur.Text.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                    .Parameters.Add(New SqlParameter("@reason", SqlDbType.NChar, 15)).Value = "edit item"
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = grdPembelian.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(4).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty", SqlDbType.Decimal)).Value = Val(nQtyAwal)
                    .Parameters.Add(New SqlParameter("@qty_to", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value.ToString.Trim))
                    .ExecuteNonQuery()

                End With
            End Using
            Conn.Close()
            HitungSubTotal()

        ElseIf e.ColumnIndex = 4 Then
            grdPembelian.CurrentRow.Cells.Item(4).Value = FormatNumber(grdPembelian.CurrentRow.Cells.Item(4).Value, 2)
            HitungSubTotal()
        ElseIf e.ColumnIndex = 5 Then
            'grdPembelian.CurrentRow.Cells.Item(6).Value = FormatNumber((Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value)) *
            '                                              Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(4).Value))) *
            '                                              Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(5).Value)) / 100, 2)
            HitungSubTotal()
        ElseIf e.ColumnIndex = 6 Then
            grdPembelian.CurrentRow.Cells.Item(6).Value = FormatNumber(grdPembelian.CurrentRow.Cells.Item(6).Value, 2)
            HitungSubTotal()
        ElseIf e.ColumnIndex = 8 Then
            grdPembelian.CurrentRow.Cells.Item(8).Value = UCase(grdPembelian.CurrentRow.Cells.Item(8).Value)
            HitungSubTotal()
        End If
        HitungTotal()
    End Sub

    Private Sub CancelItem()
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_log_pembelian"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = txtNomorFaktur.Text.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                    .Parameters.Add(New SqlParameter("@reason", SqlDbType.NChar, 15)).Value = "remove item"
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = grdPembelian.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(4).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty_to", SqlDbType.Decimal)).Value = 0
                    .ExecuteNonQuery()

                End With
            End Using
            Conn.Close()

            grdPembelian.Rows.RemoveAt(grdPembelian.CurrentCell.RowIndex)
            HitungTotal()
            txtNamaBarang.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnHapusBaris_Click(sender As Object, e As EventArgs) Handles btnHapusBaris.Click
        Try
            CancelItem()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub HitungTotal()
        Try
            Dim nSubTotal As Decimal = 0
            Dim nSubPajak As Decimal = 0
            For Each datarows In grdPembelian.Rows
                nSubTotal = Val(nSubTotal) + Val(Decimal.Parse(datarows.Cells.Item(7).Value.ToString.Trim))
                nSubPajak = Val(nSubPajak) + Val(Decimal.Parse(datarows.Cells.Item(9).Value.ToString.Trim))
            Next

            txtTotalPajak.Text = Val(nSubPajak)
            txtTotalSetelahPajak.Text = Val(Decimal.Parse(txtBiayaLain.Text.Trim)) + Val(nSubTotal) + Val(nSubPajak)

            If chkInvoice.Checked = True Then
                txtSaldoTerhutang.Text = Val(Decimal.Parse(txtTotalSetelahPajak.Text.Trim)) - Val(Decimal.Parse(txtDibayarDimuka.Text.Trim))
            ElseIf chkTunai.Checked = True Then
                txtDibayarDimuka.Text = Val(Decimal.Parse(txtBiayaLain.Text.Trim)) + Val(nSubTotal) + Val(nSubPajak)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdPembelian_Click(sender As Object, e As EventArgs) Handles grdPembelian.Click

    End Sub

    Private Sub txtBiayaLain_TextChanged(sender As Object, e As EventArgs) Handles txtBiayaLain.TextChanged
        Try
            HitungTotal()
        Catch ex As Exception

        End Try
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
            MsgBox("Anda belum memasukkan data suplier", MsgBoxStyle.Critical, "Error")
            txtNamaSuplier.Focus()
            Exit Sub
        End If

        If txtNomorFaktur.Text.Trim = "" Then
            MsgBox("Anda belum memasukkan nomor faktur", MsgBoxStyle.Critical, "Error")
            txtNomorFaktur.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtNomorFaktur.Text.Trim,
                                "select [nomor faktur] from pembelian " &
                                "where [nomor faktur] = '" & txtNomorFaktur.Text.Trim & "'") Then

            MsgBox("nomor faktur tersebut sudah tersimpan dalam database", MsgBoxStyle.Critical, "Error")
            txtNomorFaktur.Focus()
            Exit Sub
        End If

        If grdPembelian.RowCount = 0 Then
            MsgBox("Tidak ada item obat pada grid pembelian", MsgBoxStyle.Critical, "Error")
            txtNamaBarang.Focus()
            Exit Sub
        End If


        'simpan datanya disini
        'malam adalah pendengar yang baik.
        'sebab pikiran dan hati bisa duduk 
        'bersama dan saling bercerita

        Dim cJenis As String = ""
        If chkInvoice.Checked = True Then
            cJenis = "K"
        ElseIf chkTunai.Checked = True Then
            cJenis = "T"
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_pembelian"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = txtNomorFaktur.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 5)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@tgl_faktur", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtTglFaktur)
                    .Parameters.Add(New SqlParameter("@jenis", SqlDbType.NChar, 1)).Value = cJenis.Trim
                    .Parameters.Add(New SqlParameter("@tgl_terima", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtTerima)
                    .Parameters.Add(New SqlParameter("@tgl_jatuh_tempo", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtJatuhTempo)
                    .Parameters.Add(New SqlParameter("@biaya_lain", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtBiayaLain.Text.Trim))
                    .Parameters.Add(New SqlParameter("@total_pajak", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtTotalPajak.Text.Trim))
                    .Parameters.Add(New SqlParameter("@uang_muka", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtDibayarDimuka.Text.Trim))
                    .Parameters.Add(New SqlParameter("@saldo_terhutang", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtSaldoTerhutang.Text.Trim))
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim


                    '/// parameter structured \\\

                    'stok obat
                    Dim dtPembelianDetail As New DataTable()
                    dtPembelianDetail.Columns.Add("nomor_faktur", GetType([String])).MaxLength = 15
                    dtPembelianDetail.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtPembelianDetail.Columns.Add("qty", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("satuan", GetType([String])).MaxLength = 10
                    dtPembelianDetail.Columns.Add("harga_pokok_pembelian", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("discount", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("sub_total", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("pajak", GetType([String])).MaxLength = 1
                    dtPembelianDetail.Columns.Add("besar_pajak", GetType(Decimal))
                    'dtPembelianDetail.Columns.Add("exp_date", GetType([String])).MaxLength = 10
                    'dtPembelianDetail.Columns.Add("batch_no", GetType([String])).MaxLength = 15


                    For Each DataRows In grdPembelian.Rows
                        dtPembelianDetail.Rows.Add(New Object() {txtNomorFaktur.Text.Trim,
                                                                 DataRows.Cells.Item(0).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(2).Value.ToString.Trim)),
                                                                 DataRows.Cells.Item(3).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(4).Value.ToString.Trim)),
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(6).Value.ToString.Trim)),
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(7).Value.ToString.Trim)),
                                                                 DataRows.Cells.Item(8).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(9).Value.ToString.Trim))})
                    Next

                    .Parameters.Add("@PembelianDetailTVP", SqlDbType.Structured).Value = dtPembelianDetail
                    .ExecuteNonQuery()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        'update harga terbaru
        For Each datarows In grdPembelian.Rows
            HargaTerbaru(datarows.Cells.Item(0).Value.ToString.Trim,
                         Val(Decimal.Parse(datarows.Cells.Item(4).Value.ToString.Trim)))
            Wait(0.5)
        Next

        xForm.Dispose()

        MsgBox("Data berhasil disimpan")
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeSuplier.Text.Trim = "" Then
            MsgBox("Anda belum memasukkan data suplier", MsgBoxStyle.Critical, "Error")
            txtNamaSuplier.Focus()
            Exit Sub
        End If

        If txtNomorFaktur.Text.Trim = "" Then
            MsgBox("Anda belum memasukkan nomor faktur", MsgBoxStyle.Critical, "Error")
            txtNomorFaktur.Focus()
            Exit Sub
        End If

        If grdPembelian.RowCount = 0 Then
            MsgBox("Tidak ada item obat pada grid pembelian", MsgBoxStyle.Critical, "Error")
            txtNamaBarang.Focus()
            Exit Sub
        End If


        'simpan datanya disini
        'malam adalah pendengar yang baik.
        'sebab pikiran dan hati bisa duduk 
        'bersama dan saling bercerita

        Dim cJenis As String = ""
        If chkInvoice.Checked = True Then
            cJenis = "K"
        ElseIf chkTunai.Checked = True Then
            cJenis = "T"
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_pembelian"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@old_nomor_faktur", SqlDbType.NChar, 15)).Value = cNomorFaktur.Trim
                    .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = txtNomorFaktur.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 5)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@tgl_faktur", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtTglFaktur)
                    .Parameters.Add(New SqlParameter("@jenis", SqlDbType.NChar, 1)).Value = cJenis.Trim
                    .Parameters.Add(New SqlParameter("@tgl_terima", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtTerima)
                    .Parameters.Add(New SqlParameter("@tgl_jatuh_tempo", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtJatuhTempo)
                    .Parameters.Add(New SqlParameter("@biaya_lain", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtBiayaLain.Text.Trim))
                    .Parameters.Add(New SqlParameter("@total_pajak", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtTotalPajak.Text.Trim))
                    .Parameters.Add(New SqlParameter("@uang_muka", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtDibayarDimuka.Text.Trim))
                    .Parameters.Add(New SqlParameter("@saldo_terhutang", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtSaldoTerhutang.Text.Trim))
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim


                    '/// parameter structured \\\

                    'stok obat
                    Dim dtPembelianDetail As New DataTable()
                    dtPembelianDetail.Columns.Add("nomor_faktur", GetType([String])).MaxLength = 15
                    dtPembelianDetail.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtPembelianDetail.Columns.Add("qty", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("satuan", GetType([String])).MaxLength = 10
                    dtPembelianDetail.Columns.Add("harga_pokok_pembelian", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("discount", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("sub_total", GetType(Decimal))
                    dtPembelianDetail.Columns.Add("pajak", GetType([String])).MaxLength = 1
                    dtPembelianDetail.Columns.Add("besar_pajak", GetType(Decimal))
                    'dtPembelianDetail.Columns.Add("exp_date", GetType([String])).MaxLength = 10
                    'dtPembelianDetail.Columns.Add("batch_no", GetType([String])).MaxLength = 15


                    For Each DataRows In grdPembelian.Rows
                        dtPembelianDetail.Rows.Add(New Object() {txtNomorFaktur.Text.Trim,
                                                                 DataRows.Cells.Item(0).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(2).Value.ToString.Trim)),
                                                                 DataRows.Cells.Item(3).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(4).Value.ToString.Trim)),
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(6).Value.ToString.Trim)),
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(7).Value.ToString.Trim)),
                                                                 DataRows.Cells.Item(8).Value.ToString.Trim,
                                                                 Val(Decimal.Parse(DataRows.Cells.Item(9).Value.ToString.Trim))})
                    Next

                    .Parameters.Add("@PembelianDetailTVP", SqlDbType.Structured).Value = dtPembelianDetail
                    .ExecuteNonQuery()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        'update harga terbaru
        For Each datarows In grdPembelian.Rows
            HargaTerbaru(datarows.Cells.Item(0).Value.ToString.Trim,
                         Val(Decimal.Parse(datarows.Cells.Item(4).Value.ToString.Trim)))
            Wait(0.5)
        Next

        xForm.Dispose()

        MsgBox("Data berhasil diupdate")
        DefaultSetting()
    End Sub

    Private Sub Wait(ByVal seconds As Integer)
        For i As Integer = 0 To seconds * 10
            System.Threading.Thread.Sleep(5)
            Application.DoEvents()
        Next
    End Sub

    Private Function HargaTerbaru(cKodeBarang As String, nHargaPokokPembelian As Decimal) As Boolean

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "select [harga pokok pembelian],[kena pajak] from barang where [kode barang] = '" & cKodeBarang.Trim & "'"

                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    If Val(Decimal.Parse(rDr.Item(0).ToString.Trim)) <> Val(Decimal.Parse(nHargaPokokPembelian)) Then
                        UpdateHargaBarang(cKodeBarang.Trim,
                                        Decimal.Parse(nHargaPokokPembelian),
                                        rDr.Item(1).ToString.Trim)
                    End If
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()

        Return True
    End Function

    Private Function UpdateHargaBarang(ByVal cKodeBarang As String,
                                     ByVal nHargaPokokPembelian As Decimal,
                                     ByVal cKenaPajak As String) As Boolean

        Dim nHargaJualToko As Decimal = 0
        Dim nHargaJualGrosir As Decimal = 0
        Dim nHargaMarkup As Decimal = 0
        Dim nPajak As Decimal = 0
        Dim nHargaPokokPenjualan As Decimal = 0

        If cKenaPajak.Trim = "Y" Then
            nPajak = Val(Decimal.Parse(nHargaPokokPembelian) * 10 / 100)
        Else
            nPajak = 0
        End If

        nHargaMarkup = Val(Decimal.Parse(nHargaPokokPembelian)) + (Val(Decimal.Parse(nHargaPokokPembelian)) * 10 / 100)
        nHargaPokokPenjualan = Val(Decimal.Parse(nHargaPokokPembelian)) + Val(Decimal.Parse(nPajak))

        nHargaJualToko = Val(Decimal.Parse(nHargaMarkup)) + (Val(Decimal.Parse(nHargaMarkup)) * Val(nPersenHargaToko) / 100)
        nHargaJualToko = Val(Decimal.Parse(nHargaMarkup)) + (Val(Decimal.Parse(nHargaMarkup)) * Val(nPersenHargaGrosir) / 100)





        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_harga_barang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = cKodeBarang.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(nHargaPokokPembelian))
                    .Parameters.Add(New SqlParameter("@kena_pajak", SqlDbType.NChar, 1)).Value = cKenaPajak.Trim
                    .Parameters.Add(New SqlParameter("@ppn_masuk", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(nPajak))
                    .Parameters.Add(New SqlParameter("@harga_pokok_penjualan", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(nHargaPokokPenjualan))
                    .Parameters.Add(New SqlParameter("@harga_jual_toko", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(nHargaJualToko))
                    .Parameters.Add(New SqlParameter("@harga_jual_grosir", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(nHargaJualGrosir))
                    .ExecuteNonQuery()

                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try

        Return True
    End Function

    Private Sub chkInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoice.CheckedChanged
        If chkInvoice.Checked = True Then
            chkTunai.Checked = False
        End If
    End Sub

    Private Sub chkTunai_CheckedChanged(sender As Object, e As EventArgs) Handles chkTunai.CheckedChanged
        If chkTunai.Checked = True Then
            chkInvoice.Checked = False
        End If
    End Sub

    Private Sub txtDibayarDimuka_EditValueChanged(sender As Object, e As EventArgs) Handles txtDibayarDimuka.EditValueChanged
        Try
            If chkInvoice.Checked = True Then
                txtSaldoTerhutang.Text = Val(Decimal.Parse(txtTotalSetelahPajak.Text.Trim)) - Val(Decimal.Parse(txtDibayarDimuka.Text.Trim))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSaldoTerhutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSaldoTerhutang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnSimpan.Focus()
        End If
    End Sub

    Private Sub frmPembelian_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnBatal.PerformClick()
        ElseIf e.KeyCode = Keys.F3 Then
            EditItem()
        ElseIf e.KeyCode = Keys.F4 Then
            CancelItem()
        ElseIf e.KeyCode = Keys.F5 Then
            btnSimpan.PerformClick()
        End If
    End Sub

    Private Sub EditItem()
        Try
            nQtyAwal = 0
            nQtyAwal = Val(grdPembelian.CurrentRow.Cells.Item(2).Value)
            grdPembelian.CurrentRow.Cells.Item(2).BeginEdit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPembelian_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles grdPembelian.CellBeginEdit
        If e.ColumnIndex = 2 Then
            nQtyAwal = 0
            nQtyAwal = Val(Decimal.Parse(grdPembelian.CurrentRow.Cells.Item(2).Value))
            grdPembelian.CurrentRow.Cells.Item(2).BeginEdit()
        End If
    End Sub

    Private Sub frmPembelian_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnKoreksi_Click(sender As Object, e As EventArgs) Handles btnKoreksi.Click
        If clsPublic.CekAkses("pembelian_koreksi", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        Using cfrmListPembelian As New frmListPembelian
            cfrmListPembelian.nUbah = False
            cfrmListPembelian.ShowDialog()
            If cCrudAction.Trim = "crud-edit" Then
                lNew = False
                ClearHeader()
                ClearDetail()
                ClearFooter()
                EnableText()
                grdPembelian.Rows.Clear()
                EditData()
            End If
        End Using
    End Sub

    Private Sub EditData()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn

                .CommandText = "select [nomor faktur],[kode suplier]," &
                               "[tgl faktur],[jenis],[tgl terima]," &
                               "[tgl jatuh tempo],[biaya lain],[total pajak]," &
                               "[uang muka],[saldo terhutang] " &
                               "from pembelian where [nomor faktur] = '" & cNomorFaktur.Trim & "'"
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    txtNomorFaktur.Text = rDr.Item(0).ToString.Trim
                    txtKodeSuplier.Text = rDr.Item(1).ToString.Trim
                    dtTglFaktur.DateTime = rDr.Item(2).ToString.Trim

                    If rDr.Item(3).ToString.Trim = "Y" Then
                        chkTunai.Checked = True
                    ElseIf rDr.Item(3).ToString.Trim = "T" Then
                        chkInvoice.Checked = True
                    End If
                    dtTerima.DateTime = rDr.Item(4).ToString.Trim
                    dtJatuhTempo.DateTime = rDr.Item(5).ToString.Trim
                    txtBiayaLain.Text = rDr.Item(6).ToString.Trim
                    txtTotalPajak.Text = rDr.Item(7).ToString.Trim
                    txtDibayarDimuka.Text = rDr.Item(8).ToString.Trim
                    txtSaldoTerhutang.Text = rDr.Item(9).ToString.Trim
                End While
                rDr.Close()

                .CommandText = "select [nama suplier] from suplier where [kode suplier] = '" &
                               txtKodeSuplier.Text.Trim & "'"
                rDr = .ExecuteReader
                While rDr.Read
                    txtNamaSuplier.Text = rDr.Item(0).ToString.Trim
                End While
                rDr.Close()

                .CommandText = "SELECT pembelian_detail.[kode barang]," &
                               "barang.[nama barang],pembelian_detail.[qty]," &
                               "pembelian_detail.[satuan],pembelian_detail.[harga pokok pembelian]," &
                               "pembelian_detail.[discount],pembelian_detail.[sub total]," &
                               "pembelian_detail.[pajak],pembelian_detail.[besar pajak] " &
                               "FROM pembelian_detail pembelian_detail " &
                               "INNER JOIN barang barang ON pembelian_detail.[kode barang] = barang.[kode barang] " &
                               "WHERE pembelian_detail.[nomor faktur] ='" & cNomorFaktur.Trim & "'"
                rDr = .ExecuteReader
                While rDr.Read
                    grdPembelian.Rows.Add(New Object() {
                                          rDr.Item(0).ToString.Trim,
                                          rDr.Item(1).ToString.Trim,
                                          FormatNumber(Val(rDr.Item(2).ToString.Trim), 0),
                                          rDr.Item(3).ToString.Trim,
                                          FormatNumber(Val(rDr.Item(4).ToString.Trim), 2), 0,
                                          FormatNumber(Val(rDr.Item(5).ToString.Trim), 2),
                                          FormatNumber(Val(rDr.Item(6).ToString.Trim), 2),
                                          rDr.Item(7).ToString.Trim,
                                          FormatNumber(Val(rDr.Item(8).ToString.Trim), 2)
                                          })
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
    End Sub
End Class
