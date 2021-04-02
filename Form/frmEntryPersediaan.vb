Imports System.Data.SqlClient
Public Class frmEntryPersediaan
    Friend lNew As Boolean
    Friend cUserId As String
    Friend cKodeObat As String

    Dim nPersenBebas As Decimal = 0
    Dim nPersenResep As Decimal = 0

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub frmEntryPersediaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmMenuUtama
        Me.WindowState = FormWindowState.Maximized

        DisableText()
        LoadDataGudang()
        If lNew Then
            ClearScreen()
            EnableText()
            txtIsi.Text = 0
            txtStokMinimum.Text = 0
            txtKodeBarang.Focus()
        ElseIf lNew = False Then
            EnableText()
            txtKodeBarang.Text = cKodeObat.Trim
            txtKodeBarang.Properties.ReadOnly = True
            ShowDataBarang()
            txtKodeBarang.Focus()
        End If

        With cboKenaPajak
            .Properties.Items.Add("Y")
            .Properties.Items.Add("T")
        End With
    End Sub

    Private Sub ShowDataBarang()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [nama barang],[kode kategori],[kode sub kategori]," &
                                   "[kode produsen],[kode suplier],[bentuk sediaan]," &
                                   "[satuan terbesar],[isi],[satuan terkecil]," &
                                   "[harga pokok pembelian],[ppn masuk],[harga pokok penjualan]," &
                                   "[harga jual toko],[harga jual grosir]," &
                                   "[kena pajak],[stok minimum],[plu] from barang where [kode barang] = '" &
                                   txtKodeBarang.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        txtNamaBarang.Text = rDr.Item(0).ToString.Trim
                        txtKodeKategori.Text = rDr.Item(1).ToString.Trim
                        txtKodeSubKategori.Text = rDr.Item(2).ToString.Trim
                        txtKodeProdusen.Text = rDr.Item(3).ToString.Trim
                        txtKodeSuplier.Text = rDr.Item(4).ToString.Trim
                        cboSediaan.Text = rDr.Item(5).ToString.Trim
                        cboSatuanTerbesar.Text = rDr.Item(6).ToString.Trim
                        txtIsi.Text = Decimal.Parse(rDr.Item(7).ToString.Trim)
                        cboSatuanTerkecil.Text = rDr.Item(8).ToString.Trim
                        txtHargaPembelian.Text = Decimal.Parse(rDr.Item(9).ToString.Trim)
                        txtPPNMasuk.Text = rDr.Item(10).ToString.Trim
                        txtHargaPokokPenjualan.Text = Decimal.Parse(rDr.Item(11).ToString.Trim)
                        txtHargaJualToko.Text = Decimal.Parse(rDr.Item(12).ToString.Trim)
                        txtHargaJualGrosir.Text = Decimal.Parse(rDr.Item(13).ToString.Trim)
                        If rDr.Item(14).ToString.Trim = "Y" Then
                            cboKenaPajak.Text = "Y"
                        Else
                            cboKenaPajak.Text = "T"
                        End If
                        txtStokMinimum.Text = Val(Decimal.Parse(rDr.Item(15).ToString.Trim))
                        txtPLU.Text = rDr.Item(16).ToString.Trim
                    End While

                    rDr.Close()

                    .CommandText = "SELECT barang_gudang.[nama gudang]," &
                                   "barang_stok_awal.[kode gudang]," &
                                   "barang_stok_awal.[stok awal] " &
                                   "FROM barang_stok_awal barang_stok_awal " &
                                   "INNER JOIN barang_gudang barang_gudang " &
                                   "ON barang_stok_awal.[kode gudang] = barang_gudang.[kode gudang] " &
                                   "WHERE barang_stok_awal.[kode barang] = '" & txtKodeBarang.Text.Trim & "'"

                    grdStokAwal.Rows.Clear()

                    rDr = .ExecuteReader
                    While rDr.Read
                        grdStokAwal.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                           rDr.Item(1).ToString.Trim,
                                                           Val(Decimal.Parse(rDr.Item(2).ToString.Trim))})
                    End While
                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        txtKategori.Text = LoadKategori(txtKodeKategori.Text.Trim).Trim
        txtSubKategori.Text = LoadSubKategori(txtKodeSubKategori.Text.Trim).Trim
        txtProdusen.Text = LoadProdusen(txtKodeProdusen.Text.Trim).Trim
        txtSuplier.Text = LoadSuplier(txtKodeSuplier.Text.Trim).Trim
    End Sub

    Private Sub ClearScreen()
        txtKodeBarang.Text = ""
        txtPLU.Text = ""
        txtNamaBarang.Text = ""
        txtKategori.Text = ""
        txtKodeKategori.Text = ""
        txtSubKategori.Text = ""
        txtKodeSubKategori.Text = ""
        txtProdusen.Text = ""
        txtKodeProdusen.Text = ""
        txtSuplier.Text = ""
        txtKodeSuplier.Text = ""
        cboSediaan.Text = ""
        cboSatuanTerbesar.Text = ""
        txtIsi.Text = ""
        cboSatuanTerkecil.Text = ""
        txtHargaPembelian.Text = ""
        txtPPNMasuk.Text = ""
        txtHargaPokokPenjualan.Text = ""
        txtHargaJualToko.Text = ""
        txtHargaJualGrosir.Text = ""
        cboKenaPajak.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeBarang.Properties.ReadOnly = True
        txtPLU.Properties.ReadOnly = True
        txtNamaBarang.Properties.ReadOnly = True
        txtKategori.Properties.ReadOnly = True
        txtKodeKategori.Properties.ReadOnly = True
        txtSubKategori.Properties.ReadOnly = True
        txtKodeSubKategori.Properties.ReadOnly = True
        txtProdusen.Properties.ReadOnly = True
        txtKodeProdusen.Properties.ReadOnly = True
        txtSuplier.Properties.ReadOnly = True
        txtKodeSuplier.Properties.ReadOnly = True
        cboSediaan.Properties.ReadOnly = True
        cboSatuanTerbesar.Properties.ReadOnly = True
        txtIsi.Properties.ReadOnly = True
        cboSatuanTerkecil.Properties.ReadOnly = True
        txtHargaPembelian.Properties.ReadOnly = True
        txtPPNMasuk.Properties.ReadOnly = True
        txtHargaPokokPenjualan.Properties.ReadOnly = True
        txtHargaJualToko.Properties.ReadOnly = True
        txtHargaJualGrosir.Properties.ReadOnly = True
        cboKenaPajak.Properties.ReadOnly = True
        txtStokMinimum.Properties.ReadOnly = True
        cboGudang.Properties.ReadOnly = True
        txtKodeGudang.Properties.ReadOnly = True
        txtStokAwal.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeBarang.Properties.ReadOnly = False
        txtPLU.Properties.ReadOnly = False
        txtNamaBarang.Properties.ReadOnly = False
        txtKategori.Properties.ReadOnly = False
        'txtKodeKategori.Properties.ReadOnly = False
        txtSubKategori.Properties.ReadOnly = False
        'txtKodeSubKategori.Properties.ReadOnly = False
        txtProdusen.Properties.ReadOnly = False
        'txtKodeProdusen.Properties.ReadOnly = False
        txtSuplier.Properties.ReadOnly = False
        'txtKodeSuplier.Properties.ReadOnly = False
        cboSediaan.Properties.ReadOnly = False
        cboSatuanTerbesar.Properties.ReadOnly = False
        txtIsi.Properties.ReadOnly = False
        cboSatuanTerkecil.Properties.ReadOnly = False
        txtHargaPembelian.Properties.ReadOnly = False
        txtPPNMasuk.Properties.ReadOnly = False
        txtHargaPokokPenjualan.Properties.ReadOnly = False
        txtHargaJualToko.Properties.ReadOnly = False
        txtHargaJualGrosir.Properties.ReadOnly = False
        txtStokMinimum.Properties.ReadOnly = False
        cboKenaPajak.Properties.ReadOnly = False
        cboGudang.Properties.ReadOnly = False
        'txtKodeGudang.Properties.ReadOnly = False
        txtStokAwal.Properties.ReadOnly = False
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()

        If clsPublic.VerifyData(txtKodeBarang.Text.Trim, "select [kode barang] from barang where [kode barang] = '" & txtKodeBarang.Text.Trim & "'") Then
            MsgBox("Data barang dengan barcode tersebut sudah ada", MsgBoxStyle.Critical, "Masukkan barcode yang lain")
            txtKodeBarang.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtPLU.Text.Trim, "select [plu] from barang where [plu] = '" & txtPLU.Text.Trim & "'") Then
            MsgBox("Data barang dengan kode tersebut sudah ada", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtPLU.Focus()
            Exit Sub
        End If

        If txtKodeBarang.Text.Trim = "" Then
            MsgBox("Kode obat tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtKodeBarang.Focus()
            Exit Sub
        End If

        If txtNamaBarang.Text.Trim = "" Then
            MsgBox("Nama obat tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtNamaBarang.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_barang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = txtKodeBarang.Text.Trim
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 7)).Value = txtPLU.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_barang ", SqlDbType.NChar, 50)).Value = txtNamaBarang.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 5)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_sub_kategori", SqlDbType.NChar, 10)).Value = txtKodeSubKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_produsen", SqlDbType.NChar, 5)).Value = txtKodeProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 5)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@bentuk_sediaan", SqlDbType.NChar, 50)).Value = cboSediaan.Text.Trim
                    .Parameters.Add(New SqlParameter("@satuan_terbesar", SqlDbType.NChar, 50)).Value = cboSatuanTerbesar.Text.Trim
                    .Parameters.Add(New SqlParameter("@isi", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtIsi.Text.Trim))
                    .Parameters.Add(New SqlParameter("@satuan_terkecil", SqlDbType.NChar, 50)).Value = cboSatuanTerkecil.Text.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaPembelian.Text.Trim))
                    .Parameters.Add(New SqlParameter("@ppn_masuk", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtPPNMasuk.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_pokok_penjualan", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_jual_toko", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaJualToko.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_jual_grosir", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaJualGrosir.Text.Trim))
                    .Parameters.Add(New SqlParameter("@kena_pajak", SqlDbType.NChar, 1)).Value = cboKenaPajak.Text.Trim
                    .Parameters.Add(New SqlParameter("@stok_minimum", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtStokMinimum.Text.Trim))

                    '/// parameter structured \\\
                    Dim cTanggal As String = Now.Year.ToString.Trim & "-" & Now.Month.ToString.Trim & "-" & Now.Day.ToString.Trim

                    'stok obat
                    Dim dtDataBarangDetail As New DataTable()
                    dtDataBarangDetail.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtDataBarangDetail.Columns.Add("plu", GetType([String])).MaxLength = 7
                    dtDataBarangDetail.Columns.Add("kode_gudang", GetType([String])).MaxLength = 3
                    dtDataBarangDetail.Columns.Add("stok_awal", GetType(Decimal))
                    dtDataBarangDetail.Columns.Add("tgl_stok", GetType([String])).MaxLength = 10
                    dtDataBarangDetail.Columns.Add("user_id", GetType([String])).MaxLength = 15

                    For Each DataRows In grdStokAwal.Rows
                        dtDataBarangDetail.Rows.Add(New Object() {txtKodeBarang.Text.Trim,
                            txtPLU.Text.Trim,
                            DataRows.Cells.Item(1).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRows.Cells.Item(1).Value.ToString.Trim)),
                            cTanggal.Trim,
                            cUserId.Trim})
                    Next
                    .Parameters.Add("@Barang_Stok_TVP", SqlDbType.Structured).Value = dtDataBarangDetail
                    .ExecuteNonQuery()


                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("Data berhasil disimpan")
        ClearScreen()
        txtKodeBarang.Focus()
        grdStokAwal.Rows.Clear()
    End Sub

    Private Sub UpdateData()
        If txtKodeBarang.Text.Trim = "" Then
            MsgBox("Kode barang tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtKodeBarang.Focus()
            Exit Sub
        End If

        If txtNamaBarang.Text.Trim = "" Then
            MsgBox("Nama barang tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtNamaBarang.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "update_barang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = txtKodeBarang.Text.Trim
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 7)).Value = txtPLU.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_barang", SqlDbType.NChar, 50)).Value = txtNamaBarang.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_kategori", SqlDbType.NChar, 3)).Value = txtKodeKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_sub_kategori", SqlDbType.NChar, 6)).Value = txtKodeSubKategori.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_produsen", SqlDbType.NChar, 3)).Value = txtKodeProdusen.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 3)).Value = txtKodeSuplier.Text.Trim
                    .Parameters.Add(New SqlParameter("@bentuk_sediaan", SqlDbType.NChar, 50)).Value = cboSediaan.Text.Trim
                    .Parameters.Add(New SqlParameter("@satuan_terbesar", SqlDbType.NChar, 50)).Value = cboSatuanTerbesar.Text.Trim
                    .Parameters.Add(New SqlParameter("@isi", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtIsi.Text.Trim))
                    .Parameters.Add(New SqlParameter("@satuan_terkecil", SqlDbType.NChar, 50)).Value = cboSatuanTerkecil.Text.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaPembelian.Text.Trim))
                    .Parameters.Add(New SqlParameter("@ppn_masuk", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtPPNMasuk.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_pokok_penjualan", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_jual_toko", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaJualToko.Text.Trim))
                    .Parameters.Add(New SqlParameter("@harga_jual_grosir", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtHargaJualGrosir.Text.Trim))
                    .Parameters.Add(New SqlParameter("@kena_pajak", SqlDbType.NChar, 1)).Value = cboKenaPajak.Text.Trim
                    .Parameters.Add(New SqlParameter("@stok_minimum", SqlDbType.Decimal, 18, 2)).Value = Val(Decimal.Parse(txtStokMinimum.Text.Trim))

                    '/// parameter structured \\\
                    Dim cTanggal As String = Now.Year.ToString.Trim & "-" & Now.Month.ToString.Trim & "-" & Now.Day.ToString.Trim

                    'stok obat
                    Dim dtDataBarangDetail As New DataTable()
                    dtDataBarangDetail.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtDataBarangDetail.Columns.Add("plu", GetType([String])).MaxLength = 7
                    dtDataBarangDetail.Columns.Add("kode_gudang", GetType([String])).MaxLength = 3
                    dtDataBarangDetail.Columns.Add("stok_awal", GetType(Decimal))
                    dtDataBarangDetail.Columns.Add("tgl_stok", GetType([String])).MaxLength = 10
                    dtDataBarangDetail.Columns.Add("user_id", GetType([String])).MaxLength = 15

                    For Each DataRows In grdStokAwal.Rows
                        dtDataBarangDetail.Rows.Add(New Object() {txtKodeBarang.Text.Trim,
                            txtPLU.Text.Trim,
                            DataRows.Cells.Item(1).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRows.Cells.Item(2).Value.ToString.Trim)),
                            cTanggal.Trim,
                            cUserId.Trim})
                    Next

                    .Parameters.Add("@Barang_Stok_TVP", SqlDbType.Structured).Value = dtDataBarangDetail
                    .ExecuteNonQuery()


                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("Data berhasil disimpan")

    End Sub

    Private Sub txtKodeKategori_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeKategori.ButtonClick
        frmKategori.ShowDialog()
    End Sub

    Private Sub txtKodeSubKategori_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeSubKategori.ButtonClick
        frmSubKategori.ShowDialog()
    End Sub

    Private Sub txtKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKategori.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCarikategori As New frmCariKategori
                Try
                    cfrmCarikategori.ShowDialog()
                    txtKodeKategori.Text = cfrmCarikategori.grdKategori.CurrentRow.Cells.Item(1).Value.ToString.Trim
                    txtKategori.Text = cfrmCarikategori.grdKategori.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtSubKategori.Focus()
                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            grdStokAwal.Rows.Add(New Object() {cboGudang.Text.Trim, txtKodeGudang.Text.Trim, Val(txtStokAwal.Text.Trim)})

            cboGudang.Text = ""
            txtKodeGudang.Text = ""
            txtStokAwal.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdStokAwal_Click(sender As Object, e As EventArgs) Handles grdStokAwal.Click
        Try
            cboGudang.Text = grdStokAwal.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtKodeGudang.Text = grdStokAwal.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtStokAwal.Text = grdStokAwal.CurrentRow.Cells.Item(2).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            grdStokAwal.Rows.RemoveAt(grdStokAwal.CurrentCell.RowIndex)
            cboGudang.Text = ""
            txtKodeGudang.Text = ""
            txtStokAwal.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSubKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSubKategori.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCarisubkategori As New frmCariSubKategori
                Try
                    cfrmCarisubkategori.cKodeKategori = txtKodeKategori.Text.Trim
                    cfrmCarisubkategori.ShowDialog()
                    txtKodeSubKategori.Text = cfrmCarisubkategori.grdSubKategori.CurrentRow.Cells.Item(1).Value.ToString.Trim
                    txtSubKategori.Text = cfrmCarisubkategori.grdSubKategori.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtProdusen.Focus()
                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub txtKodeProdusen_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeProdusen.ButtonClick
        frmProdusen.ShowDialog()
    End Sub

    Private Sub txtProdusen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtProdusen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariProdusen As New frmCariProdusen
                Try
                    cfrmCariProdusen.ShowDialog()
                    txtProdusen.Text = cfrmCariProdusen.grdCariProdusen.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeProdusen.Text = cfrmCariProdusen.grdCariProdusen.CurrentRow.Cells.Item(1).Value.ToString.Trim
                    txtSuplier.Focus()
                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub
    Private Sub txtKodeSuplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeSuplier.ButtonClick
        frmSuplier.ShowDialog()
    End Sub

    Private Sub txtSuplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSuplier.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariSuplier As New frmCariSuplier
                Try

                    cfrmCariSuplier.ShowDialog()
                    txtSuplier.Text = cfrmCariSuplier.grdCariSuplier.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeSuplier.Text = cfrmCariSuplier.grdCariSuplier.CurrentRow.Cells.Item(1).Value.ToString.Trim
                    cboSediaan.Focus()

                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub cboGudang_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cboGudang.ButtonClick
        If e.Button.Index = 1 Then
            frmGudang.ShowDialog()
            LoadDataGudang()
        End If
    End Sub

    Private Sub LoadDataGudang()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama gudang] from barang_gudang order by [kode gudang] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    cboGudang.Properties.Items.Clear()

                    While rDr.Read
                        cboGudang.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()

                    .CommandText = "select [persen harga toko],[persen harga grosir] from barang_pengaturan_markup"
                    rDr = .ExecuteReader
                    While rDr.Read
                        nPersenBebas = Val(rDr.Item(0).ToString.Trim)
                        nPersenResep = Val(rDr.Item(1).ToString.Trim)
                    End While
                    rDr.Close()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
    End Sub

    Private Sub cboGudang_EditValueChanged(sender As Object, e As EventArgs) Handles cboGudang.EditValueChanged
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [kode gudang] from barang_gudang " &
                                   "where [nama gudang]  = '" & cboGudang.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtKodeGudang.Text = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
    End Sub

    Private Sub frmEntryPersediaan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
        If lNew Then
            txtKodeBarang.Focus()
        Else
            txtNamaBarang.Focus()
        End If
    End Sub

    Private Sub txtHargaPembelian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHargaPembelian.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cboKenaPajak.Text.Trim = "Y" Then
                txtPPNMasuk.Text = Val(Decimal.Parse(txtHargaPembelian.Text.Trim)) * 10 / 100
            Else
                txtPPNMasuk.Text = 0
            End If
            txtHargaPokokPenjualan.Text = Val(Decimal.Parse(txtHargaPembelian.Text.Trim)) + Val(Decimal.Parse(txtPPNMasuk.Text.Trim))
            txtHargaJualToko.Text = Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim)) +
                                     (Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim)) * Val(Decimal.Parse(nPersenBebas) / 100))
            txtHargaJualGrosir.Text = Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim)) +
                                     (Val(Decimal.Parse(txtHargaPokokPenjualan.Text.Trim)) * Val(Decimal.Parse(nPersenResep) / 100))
        End If
    End Sub

    Private Sub txtKodeObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKodeBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPLU.Focus()
        End If
    End Sub

    Private Sub frmEntryPersediaan_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Function LoadKategori(ByVal cKodeKategori As String) As String
        Dim cKategori As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [nama kategori] from kategori " &
                                   "where [kode kategori]  = '" & cKodeKategori.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cKategori = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        Return cKategori.Trim
    End Function

    Private Function LoadSubKategori(ByVal cKodeSubKategori As String) As String
        Dim cSubKategori As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [nama sub kategori] from sub_kategori " &
                                   "where [kode sub kategori]  = '" & cKodeSubKategori.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cSubKategori = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        Return cSubKategori.Trim
    End Function

    Private Function LoadProdusen(ByVal cKodeProdusen As String) As String
        Dim cProdusen As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [nama produsen] from produsen " &
                                   "where [kode produsen]  = '" & cKodeProdusen.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cProdusen = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        Return cProdusen.Trim
    End Function

    Private Function LoadSuplier(ByVal cKodeSuplier As String) As String
        Dim cSuplier As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn

                    .CommandText = "select [nama suplier] from suplier " &
                                   "where [kode suplier]  = '" & cKodeSuplier.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cSuplier = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        Return cSuplier.Trim
    End Function

End Class