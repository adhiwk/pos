Imports System.Data.SqlClient
Public Class ctrlEntryPersediaan
    Dim lNew As Boolean
    Dim lShowData As Boolean
    Dim nPersenBebas As Decimal = 0
    Dim nPersenResep As Decimal = 0
    Dim cOldPlu As String

    Private Sub ctrlEntryPersediaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        LoadData()
        DefaultSetting()
        With cboKenaPajak
            .Properties.Items.Add("Y")
            .Properties.Items.Add("T")
        End With
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        frmMenuUtama.tmMenu.StartTransition(frmMenuUtama.pnlApp)
        Me.Hide()
        frmMenuUtama.tmMenu.EndTransition()
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
                                   "[kena pajak],[stok minimum],[plu],[kode barang] from barang where [plu] = '" &
                                   txtPLU.Text.Trim & "'"

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
                        txtKodeBarang.Text = rDr.Item(17).ToString.Trim
                    End While

                    rDr.Close()

                    .CommandText = "SELECT barang_gudang.[nama gudang]," &
                                   "barang_stok_awal.[kode gudang]," &
                                   "barang_stok_awal.[stok awal] " &
                                   "FROM barang_stok_awal barang_stok_awal " &
                                   "INNER JOIN barang_gudang barang_gudang " &
                                   "ON barang_stok_awal.[kode gudang] = barang_gudang.[kode gudang] " &
                                   "WHERE barang_stok_awal.[plu] = '" & txtPLU.Text.Trim & "'"

                    grdStokAwal.Rows.Clear()

                    rDr = .ExecuteReader
                    While rDr.Read
                        grdStokAwal.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                           rDr.Item(1).ToString.Trim,
                                                           Val(Decimal.Parse(rDr.Item(2).ToString.Trim))})
                    End While
                    rDr.Close()

                    .CommandText = "SELECT [plu],[kode barang]," &
                                   "[nama barang],[satuan terbesar],[isi],[satuan terkecil]," &
                                   "[harga pokok pembelian],[ppn masuk],[harga pokok penjualan]," &
                                   "[harga jual toko],[harga jual grosir] FROM barang WHERE " &
                                   "[sub plu] = '" & txtPLU.Text.Trim & "'"

                    grdSubItem.Rows.Clear()
                    rDr = .ExecuteReader
                    While rDr.Read
                        grdSubItem.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                          rDr.Item(1).ToString.Trim,
                                                          rDr.Item(2).ToString.Trim,
                                                          rDr.Item(3).ToString.Trim,
                                                          Val(Decimal.Parse(rDr.Item(4).ToString.Trim)),
                                                          rDr.Item(5).ToString.Trim,
                                                          FormatNumber(Decimal.Parse(rDr.Item(6).ToString.Trim), 2),
                                                          FormatNumber(Decimal.Parse(rDr.Item(7).ToString.Trim), 2),
                                                          FormatNumber(Decimal.Parse(rDr.Item(8).ToString.Trim), 2),
                                                          FormatNumber(Decimal.Parse(rDr.Item(9).ToString.Trim), 2),
                                                          FormatNumber(Decimal.Parse(rDr.Item(10).ToString.Trim), 2)})
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
        ClearSubItem()
        DisableText()
        btnTambah.Enabled = True
        btnUbah.Enabled = False
        btnBatal.Enabled = False
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        grdStokAwal.Rows.Clear()
        grdSubItem.Rows.Clear()
        lNew = False
        lShowData = False
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
        lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
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

        If txtPLU.Text.Trim = "" Then
            MsgBox("plu tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
            txtPLU.Focus()
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
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 15)).Value = txtPLU.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_barang ", SqlDbType.NChar, 100)).Value = txtNamaBarang.Text.Trim
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
                    .Parameters.Add(New SqlParameter("@sub_item", SqlDbType.NChar, 1)).Value = "T"

                    Dim cTanggal As String = Now.Year.ToString.Trim & "-" & Now.Month.ToString.Trim & "-" & Now.Day.ToString.Trim
                    '----------------------------------------------------------------------------------------------
                    'Dari aku, untukmu yang sempat menjadi KITA. 
                    'terimakasih, untuk waktu dan kenangan yang sama2 kita bagikan. 
                    'untuk cinta yang sempat singgah walau hanya sesaat. 
                    'dan untuk pengorbanan yang telah kita curahkan untuk mempertahankan kata "kita". 
                    'aku yang sekarang mulai mengerti, bukan hilangnya cinta yang memisahkan kita, 
                    'bukan juga sebuah kebosanan, melainkan kita yang sebenarnya bukan untuk masing2. 
                    'kita hanya memaksakan apa yang menurut kita baik, sedangkan takdir mengatakan sebaliknya.
                    'semoga kamu selalu bahagia, dengan siapapun yang akan menemanimu kelak.
                    'biarkan dirimu menemukan "indah" mu pada waktunya, dan aku akan selalu mendoakannya.

                    Dim dtDataBarangDetail As New DataTable()
                    dtDataBarangDetail.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtDataBarangDetail.Columns.Add("plu", GetType([String])).MaxLength = 15
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
                                frmLogin.txtUserId.Text.Trim})
                    Next
                    .Parameters.Add("@Barang_Stok_TVP", SqlDbType.Structured).Value = dtDataBarangDetail

                    Dim dtDataSubItem As New DataTable
                    dtDataSubItem.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtDataSubItem.Columns.Add("plu", GetType([String])).MaxLength = 15
                    dtDataSubItem.Columns.Add("nama_barang", GetType([String])).MaxLength = 100
                    dtDataSubItem.Columns.Add("satuan_terbesar", GetType([String])).MaxLength = 50
                    dtDataSubItem.Columns.Add("isi", GetType(Decimal))
                    dtDataSubItem.Columns.Add("satuan_terkecil", GetType([String])).MaxLength = 50
                    dtDataSubItem.Columns.Add("harga_pokok_pembelian", GetType(Decimal))
                    dtDataSubItem.Columns.Add("ppn_masuk", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_pokok_penjualan", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_jual_toko", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_jual_grosir", GetType(Decimal))
                    dtDataSubItem.Columns.Add("sub_item", GetType([String])).MaxLength = 1
                    dtDataSubItem.Columns.Add("sub_plu", GetType([String])).MaxLength = 15

                    For Each datarows In grdSubItem.Rows
                        dtDataSubItem.Rows.Add(New Object() {datarows.Cells.Item(1).Value.ToString.Trim,
                                               datarows.Cells.Item(0).Value.ToString.Trim,
                                               datarows.Cells.Item(2).Value.ToString.Trim,
                                               datarows.Cells.Item(3).Value.ToString.Trim,
                                               Val(Decimal.Parse(datarows.Cells.Item(4).Value.ToString.Trim)),
                                               datarows.Cells.Item(5).Value.ToString.Trim,
                                               Decimal.Parse(datarows.Cells.Item(6).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(7).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(8).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(9).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(10).Value.ToString.Trim),
                                               "Y",
                                               txtPLU.Text.Trim})
                    Next
                    .Parameters.Add("@Barang_Sub_Item_TVP", SqlDbType.Structured).Value = dtDataSubItem
                    .ExecuteNonQuery()

                    grdBarang.Rows.Add(New Object() {txtNamaBarang.Text.Trim,
                                       txtPLU.Text.Trim,
                                       txtKodeBarang.Text.Trim,
                                       FormatNumber(Decimal.Parse(txtHargaPembelian.Text.Trim), 2),
                                       FormatNumber(Decimal.Parse(txtHargaJualToko.Text.Trim), 2),
                                       FormatNumber(Decimal.Parse(txtHargaJualGrosir.Text.Trim), 2),
                                       FormatNumber(Decimal.Parse(txtIsi.Text.Trim), 2),
                                       cboSatuanTerkecil.Text.Trim,
                                       FormatNumber(clsPublic.CekStok(txtPLU.Text.Trim), 0)
                                       })
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("Data berhasil disimpan")
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtPLU.Text.Trim = "" Then
            MsgBox("plu tidak boleh kosong", MsgBoxStyle.Critical, "Masukkan kode yang lain")
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
                    .Parameters.Add(New SqlParameter("@old_plu", SqlDbType.NChar, 15)).Value = cOldPlu.Trim
                    .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = txtKodeBarang.Text.Trim
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 15)).Value = txtPLU.Text.Trim
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
                    dtDataBarangDetail.Columns.Add("plu", GetType([String])).MaxLength = 15
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
                        frmLogin.txtUserId.Text.Trim})
                    Next

                    .Parameters.Add("@Barang_Stok_TVP", SqlDbType.Structured).Value = dtDataBarangDetail

                    Dim dtDataSubItem As New DataTable
                    dtDataSubItem.Columns.Add("kode_barang", GetType([String])).MaxLength = 25
                    dtDataSubItem.Columns.Add("plu", GetType([String])).MaxLength = 15
                    dtDataSubItem.Columns.Add("nama_barang", GetType([String])).MaxLength = 100
                    dtDataSubItem.Columns.Add("satuan_terbesar", GetType([String])).MaxLength = 50
                    dtDataSubItem.Columns.Add("isi", GetType(Decimal))
                    dtDataSubItem.Columns.Add("satuan_terkecil", GetType([String])).MaxLength = 50
                    dtDataSubItem.Columns.Add("harga_pokok_pembelian", GetType(Decimal))
                    dtDataSubItem.Columns.Add("ppn_masuk", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_pokok_penjualan", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_jual_toko", GetType(Decimal))
                    dtDataSubItem.Columns.Add("harga_jual_grosir", GetType(Decimal))
                    dtDataSubItem.Columns.Add("sub_item", GetType([String])).MaxLength = 1
                    dtDataSubItem.Columns.Add("sub_plu", GetType([String])).MaxLength = 15

                    For Each datarows In grdSubItem.Rows
                        dtDataSubItem.Rows.Add(New Object() {datarows.Cells.Item(1).Value.ToString.Trim,
                                               datarows.Cells.Item(0).Value.ToString.Trim,
                                               datarows.Cells.Item(2).Value.ToString.Trim,
                                               datarows.Cells.Item(3).Value.ToString.Trim,
                                               Val(Decimal.Parse(datarows.Cells.Item(4).Value.ToString.Trim)),
                                               datarows.Cells.Item(5).Value.ToString.Trim,
                                               Decimal.Parse(datarows.Cells.Item(6).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(7).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(8).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(9).Value.ToString.Trim),
                                               Decimal.Parse(datarows.Cells.Item(10).Value.ToString.Trim),
                                               "Y",
                                               txtPLU.Text.Trim})
                    Next
                    .Parameters.Add("@Barang_Sub_Item_TVP", SqlDbType.Structured).Value = dtDataSubItem
                    .ExecuteNonQuery()

                    grdBarang.CurrentRow.Cells.Item(0).Value = txtNamaBarang.Text.Trim
                    grdBarang.CurrentRow.Cells.Item(1).Value = txtPLU.Text.Trim
                    grdBarang.CurrentRow.Cells.Item(2).Value = txtKodeBarang.Text.Trim
                    grdBarang.CurrentRow.Cells.Item(3).Value = FormatNumber(Decimal.Parse(txtHargaPembelian.Text.Trim), 2)
                    grdBarang.CurrentRow.Cells.Item(4).Value = FormatNumber(Decimal.Parse(txtHargaJualToko.Text.Trim), 2)
                    grdBarang.CurrentRow.Cells.Item(5).Value = FormatNumber(Decimal.Parse(txtHargaJualGrosir.Text.Trim), 2)
                    grdBarang.CurrentRow.Cells.Item(6).Value = FormatNumber(Decimal.Parse(txtIsi.Text.Trim), 2)
                    grdBarang.CurrentRow.Cells.Item(7).Value = cboSatuanTerkecil.Text.Trim
                    grdBarang.CurrentRow.Cells.Item(8).Value = FormatNumber(clsPublic.CekStok(txtPLU.Text.Trim), 0)
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("Data berhasil disimpan")
        DefaultSetting()
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
            If txtStokAwal.Text = 0 Then
                MsgBox("Stok Awal tidak boleh kosong", MsgBoxStyle.Critical, "error")
                Exit Sub
            End If
            grdStokAwal.Rows.Add(New Object() {cboGudang.Text.Trim, txtKodeGudang.Text.Trim, FormatNumber(Integer.Parse(txtStokAwal.Text.Trim), 0)})

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


    Private Sub cboGudang_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cboGudang.ButtonClick
        If e.Button.Index = 1 Then
            frmGudang.ShowDialog()

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
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
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


                    'Select [nama barang],[plu],[kode barang],
                    '       [harga pokok pembelian],[harga jual toko],
                    '       [harga jual grosir],[isi],
                    '       [satuan terkecil]
                    'FROM (Select  tbl.*, ROW_NUMBER() OVER (ORDER BY [nama barang]) rownum
                    'From barang As tbl) seq
                    'WHERE seq.rownum BETWEEN 1 And 1000

                    .CommandText = "select [nama barang],[plu],[kode barang]," &
                                   "[harga pokok pembelian],[harga jual toko]," &
                                   "[harga jual grosir],[isi]," &
                                   "[satuan terkecil] from barang where [sub item] = 'T'" &
                                   " order by [nama barang] asc"

                    rDr = .ExecuteReader
                    While rDr.Read
                        grdBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                          rDr.Item(1).ToString.Trim,
                                                          rDr.Item(2).ToString.Trim,
                                                          FormatNumber(rDr.Item(3).ToString.Trim, 0),
                                                          FormatNumber(rDr.Item(4).ToString.Trim, 0),
                                                          FormatNumber(rDr.Item(5).ToString.Trim, 0),
                                                          FormatNumber(rDr.Item(6).ToString.Trim, 0),
                                                          rDr.Item(7).ToString.Trim,
                                                          FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 0)})
                    End While
                    rDr.Close()

                    'Dim da As New SqlDataAdapter(Cmd)
                    'da.Fill(ds, "barang")
                    'If ds.Tables("barang").Rows.Count > 0 Then
                    '    grdBarang.DataSource = ds.Tables("barang")
                    '    GridFormat()
                    'End If
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            lblRecord.Text = "Total Record : " & FormatNumber(grdBarang.Rows.Count, 0)
        End Using
        Conn.Close()
        ds.Dispose()
    End Sub

    Private Sub GridFormat()
        grdBarang.Columns(0).HeaderText = "NAMA BARANG"
        grdBarang.Columns(0).Width = 350
        grdBarang.Columns(1).HeaderText = "PLU"
        grdBarang.Columns(1).Width = 100
        grdBarang.Columns(2).HeaderText = "BARCODE"
        grdBarang.Columns(2).Width = 120
        grdBarang.Columns(3).HeaderText = "HARGA BELI"
        grdBarang.Columns(3).Width = 120
        grdBarang.Columns(3).TextAlignment = ContentAlignment.MiddleRight
        grdBarang.Columns(3).FormatString = "{0:###,###.00}"
        grdBarang.Columns(4).HeaderText = "HARGA JUAL"
        grdBarang.Columns(4).Width = 120
        grdBarang.Columns(4).TextAlignment = ContentAlignment.MiddleRight
        grdBarang.Columns(4).FormatString = "{0:###,###.00}"
        grdBarang.Columns(5).HeaderText = "HARGA GROSIR"
        grdBarang.Columns(5).Width = 120
        grdBarang.Columns(5).TextAlignment = ContentAlignment.MiddleRight
        grdBarang.Columns(5).FormatString = "{0:###,###.00}"
        grdBarang.Columns(6).HeaderText = "ISI"
        grdBarang.Columns(6).Width = 50
        grdBarang.Columns(6).TextAlignment = ContentAlignment.MiddleRight
        grdBarang.Columns(6).FormatString = "{0:###,###}"
        grdBarang.Columns(7).HeaderText = "SATUAN"
        grdBarang.Columns(7).Width = 80
        grdBarang.Columns(8).HeaderText = "STOK"
        grdBarang.Columns(8).Width = 50
        grdBarang.Columns(8).TextAlignment = ContentAlignment.MiddleRight
        grdBarang.Columns(8).FormatString = "{0:###,###}"
    End Sub
    'Private Sub frmEntryPersediaan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '    Me.WindowState = FormWindowState.Maximized
    '    If lNew Then
    '        txtKodeBarang.Focus()
    '    Else
    '        txtNamaBarang.Focus()
    '    End If
    'End Sub

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

    Private Sub txtCariBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then

            If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "persediaan", "view") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
                DefaultSetting()
                Exit Sub
            End If

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            grdBarang.Rows.Clear()

            'Dim ds As New DataSet
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        Dim rDr As SqlDataReader
                        If txtCariBarang.Text.Trim = "" Then
                            .CommandText = "select [nama barang],[plu],[kode barang]," &
                                           "[harga pokok pembelian],[harga jual toko]," &
                                           "[harga jual grosir],[isi],[satuan terkecil] " &
                                           "from barang order by [nama barang] asc"
                            rDr = .ExecuteReader
                            While rDr.Read
                                grdBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                        rDr.Item(1).ToString.Trim,
                                                        rDr.Item(2).ToString.Trim,
                                                        FormatNumber(rDr.Item(3).ToString.Trim, 0),
                                                        FormatNumber(rDr.Item(4).ToString.Trim, 0),
                                                        FormatNumber(rDr.Item(5).ToString.Trim, 0),
                                                        FormatNumber(rDr.Item(6).ToString.Trim, 0),
                                                        rDr.Item(7).ToString.Trim,
                                                        FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 0)})
                            End While
                            rDr.Close()

                            'Dim dsa As New SqlDataAdapter(Cmd)
                            'dsa.Fill(ds, "barang")
                            'grdBarang.DataSource = ds.Tables("barang")
                            'GridFormat()

                            lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
                            lShowData = False
                            Conn.Close()


                            grdBarang.Focus()
                            xForm.Dispose()
                            Exit Sub
                        End If

                        .CommandText = "select [nama barang],[plu],[kode barang]," &
                                       "[harga pokok pembelian],[harga jual toko]," &
                                       "[harga jual grosir],[isi],[satuan terkecil] " &
                                       "from barang where [plu] = '" & txtCariBarang.Text.Trim & "'" &
                                       " order by [nama barang] asc"
                        rDr = .ExecuteReader
                        While rDr.Read
                            grdBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                       rDr.Item(1).ToString.Trim,
                                                       rDr.Item(2).ToString.Trim,
                                                       FormatNumber(rDr.Item(3).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(4).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(5).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(6).ToString.Trim, 0),
                                                       rDr.Item(7).ToString.Trim,
                                                       FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 0)})
                        End While

                        If rDr.HasRows = True Then
                            lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
                            lShowData = False
                            Conn.Close()
                            rDr.Close()
                            grdBarang.Focus()
                            xForm.Dispose()
                            Exit Sub
                        End If

                        rDr.Close()

                        'Dim da As New SqlDataAdapter(Cmd)
                        'da.Fill(ds, "barang")
                        'If ds.Tables("barang").Rows.Count > 0 Then
                        '    grdBarang.DataSource = ds.Tables("barang")
                        '    GridFormat()

                        'Else
                        .CommandText = "select [nama barang],[plu],[kode barang]," &
                                           "[harga pokok pembelian],[harga jual toko]," &
                                           "[harga jual grosir],[isi],[satuan terkecil] " &
                                           "from barang where [kode barang] = '" & txtCariBarang.Text.Trim &
                                           "' order by [nama barang] asc"

                        rDr = .ExecuteReader
                        While rDr.Read
                            grdBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                       rDr.Item(1).ToString.Trim,
                                                       rDr.Item(2).ToString.Trim,
                                                       FormatNumber(rDr.Item(3).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(4).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(5).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(6).ToString.Trim, 0),
                                                       rDr.Item(7).ToString.Trim,
                                                       FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 0)})
                        End While

                        If rDr.HasRows = True Then
                            lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
                            lShowData = False
                            Conn.Close()
                            rDr.Close()
                            grdBarang.Focus()
                            xForm.Dispose()
                            Exit Sub
                        End If

                        rDr.Close()


                        'da = New SqlDataAdapter(Cmd)
                        '    da.Fill(ds, "barang")
                        '    If ds.Tables("barang").Rows.Count > 0 Then
                        '        grdBarang.DataSource = ds.Tables("barang")
                        '        GridFormat()

                        '    Else
                        .CommandText = "select [nama barang],[plu],[kode barang]," &
                                           "[harga pokok pembelian],[harga jual toko]," &
                                           "[harga jual grosir],[isi],[satuan terkecil] from barang " &
                                           "where [nama barang] like '%" & txtCariBarang.Text.Trim & "%'  order by [nama barang] asc"
                        rDr = .ExecuteReader
                        While rDr.Read
                            grdBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                       rDr.Item(1).ToString.Trim,
                                                       rDr.Item(2).ToString.Trim,
                                                       FormatNumber(rDr.Item(3).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(4).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(5).ToString.Trim, 0),
                                                       FormatNumber(rDr.Item(6).ToString.Trim, 0),
                                                       rDr.Item(7).ToString.Trim,
                                                       FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 0)})
                        End While
                        rDr.Close()
                        'da = New SqlDataAdapter(Cmd)
                        '        da.Fill(ds, "barang")
                        '        grdBarang.DataSource = ds.Tables("barang")
                        '        GridFormat()

                        '    End If
                        'End If
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
            End Using
            lShowData = False
            Conn.Close()
            grdBarang.Focus()
            xForm.Dispose()
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "persediaan", "add") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        lNew = True
        grdStokAwal.Rows.Clear()
        ClearScreen()
        EnableText()
        btnTambah.Enabled = False
        btnUbah.Enabled = False
        btnHapus.Enabled = False
        btnBatal.Enabled = True
        btnSimpan.Enabled = True

        btnAdd.Enabled = True
        btnRemove.Enabled = True
        txtIsi.Text = 0
        txtStokMinimum.Text = 0
        txtPLU.Focus()
    End Sub

    Private Sub btnBatal_Click_1(sender As Object, e As EventArgs) Handles btnBatal.Click
        DefaultSetting()
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "persediaan", "update") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            DefaultSetting()
            Exit Sub
        End If

        lNew = False
        EnableText()
        btnTambah.Enabled = False
        btnUbah.Enabled = False
        btnHapus.Enabled = False
        btnBatal.Enabled = True
        btnSimpan.Enabled = True

        btnAdd.Enabled = True
        btnRemove.Enabled = True
        cOldPlu = txtPLU.Text.Trim
        txtKodeBarang.Focus()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "persediaan", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            DefaultSetting()
            Exit Sub
        End If

        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "delete_barang"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 15)).Value = grdBarang.CurrentRow.Cells.Item(1).Value.ToString.Trim
                        .ExecuteNonQuery()

                        grdBarang.Rows.RemoveAt(grdBarang.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
        lblRecord.Text = "Jumlah Record : " & FormatNumber(grdBarang.Rows.Count, 0)
    End Sub

    Private Sub MasterTemplate_Click(sender As Object, e As EventArgs) Handles grdBarang.Click
        Try
            txtPLU.Text = grdBarang.CurrentRow.Cells.Item(1).Value.ToString.Trim
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True
            lShowData = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdBarang_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdBarang.CurrentRowChanged
        If lShowData Then
            txtPLU.Text = grdBarang.CurrentRow.Cells.Item(1).Value.ToString.Trim
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True
        End If
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        Try
            grdBarang.GridNavigator.SelectFirstRow()
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True
            lShowData = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            grdBarang.GridNavigator.SelectNextRow(1)
            lShowData = True
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Try
            grdBarang.GridNavigator.SelectPreviousRow(1)
            lShowData = True
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Try
            grdBarang.GridNavigator.SelectLastRow()
            lShowData = True
            ShowDataBarang()
            btnUbah.Enabled = True
            btnHapus.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdBarang_DataBindingComplete(sender As Object, e As Telerik.WinControls.UI.GridViewBindingCompleteEventArgs) Handles grdBarang.DataBindingComplete
        For Each datarows In grdBarang.Rows
            datarows.Cells.Item(8).Value = Val(clsPublic.CekStok(datarows.Cells.Item(1).Value.ToString.Trim))
        Next
    End Sub

    Private Sub btnAddSub_Click(sender As Object, e As EventArgs) Handles btnAddSub.Click
        Try
            grdSubItem.Rows.Add(New Object() {txtSubPLU.Text.Trim,
                                txtSubBarcode.Text.Trim,
                                txtSubNamaBarang.Text.Trim,
                                txtSubSatuanTerbesar.Text.Trim,
                                FormatNumber(Decimal.Parse(txtSubIsi.Text.Trim), 0),
                                txtSubSatuanTerkecil.Text.Trim,
                                FormatNumber(Decimal.Parse(txtSubHargaPokokPembelian.Text.Trim), 0),
                                FormatNumber(Decimal.Parse(txtSubPPNMasuk.Text.Trim), 0),
                                FormatNumber(Decimal.Parse(txtSubHargaPokokPenjualan.Text.Trim), 0),
                                FormatNumber(Decimal.Parse(txtSubHargaJualToko.Text.Trim), 0),
                                FormatNumber(Decimal.Parse(txtSubHargaJualGrosir.Text.Trim), 0)
                                })
            ClearSubItem()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearSubItem()
        txtSubPLU.Text = ""
        txtSubBarcode.Text = ""
        txtSubNamaBarang.Text = ""
        txtSubSatuanTerbesar.Text = ""
        txtSubSatuanTerkecil.Text = ""
        txtSubIsi.Text = 0
        txtSubHargaPokokPembelian.Text = 0
        txtSubPPNMasuk.Text = 0
        txtSubHargaPokokPenjualan.Text = 0
        txtSubHargaJualToko.Text = 0
        txtSubHargaJualGrosir.Text = 0
    End Sub
End Class
