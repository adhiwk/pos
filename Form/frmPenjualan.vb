
Imports System.Drawing.Printing
Imports System.Data.SqlClient
Public Class frmPenjualan

    Dim lNew As Boolean
    Friend cUserId As String
    Friend cNamaKasir As String
    Friend cBulan As String
    Friend cTahun As String
    Dim nQtyAwal As Decimal = 0

    'var utk proses tampung
    Friend cTampungAction As String
    Friend cKodeTampung As String
    Friend cAkses As Boolean
    Friend cCustName As String

    'var utk proses crud
    Friend cCrudAction As String
    Friend cNomorNota As String


    'variabel penjualan
    Dim cTanggal As String = ""

    Dim cPaket As String = ""
    Dim cTunai As String = ""
    Dim cGrosir As String = ""

    'variabel untuk data pembayaran
    Dim nTotal As Decimal = 0
    Dim cCaraBayar As String = ""

    Dim cNamaBank As String = ""
    Dim cJenisKartu As String = ""
    Dim cApprCode As String = ""

    Dim cTidCredit As String = ""
    Dim cCreditCardType As String = ""
    Dim cCreditCardNumber As String = ""
    'Dim cCreditCardHoldName As String = ""
    Dim cCreditCardExpDate As String = ""

    'variabel untuk perhitungan pajak
    Dim DPP As Decimal = 0
    Dim BKP As Decimal = 0
    Dim BTKP As Decimal = 0
    Dim PPN As Decimal = 0
    Dim nBayar, nKembali As Decimal

#Region "Printing Document"
    'Modify PrinterName to your receipt printer name
    Dim PrinterName As String = My.Settings.printername.Trim
    ReadOnly smallfont As New Font("Tahoma", 9)
    Private prn As PrintDocument
    Private Sub CutPapers()
        Try
            prn = New PrintDocument
            prn.PrinterSettings.PrinterName = PrinterName.Trim
            AddHandler prn.PrintPage, AddressOf Me.CutPrinting
            prn.Print()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CutPrinting(ByVal sender As Object, ByVal e As PrintPageEventArgs) As Boolean
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}
        e.Graphics.DrawString(".", smallfont, Brushes.Black, New PointF(19, 30), sf)
        e.HasMorePages = False
        Return True
    End Function

#End Region
#Region "Action Form"
    Private Sub frmPenjualan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If clsPublic.CekAkses("penjualan_edit_item", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If
        EditItem()
    End Sub
    Private Sub frmPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Beginilah jika sebuah ujian datang atas nama cinta, 
        'logika kalah dan hati berkali-kali berteman rasa haru. 
        'Tapi mungkin ini adalah hal terbaik, karena tuhan sedang rindu padaku.
        Me.MdiParent = frmMenuUtama
        DefaultSetting()
        tmTick.Start()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub ClearScreen()
        txtNomorNota.Text = ""
        txtNomorNota2.Text = ""
        dtTransaksi.DateTime = Date.Parse(clsPublic.GetDate)
        tmJam.Time = Now.Date
        dtJatuhTempo.DateTime = Now.Date
        txtSales.Text = ""
        txtKodeSales.Text = ""
        txtCustomer.Text = ""
        txtKodeCostumer.Text = ""
        chkEcer.Checked = False
        chkPaket.Checked = False
        chkTunai.Checked = False
        chkInvoice.Checked = False
        chkToko.Checked = False
        chkGrosir.Checked = False

        txtTotal.Text = FormatNumber(0, 2)
        grdPenjualan.Rows.Clear()
    End Sub

    Private Sub ClearDetail()
        txtNamaBarang.Text = ""
        txtKodeBarang.Text = ""
    End Sub

    Private Sub DisableText()
        txtNomorNota.Properties.ReadOnly = True
        txtNomorNota2.Properties.ReadOnly = True
        dtTransaksi.Properties.ReadOnly = True
        tmJam.Properties.ReadOnly = True
        dtJatuhTempo.Properties.ReadOnly = True
        txtSales.Properties.ReadOnly = True
        txtKodeSales.Properties.ReadOnly = True
        txtCustomer.Properties.ReadOnly = True
        txtKodeCostumer.Properties.ReadOnly = True
        chkEcer.Properties.ReadOnly = True
        chkPaket.Properties.ReadOnly = True
        chkTunai.Properties.ReadOnly = True
        chkInvoice.Properties.ReadOnly = True
        chkToko.Properties.ReadOnly = True
        chkGrosir.Properties.ReadOnly = True

        txtNamaBarang.Properties.ReadOnly = True
        txtKodeBarang.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        'txtNomorResep.Properties.ReadOnly = False
        'txtNomorResep2.Properties.ReadOnly = False
        dtTransaksi.Properties.ReadOnly = False
        tmJam.Properties.ReadOnly = False
        dtJatuhTempo.Properties.ReadOnly = False
        txtCustomer.Properties.ReadOnly = False
        txtSales.Properties.ReadOnly = False
        chkEcer.Properties.ReadOnly = False
        chkPaket.Properties.ReadOnly = False
        chkTunai.Properties.ReadOnly = False
        chkInvoice.Properties.ReadOnly = False
        chkToko.Properties.ReadOnly = False
        chkGrosir.Properties.ReadOnly = False
        txtNamaBarang.Properties.ReadOnly = False
        txtKodeBarang.Properties.ReadOnly = False
    End Sub
    Private Sub DefaultSetting()
        ClearScreen()
        ClearDetail()
        DisableText()
        DisableButton()
    End Sub
    Private Sub DisableButton()
        btnEdit.Enabled = False
        btnCancel.Enabled = False
        btnBayar.Enabled = False
        btnRekap.Enabled = True
        btnKoreksi.Enabled = False
        btnReprint.Enabled = False
        btnScreen.Enabled = False
        btnTampung.Enabled = False
        btnClose.Enabled = True
    End Sub

    Private Sub EnableButton()
        btnEdit.Enabled = True
        btnCancel.Enabled = True
        btnBayar.Enabled = True
        btnRekap.Enabled = False
        btnKoreksi.Enabled = True
        'btnReprint.Enabled = True
        btnTampung.Enabled = True
        btnClose.Enabled = False
    End Sub
    Private Sub txtNamaBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Using cfrmCariObat As New frmCariObat
                    cfrmCariObat.txtCariBarang.Text = txtNamaBarang.Text.Trim
                    cfrmCariObat.ShowDialog()
                    CariDataBarang(cfrmCariObat.grdCariBarang.CurrentRow.Cells.Item(1).Value.ToString.Trim)
                    ClearDetail()
                    txtKodeBarang.Focus()
                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        If btnBaru.Text = "F2 - &Baru" Then
            If clsPublic.CekAkses("penjualan_add", frmLogin.txtUserId.Text.Trim) = False Then
                MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
                DefaultSetting()
                Exit Sub
            End If
            lNew = True
            btnBaru.Text = "&F2 - &Batal"
            ClearScreen()
            ClearDetail()
            grdPenjualan.Rows.Clear()
            EnableButton()
            EnableText()
            chkEcer.Checked = True
            chkTunai.Checked = True
            chkToko.Checked = True
            GenerateNomorNota()
            txtSales.Focus()
        Else
            btnBaru.Text = "F2 - &Baru"
            DefaultSetting()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If clsPublic.CekAkses("penjualan_cancel_item", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If
        CancelItem()
    End Sub

    'Private Sub frmPenjualan_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.KeyCode = Keys.F2 Then
    '        btnBaru.PerformClick()
    '    ElseIf e.KeyCode = Keys.F3 Then
    '        EditItem()
    '    ElseIf e.KeyCode = Keys.F4 Then
    '        CancelItem()
    '    ElseIf e.KeyCode = Keys.Delete Then
    '        CancelItem()
    '    ElseIf e.KeyCode = Keys.F5 Then
    '        btnBayar.PerformClick()
    '    ElseIf e.KeyCode = Keys.F7 Then
    '        btnKoreksi.PerformClick()
    '    ElseIf e.KeyCode = Keys.F9 Then
    '        btnTampung.PerformClick()
    '    End If
    'End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub
    Private Sub btnReprint_Click(sender As Object, e As EventArgs) Handles btnReprint.Click
        CetakStruk()
    End Sub
    Private Sub btnKoreksi_Click(sender As Object, e As EventArgs) Handles btnKoreksi.Click
        If clsPublic.CekAkses("penjualan_koreksi", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        Using cfrmListPenjualan As New frmListPenjualan
            cfrmListPenjualan.nUbah = False
            cfrmListPenjualan.ShowDialog()
            If cCrudAction.Trim = "crud-edit" Then
                lNew = False
                ClearScreen()
                ClearDetail()
                txtNomorNota2.Properties.ReadOnly = True
                EditData()
            End If
        End Using
    End Sub
    Private Sub txtKodePasien_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        frmCustomer.ShowDialog()
    End Sub

    Private Sub txtKodeDokter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        frmDokter.ShowDialog()
    End Sub

    Private Sub chkEcer_CheckedChanged(sender As Object, e As EventArgs) Handles chkEcer.CheckedChanged
        If chkEcer.Checked = True Then
            chkPaket.Checked = False
        End If
    End Sub

    Private Sub chkResep_CheckedChanged(sender As Object, e As EventArgs) Handles chkPaket.CheckedChanged
        If chkPaket.Checked = True Then
            chkEcer.Checked = False
        End If
    End Sub

    Private Sub chkTunai_CheckedChanged(sender As Object, e As EventArgs) Handles chkTunai.CheckedChanged
        If chkTunai.Checked = True Then
            chkInvoice.Checked = False
        End If
    End Sub

    Private Sub chkInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoice.CheckedChanged
        If chkInvoice.Checked = True Then
            chkTunai.Checked = False
        End If
    End Sub

    Private Sub txtKodeObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKodeBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            CariDataBarang(txtKodeBarang.Text.Trim)
            ClearDetail()
            txtKodeBarang.Focus()
        End If
    End Sub

    Private Sub ChkToko_CheckedChanged(sender As Object, e As EventArgs) Handles chkToko.CheckedChanged
        If chkToko.CheckState = CheckState.Checked Then
            chkGrosir.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub ChkGrosir_CheckedChanged(sender As Object, e As EventArgs) Handles chkGrosir.CheckedChanged
        If chkGrosir.CheckState = CheckState.Checked Then
            chkToko.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub TmTick_Tick(sender As Object, e As EventArgs) Handles tmTick.Tick
        tmJam.EditValue = DateAndTime.TimeString
    End Sub

    Private Sub TxtCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtCustomer.ButtonClick
        frmCustomer.ShowDialog()
    End Sub
    Private Sub txtCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCustomer.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Using cfrmCariCustomer As New frmCariCustomer
                    cfrmCariCustomer.ShowDialog()
                    txtCustomer.Text = cfrmCariCustomer.grdCustomer.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeCostumer.Text = cfrmCariCustomer.grdCustomer.CurrentRow.Cells.Item(1).Value.ToString.Trim
                End Using
                txtKodeBarang.Focus()
            Catch ex As Exception

            End Try
        End If
    End Sub

#End Region
#Region "Grid Action"
    Private Sub grdPenjualan_CellEndEdit(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdPenjualan.CellEndEdit
        '8 = (3 * 6) - 8
        If e.ColumnIndex = 3 Then

            If Val(grdPenjualan.CurrentRow.Cells.Item(3).Value) = 0 Then
                grdPenjualan.CancelEdit()
                grdPenjualan.CurrentRow.Cells.Item(2).Value = Val(nQtyAwal)
            End If

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_log_penjualan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_nota", SqlDbType.NChar, 15)).Value = txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = frmLogin.txtUserId.Text.Trim
                    .Parameters.Add(New SqlParameter("@reason", SqlDbType.NChar, 15)).Value = "edit item"
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 7)).Value = grdPenjualan.CurrentRow.Cells.Item(11).Value.ToString.Trim
                    .Parameters.Add(New SqlParameter("@harga_jual", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(6).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty", SqlDbType.Decimal)).Value = Val(nQtyAwal)
                    .Parameters.Add(New SqlParameter("@qty_to", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(3).Value.ToString.Trim))
                    .ExecuteNonQuery()

                End With
            End Using
            Conn.Close()

            HitungSubTotal()

        ElseIf e.ColumnIndex = 6 Then
            grdPenjualan.CurrentRow.Cells.Item(6).Value = FormatNumber(Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(6).Value)), 0)
            HitungSubTotal()
        ElseIf e.ColumnIndex = 7 Then
            grdPenjualan.CurrentRow.Cells.Item(7).Value = FormatNumber(Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(7).Value)), 0)
            HitungSubTotal()
        ElseIf e.ColumnIndex = 8 Then
            HitungSubTotal()
        ElseIf e.ColumnIndex = 9 Then
            grdPenjualan.CurrentRow.Cells.Item(9).Value = FormatNumber(Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(9).Value)), 0)
        End If
        HitungTotal()
        txtKodeBarang.Focus()
    End Sub

    Private Sub grdPenjualan_CellBeginEdit(sender As Object, e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles grdPenjualan.CellBeginEdit
        If e.ColumnIndex = 0 Or e.ColumnIndex = 1 Or e.ColumnIndex = 4 Or e.ColumnIndex = 5 Or e.ColumnIndex = 9 Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If

        If e.ColumnIndex = 3 Then
            nQtyAwal = 0
            nQtyAwal = Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(3).Value))
            grdPenjualan.CurrentRow.Cells.Item(3).BeginEdit()
        End If
    End Sub
#End Region
#Region "Sub Routine"
    Private Sub CariDataBarang(ByVal cKodeBarang As String)
        Dim cPaket As String = ""
        Dim cGrosir As String = ""
        Dim nHarga As Decimal = 0
        Dim cQuery As String = ""

        If chkEcer.CheckState = CheckState.Checked Then
            cPaket = "T"
        Else
            cPaket = "Y"
        End If

        If chkGrosir.CheckState = CheckState.Checked Then
            cGrosir = "Y"
        Else
            cGrosir = "T"
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Try
            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn

                    'cari barcode
                    If chkToko.CheckState = CheckState.Checked Then
                        .CommandText = "select [nama barang],[kode barang]," &
                        "[bentuk sediaan],[satuan terkecil]," &
                        "[harga jual toko] from barang where [kode barang] = '" & cKodeBarang.Trim & "'"
                    ElseIf chkGrosir.CheckState = CheckState.Checked Then
                        .CommandText = "select [nama barang],[kode barang]," &
                        "[bentuk sediaan],[satuan terkecil]," &
                        "[harga jual grosir] from barang where [kode barang] = '" & cKodeBarang.Trim & "'"
                    End If

                    Dim nQty As Integer = 0
                    Dim lMatch As Boolean = False
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        'If grdPenjualan.Rows.Count > 0 Then
                        '    grdPenjualan.GridNavigator.SelectLastRow()
                        '    grdPenjualan.GridNavigator.SelectFirstRow()
                        'End If

                        For Each datarows In grdPenjualan.Rows
                            If datarows.Cells.Item(1).Value.ToString.Trim = rDr.Item(1).ToString.Trim And
                                   Decimal.Parse(datarows.Cells.Item(6).Value.ToString.Trim) = Decimal.Parse(rDr.Item(4).ToString.Trim) Then

                                nQty = Integer.Parse(datarows.Cells.Item(3).Value.ToString.Trim) + 1
                                datarows.Cells.Item(3).Value = Integer.Parse(nQty)

                                datarows.Cells.Item(8).Value = FormatNumber((Val(Decimal.Parse(datarows.Cells.Item(3).Value)) *
                                            Val(Decimal.Parse(datarows.Cells.Item(6).Value))) *
                                            Val(Decimal.Parse(datarows.Cells.Item(7).Value)) / 100, 0)

                                datarows.Cells.Item(9).Value =
                                            FormatNumber((Val(Decimal.Parse(datarows.Cells.Item(3).Value)) *
                                            Val(Decimal.Parse(datarows.Cells.Item(6).Value))) -
                                            Val(Decimal.Parse(datarows.Cells.Item(8).Value)), 0)

                                lMatch = True
                            End If
                            'grdPenjualan.GridNavigator.SelectNextRow(1)
                        Next

                        If lMatch = False Then
                            nHarga = Decimal.Parse(rDr.Item(4).ToString.Trim)
                            grdPenjualan.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                                rDr.Item(1).ToString.Trim,
                                                                cPaket.Trim, 1, rDr.Item(2).ToString.Trim,
                                                                rDr.Item(3).ToString.Trim,
                                                                FormatNumber(Decimal.Parse(nHarga), 0),
                                                                0, FormatNumber(0, 0),
                                                                FormatNumber(Decimal.Parse(nHarga), 0),
                                                                cGrosir.Trim})

                        End If
                    End While

                    rDr.Close()

                    'cari plu
                    If chkToko.CheckState = CheckState.Checked Then
                        .CommandText = "select [nama barang],[kode barang]," &
                        "[bentuk sediaan],[satuan terkecil]," &
                        "[harga jual toko] from barang where [plu] = '" & cKodeBarang.Trim & "'"
                    ElseIf chkGrosir.CheckState = CheckState.Checked Then
                        .CommandText = "select [nama barang],[kode barang]," &
                        "[bentuk sediaan],[satuan terkecil]," &
                        "[harga jual grosir] from barang where [plu] = '" & cKodeBarang.Trim & "'"
                    End If

                    nQty = 0
                    lMatch = False
                    rDr = .ExecuteReader
                    While rDr.Read
                        For Each datarows In grdPenjualan.Rows
                            If datarows.Cells.Item(1).Value.ToString.Trim = rDr.Item(1).ToString.Trim And
                                   Decimal.Parse(datarows.Cells.Item(6).Value.ToString.Trim) = Decimal.Parse(rDr.Item(4).ToString.Trim) Then

                                nQty = Integer.Parse(datarows.Cells.Item(3).Value.ToString.Trim) + 1
                                datarows.Cells.Item(3).Value = Integer.Parse(nQty)

                                datarows.Cells.Item(8).Value = FormatNumber((Val(Decimal.Parse(datarows.Cells.Item(3).Value)) *
                                            Val(Decimal.Parse(datarows.Cells.Item(6).Value))) *
                                            Val(Decimal.Parse(datarows.Cells.Item(7).Value)) / 100, 0)

                                datarows.Cells.Item(9).Value =
                                            FormatNumber((Val(Decimal.Parse(datarows.Cells.Item(3).Value)) *
                                            Val(Decimal.Parse(datarows.Cells.Item(6).Value))) -
                                            Val(Decimal.Parse(datarows.Cells.Item(8).Value)), 0)

                                lMatch = True
                            End If
                        Next

                        If lMatch = False Then
                            nHarga = Decimal.Parse(rDr.Item(4).ToString.Trim)
                            grdPenjualan.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                                rDr.Item(1).ToString.Trim,
                                                                cPaket.Trim, 1, rDr.Item(2).ToString.Trim,
                                                                rDr.Item(3).ToString.Trim,
                                                                FormatNumber(Decimal.Parse(nHarga), 0),
                                                                0, FormatNumber(0, 0),
                                                                FormatNumber(Decimal.Parse(nHarga), 0),
                                                                cGrosir.Trim})

                        End If
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
        grdPenjualan.CurrentRow.Cells.Item(8).Value = FormatNumber((Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(3).Value)) *
                 Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(6).Value))) *
                 Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(7).Value)) / 100, 0)

        grdPenjualan.CurrentRow.Cells.Item(9).Value =
           FormatNumber((Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(3).Value)) *
           Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(6).Value))) -
           Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(8).Value)), 0)
    End Sub

    Private Sub CancelItem()
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_log_penjualan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_nota", SqlDbType.NChar, 15)).Value = txtNomorNota.Text.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = frmLogin.txtUserId.Text.Trim
                    .Parameters.Add(New SqlParameter("@reason", SqlDbType.NChar, 15)).Value = "remove item"
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 7)).Value = grdPenjualan.CurrentRow.Cells.Item(11).Value.ToString.Trim
                    .Parameters.Add(New SqlParameter("@harga_jual", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(6).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty", SqlDbType.Decimal)).Value = Val(Decimal.Parse(grdPenjualan.CurrentRow.Cells.Item(3).Value.ToString.Trim))
                    .Parameters.Add(New SqlParameter("@qty_to", SqlDbType.Decimal)).Value = 0
                    .ExecuteNonQuery()

                End With
            End Using
            Conn.Close()

            grdPenjualan.Rows.RemoveAt(grdPenjualan.CurrentCell.RowIndex)
            HitungTotal()
            txtKodeBarang.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub HitungTotal()
        Dim nSubTotal As Decimal = 0
        For Each datarows In grdPenjualan.Rows
            nSubTotal = nSubTotal + Val(Decimal.Parse(datarows.Cells.Item(9).Value.ToString))
        Next
        txtTotal.Text = Val(nSubTotal)
    End Sub


    Private Sub EditItem()
        Try
            nQtyAwal = 0
            nQtyAwal = Val(grdPenjualan.CurrentRow.Cells.Item(3).Value)
            grdPenjualan.CurrentRow.Cells.Item(3).BeginEdit()
        Catch ex As Exception

        End Try
    End Sub

    Private Function GenerateNomorNota() As Boolean

        Dim cDay As String = ""
        Dim cMonth As String = ""
        Dim dTanggal As Date = Now.Date

        Dim cLastNoSticker As String = ""
        Dim cNowNoSticker As String = ""
        Dim strBarang As String =
            "select top 1 [nomor nota] from penjualan " &
            "where day([tgl nota]) = day(getdate()) and month([tgl nota]) = month(getdate()) " &
            "and year([tgl nota]) = year(getdate()) order by [nomor nota] desc"

        Dim conn As SqlConnection = clsPublic.KoneksiSQL()
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = strBarang
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            'MsgBox(Mid(rDr.Item(0).ToString.Trim, 9, 4))
                            cLastNoSticker = Val(Mid(rDr.Item(0).ToString.Trim, 10, 5)) + 1
                        End If
                    End While

                    If rDr.HasRows = True Then
                        If Len(Trim(Val(cLastNoSticker))) = 1 Then
                            cNowNoSticker = "0000" & Trim(Val(cLastNoSticker))
                        ElseIf Len(Trim(Val(cLastNoSticker))) = 2 Then
                            cNowNoSticker = "000" & Trim(Val(cLastNoSticker))
                        ElseIf Len(Trim(Val(cLastNoSticker))) = 3 Then
                            cNowNoSticker = "00" & Trim(Val(cLastNoSticker))
                        ElseIf Len(Trim(Val(cLastNoSticker))) = 4 Then
                            cNowNoSticker = "0" & Trim(Val(cLastNoSticker))
                        ElseIf Len(Trim(Val(cLastNoSticker))) = 5 Then
                            cNowNoSticker = Trim(Val(cLastNoSticker))
                        End If
                    Else
                        cNowNoSticker = "00001"
                    End If

                    rDr.Close()

                    .CommandText = "Select CONVERT(Char(23), GETDATE(), 21) As [tanggal]"
                    rDr = .ExecuteReader
                    While rDr.Read
                        dTanggal = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()

                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()

        If Len(dTanggal.Month.ToString.Trim) = 1 Then
            cMonth = "0" & dTanggal.Month.ToString.Trim
        ElseIf Len(dTanggal.Month.ToString.Trim) = 2 Then
            cMonth = dTanggal.Month.ToString.Trim
        End If

        If Len(dTanggal.Day.ToString.Trim) = 1 Then
            cDay = "0" & dTanggal.Day.ToString.Trim
        ElseIf Len(dTanggal.Day.ToString.Trim) = 2 Then
            cDay = dTanggal.Day.ToString.Trim
        End If

        'cNowNoSticker = 
        txtNomorNota.Text = dTanggal.Year.ToString.Trim &
                        cMonth.Trim & cDay.Trim
        txtNomorNota2.Text = cNowNoSticker.Trim

        Return True
    End Function
#End Region
#Region "Save Data"
    Private Sub SaveData()

        If txtNomorNota2.Text.Trim = "" Then
            MsgBox("nomor resep tidak boleh kosong", MsgBoxStyle.Critical, "Error")
            txtNomorNota2.Focus()
            Exit Sub
        End If

        If chkPaket.Checked = True Then
            cPaket = "Y"
        ElseIf chkEcer.Checked = True Then
            cPaket = "T"
        End If

        If chkTunai.Checked = True Then
            cTunai = "Y"
        ElseIf chkInvoice.Checked = True Then
            cTunai = "T"
        End If

        If chkToko.Checked = True Then
            cGrosir = "T"
        ElseIf chkGrosir.Checked = True Then
            cGrosir = "Y"
        End If

        If cTunai.Trim = "T" Then
            If txtKodeCostumer.Text.Trim = "" Then
                MsgBox("kode customer tidak boleh kosong", MsgBoxStyle.Critical, "error")
                txtCustomer.Focus()
                Exit Sub
            End If
        End If

        If clsPublic.VerifyData(txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim, "select [nomor nota] from penjualan " &
                                "where [nomor nota] = '" & txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim & "'") Then
            MsgBox("nomor nota sudah tersimpan dalam database", MsgBoxStyle.Critical, "Konfirmasi")
            'GenerateNomorResep()
            Exit Sub
        End If

        If grdPenjualan.Rows.Count <= 0 Then
            MsgBox("Basket penjualan masih kosong, data penjualan gagal disimpan", MsgBoxStyle.Critical, "Informasi")
            Exit Sub
        End If

        If cTunai.Trim = "Y" Then
            Try
                Using cFrmBayar As New frmPembayaran
                    For Each DataRow In grdPenjualan.Rows
                        nTotal = nTotal + Val(Decimal.Parse(DataRow.Cells(9).Value.ToString.Trim))
                    Next

                    cFrmBayar.nTotal = Val(nTotal)
                    cFrmBayar.ShowDialog()

                    nBayar = Val(Decimal.Parse(cFrmBayar.txtBayar.Text.Trim))
                    nKembali = Val(Decimal.Parse(nBayar)) - Val(Decimal.Parse(nTotal))

                    cCaraBayar = cFrmBayar.cJenisPembayaran.Trim
                    'cNamaBank = cFrmBayar.cboBankName.Text.Trim
                    cJenisKartu = cFrmBayar.cboJenisKartu.Text.Trim
                    cApprCode = cFrmBayar.txtApprCode.Text.Trim
                    'cCreditCardType = cFrmBayar.cboCCCardType.Text.Trim
                    cCreditCardNumber = cFrmBayar.txtCardNumber.Text.Trim
                    cCreditCardExpDate = cFrmBayar.txtExpDate.Text.Trim
                    'cCreditCardHoldName = cFrmBayar.txtCardHolderName.Text.Trim
                End Using
            Catch ex As Exception

            End Try

        ElseIf cTunai.Trim = "T" Then
            cCaraBayar = "Piutang"
            cJenisKartu = "-"
            cApprCode = "-"
            cCreditCardNumber = "-"
            cCreditCardExpDate = "-"
        End If


        Dim cKonfirmasi As String = MsgBox("Apakah transaksi penjualan sudah selesai?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Konfirmasi")
        If cKonfirmasi = vbYes Then

            cTanggal = clsPublic.ConvertTanggal(dtTransaksi) & " " & tmJam.EditValue.ToString.Trim

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                'Try
                With Cmd

                    .Connection = Conn
                    ''.CommandText = "select CONVERT(CHAR(23), GETDATE(), 21) as [tanggal]"
                    ''Dim rDr As SqlDataReader = .ExecuteReader

                    ''While rDr.Read
                    ''    dTanggal = rDr.Item(0).ToString.Trim
                    ''End While
                    ''cTanggal = dTanggal.Year & "-" & dTanggal.Month & "-" & dTanggal.Day

                    ''rDr.Close()

                    .CommandText = "add_penjualan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_nota", SqlDbType.NChar, 15)).Value = txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim
                    .Parameters.Add(New SqlParameter("@tgl_nota", SqlDbType.NChar, 20)).Value = cTanggal.Trim
                    .Parameters.Add(New SqlParameter("@kode_sales", SqlDbType.NChar, 5)).Value = txtKodeSales.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_cust", SqlDbType.NChar, 5)).Value = txtKodeCostumer.Text.Trim
                    .Parameters.Add(New SqlParameter("@tunai", SqlDbType.NChar, 1)).Value = cTunai.Trim
                    .Parameters.Add(New SqlParameter("@tgl_jatuh_tempo", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtJatuhTempo)
                    .Parameters.Add(New SqlParameter("@paket", SqlDbType.NChar, 1)).Value = cPaket.Trim
                    .Parameters.Add(New SqlParameter("@grosir", SqlDbType.NChar, 1)).Value = cGrosir.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                    .Parameters.Add(New SqlParameter("@cara_bayar", SqlDbType.NChar, 11)).Value = cCaraBayar.Trim
                    .Parameters.Add(New SqlParameter("@jenis_kartu", SqlDbType.NChar, 20)).Value = cJenisKartu.Trim
                    '.Parameters.Add(New SqlParameter("@nama_bank", SqlDbType.NChar, 25)).Value = cNamaBank.Trim
                    .Parameters.Add(New SqlParameter("@appr_code", SqlDbType.NChar, 15)).Value = cApprCode.Trim
                    '.Parameters.Add(New SqlParameter("@jenis_kartu_kredit", SqlDbType.NChar, 20)).Value = cApprCode.Trim
                    .Parameters.Add(New SqlParameter("@nomor_kartu", SqlDbType.NChar, 20)).Value = cCreditCardNumber.Trim
                    .Parameters.Add(New SqlParameter("@tanggal_expired", SqlDbType.NChar, 5)).Value = cCreditCardExpDate.Trim
                    '.Parameters.Add(New SqlParameter("@kode_toko", SqlDbType.NChar, 4)).Value = My.Settings.kodetoko.Trim
                    .Parameters.Add(New SqlParameter("@kode_kasir", SqlDbType.NChar, 5)).Value = My.Settings.kodekasir.Trim
                    .Parameters.Add(New SqlParameter("@shift", SqlDbType.NChar, 10)).Value = frmLogin.cboShift.Text.Trim
                    '/// parameter structured \\\

                    'data barang detail
                    Dim dtPenjualanDetail As New DataTable()
                    dtPenjualanDetail.Columns.Add("nomor nota", GetType([String])).MaxLength = 15
                    dtPenjualanDetail.Columns.Add("tgl nota", GetType([String])).MaxLength = 20
                    dtPenjualanDetail.Columns.Add("kode barang", GetType([String])).MaxLength = 25
                    dtPenjualanDetail.Columns.Add("paket", GetType([String])).MaxLength = 1
                    dtPenjualanDetail.Columns.Add("grosir", GetType([String])).MaxLength = 1
                    dtPenjualanDetail.Columns.Add("qty", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("kemasan", GetType([String])).MaxLength = 15
                    dtPenjualanDetail.Columns.Add("satuan", GetType([String])).MaxLength = 50
                    dtPenjualanDetail.Columns.Add("harga jual", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("disc", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("disc_total", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("sub_total", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("kode kasir", GetType([String])).MaxLength = 5


                    Dim nSubTotal As Decimal = 0


                    nTotal = 0

                    'salin semua baris pada datarow pada grid penjualan dan masukkan
                    'kedalam datatable
                    For Each DataRow In grdPenjualan.Rows

                        nSubTotal = Val(Decimal.Parse(DataRow.Cells(9).Value.ToString.Trim))
                        nTotal = Val(nTotal) + Val(nSubTotal)

                        'masukkan baris dari grid kedalam
                        'tabel detail penjualan
                        dtPenjualanDetail.Rows.Add(New Object() {
                            txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim,
                            cTanggal.Trim,
                            DataRow.Cells.Item(1).Value.ToString.Trim,
                            DataRow.Cells.Item(2).Value.ToString.Trim,
                            DataRow.Cells.Item(10).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRow.Cells.Item(3).Value.ToString.Trim)),
                            DataRow.Cells.Item(4).Value.ToString.Trim,
                            DataRow.Cells.Item(5).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRow.Cells.Item(6).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(7).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(8).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(9).Value.ToString.Trim)),
                            My.Settings.kodekasir.Trim
                           })
                    Next
                    .Parameters.Add("@PenjualanDetailTVP", SqlDbType.Structured).Value = dtPenjualanDetail
                    .Parameters.Add(New SqlParameter("@total", SqlDbType.Decimal)).Value = Val(nTotal)
                    .Parameters.Add(New SqlParameter("@bayar", SqlDbType.Decimal)).Value = Val(nBayar)
                    .Parameters.Add(New SqlParameter("@kembali", SqlDbType.Decimal)).Value = Val(nKembali)
                    .ExecuteNonQuery()

                End With
                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try
            End Using
            Conn.Close()

            Dim cMsg As String = MsgBox("Apakah ingin mencetak nota penjualan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Konfirmasi..")
            If cMsg = vbYes Then
                'Open Cash Drawer
                'Modify DrawerCode to your receipt printer open drawer code
                'Dim DrawerCode As String = Chr(27) & Chr(112) & Chr(48) & Chr(64) & Chr(64)
                'RawPrinterHelper.SendStringToPrinter(PrinterName, DrawerCode)
                CetakStruk()
            End If
            'DefaultSetting()
            cNomorNota = txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim
            btnBaru.Text = "F2 - &Baru"
            btnReprint.Enabled = True
            btnScreen.Enabled = True
            lNew = False
        End If
    End Sub
#End Region
#Region "Update Data"
    Private Sub UpdateData()
        Dim cTanggal As String = ""
        Dim cPaket As String = ""
        Dim cTunai As String = ""
        Dim cGrosir As String = ""
        Dim nTotal As Decimal = 0

        If txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim <> cNomorNota.Trim Then
            If clsPublic.VerifyData(txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim,
                                    "select [nomor nota] from penjualan where [nomor nota] = '" &
                                    txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim & "'") Then
                MsgBox("nomor nota sudah tersimpan dalam database", MsgBoxStyle.Critical, "error")
                txtNomorNota.Focus()
                Exit Sub
            End If
        End If


        If grdPenjualan.Rows.Count <= 0 Then
            MsgBox("Basket penjualan masih kosong, data penjualan gagal disimpan", MsgBoxStyle.Critical, "Informasi")
            Exit Sub
        End If


        If chkPaket.Checked = True Then
            cPaket = "Y"
        ElseIf chkEcer.Checked = True Then
            cPaket = "T"
        End If

        If chkTunai.Checked = True Then
            cTunai = "Y"
        ElseIf chkInvoice.Checked = True Then
            cTunai = "T"
        End If

        If chkGrosir.Checked = True Then
            cGrosir = "Y"
        ElseIf chkInvoice.Checked = True Then
            cTunai = "T"
        End If

        For Each DataRow In grdPenjualan.Rows
            nTotal = nTotal + Val(Decimal.Parse(DataRow.Cells(9).Value.ToString.Trim))
        Next

        cTanggal = clsPublic.ConvertTanggal(dtTransaksi) & " " & tmJam.EditValue.ToString.Trim

        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()

                With Cmd

                    .Connection = Conn

                    .CommandText = "update_penjualan"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@nomor_nota_awal", SqlDbType.NChar, 15)).Value = cNomorNota.Trim
                    .Parameters.Add(New SqlParameter("@nomor_nota", SqlDbType.NChar, 15)).Value = txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim
                    .Parameters.Add(New SqlParameter("@tgl_nota", SqlDbType.NChar, 20)).Value = cTanggal.Trim
                    .Parameters.Add(New SqlParameter("@kode_sales", SqlDbType.NChar, 5)).Value = txtKodeSales.Text.Trim
                    .Parameters.Add(New SqlParameter("@kode_cust", SqlDbType.NChar, 5)).Value = txtKodeCostumer.Text.Trim
                    .Parameters.Add(New SqlParameter("@tunai", SqlDbType.NChar, 1)).Value = cTunai.Trim
                    .Parameters.Add(New SqlParameter("@tgl_jatuh_tempo", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtJatuhTempo)
                    .Parameters.Add(New SqlParameter("@paket", SqlDbType.NChar, 1)).Value = cPaket.Trim
                    .Parameters.Add(New SqlParameter("@grosir", SqlDbType.NChar, 1)).Value = cGrosir.Trim
                    .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                    '.Parameters.Add(New SqlParameter("@cara_bayar", SqlDbType.NChar, 11)).Value = cCaraBayar.Trim
                    '.Parameters.Add(New SqlParameter("@jenis_kartu", SqlDbType.NChar, 20)).Value = cJenisKartu.Trim
                    '.Parameters.Add(New SqlParameter("@nama_bank", SqlDbType.NChar, 25)).Value = cNamaBank.Trim
                    '.Parameters.Add(New SqlParameter("@appr_code", SqlDbType.NChar, 15)).Value = cApprCode.Trim
                    '.Parameters.Add(New SqlParameter("@jenis_kartu_kredit", SqlDbType.NChar, 20)).Value = cApprCode.Trim
                    '.Parameters.Add(New SqlParameter("@nomor_kartu", SqlDbType.NChar, 20)).Value = cCreditCardNumber.Trim
                    '.Parameters.Add(New SqlParameter("@tanggal_expired", SqlDbType.NChar, 5)).Value = cCreditCardExpDate.Trim
                    '.Parameters.Add(New SqlParameter("@kode_toko", SqlDbType.NChar, 4)).Value = My.Settings.kodetoko.Trim
                    .Parameters.Add(New SqlParameter("@kode_kasir", SqlDbType.NChar, 5)).Value = My.Settings.kodekasir.Trim
                    .Parameters.Add(New SqlParameter("@shift", SqlDbType.NChar, 10)).Value = frmLogin.cboShift.Text.Trim
                    '/// parameter structured \\\

                    'data barang detail
                    Dim dtPenjualanDetail As New DataTable()
                    dtPenjualanDetail.Columns.Add("nomor nota", GetType([String])).MaxLength = 15
                    dtPenjualanDetail.Columns.Add("tgl nota", GetType([String])).MaxLength = 20
                    dtPenjualanDetail.Columns.Add("kode barang", GetType([String])).MaxLength = 25
                    dtPenjualanDetail.Columns.Add("paket", GetType([String])).MaxLength = 1
                    dtPenjualanDetail.Columns.Add("grosir", GetType([String])).MaxLength = 1
                    dtPenjualanDetail.Columns.Add("qty", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("kemasan", GetType([String])).MaxLength = 15
                    dtPenjualanDetail.Columns.Add("satuan", GetType([String])).MaxLength = 50
                    dtPenjualanDetail.Columns.Add("harga jual", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("disc", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("disc_total", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("sub_total", GetType(Decimal))
                    dtPenjualanDetail.Columns.Add("kode kasir", GetType([String])).MaxLength = 5

                    Dim nSubTotal As Decimal = 0


                    nTotal = 0

                    'salin semua baris pada datarow pada grid penjualan dan masukkan
                    'kedalam datatable
                    For Each DataRow In grdPenjualan.Rows

                        nSubTotal = Val(Decimal.Parse(DataRow.Cells(9).Value.ToString.Trim))
                        nTotal = Val(nTotal) + Val(nSubTotal)

                        'masukkan baris dari grid kedalam
                        'tabel detail penjualan
                        dtPenjualanDetail.Rows.Add(New Object() {
                            txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim,
                            cTanggal.Trim,
                            DataRow.Cells.Item(1).Value.ToString.Trim,
                            DataRow.Cells.Item(2).Value.ToString.Trim,
                            DataRow.Cells.Item(10).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRow.Cells.Item(3).Value.ToString.Trim)),
                            DataRow.Cells.Item(4).Value.ToString.Trim,
                            DataRow.Cells.Item(5).Value.ToString.Trim,
                            Val(Decimal.Parse(DataRow.Cells.Item(6).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(7).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(8).Value.ToString.Trim)),
                            Val(Decimal.Parse(DataRow.Cells.Item(9).Value.ToString.Trim)),
                            My.Settings.kodekasir.Trim
                           })
                    Next
                    .Parameters.Add("@PenjualanDetailTVP", SqlDbType.Structured).Value = dtPenjualanDetail
                    .Parameters.Add(New SqlParameter("@total", SqlDbType.Decimal)).Value = Val(nTotal)
                    '.Parameters.Add(New SqlParameter("@bayar", SqlDbType.Decimal)).Value = Val(nBayar)
                    '.Parameters.Add(New SqlParameter("@kembali", SqlDbType.Decimal)).Value = Val(nKembali)
                    .ExecuteNonQuery()

                End With

            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
        MsgBox("Data berhasil diupdate")
        btnBaru.Text = "F2 - &Baru"
        btnScreen.Enabled = True
        btnReprint.Enabled = True
        lNew = False
    End Sub
#End Region
#Region "Tampung Transaksi"
    Private Sub btnTampung_Click(sender As Object, e As EventArgs) Handles btnTampung.Click
        'Try
        Using cfrmTampungPenjualan As New frmTampungPenjualan
            cfrmTampungPenjualan.cUserId = cUserId.Trim
            cfrmTampungPenjualan.ShowDialog()

            If cTampungAction.Trim = "Tampung-Add" Then
                AddTampung()
            ElseIf cTampungAction.Trim = "Tampung-Load" Then
                LoadTampung()
            End If
        End Using
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub AddTampung()
        Dim cPaket As String = ""
        Dim cTunai As String = ""
        Dim cGrosir As String = ""

        If chkPaket.Checked = True Then
            cPaket = "Y"
        ElseIf chkEcer.Checked = True Then
            cPaket = "T"
        End If

        If chkTunai.Checked = True Then
            cTunai = "Y"
        ElseIf chkInvoice.Checked = True Then
            cTunai = "T"
        End If

        If chkGrosir.Checked = True Then
            cGrosir = "Y"
        ElseIf chkToko.Checked = True Then
            cGrosir = "T"
        End If

        If grdPenjualan.Rows.Count <= 0 Then
            Exit Sub
        End If

        Dim cNomorTampung As String = ""
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            'Try
            With Cmd

                cNomorTampung = Mid(cUserId.Trim, 1, 3) & "/" & My.Settings.kodekasir.Trim & "/" &
                                    FormatDateTime(Now.Date, DateFormat.ShortDate) & "/" &
                                    Now.Hour & Now.Minute & Now.Second


                .Connection = Conn
                .CommandText = "add_penjualan_tampung"
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                .Parameters(0).SqlDbType = SqlDbType.NChar
                .Parameters(0).Direction = ParameterDirection.Output


                .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                .Parameters.Add(New SqlParameter("@nomor_tampung", SqlDbType.NChar, 25)).Value = cNomorTampung.Trim
                .Parameters.Add(New SqlParameter("@tunai", SqlDbType.NChar, 1)).Value = cTunai.Trim
                .Parameters.Add(New SqlParameter("@paket", SqlDbType.NChar, 1)).Value = cPaket.Trim
                .Parameters.Add(New SqlParameter("@grosir", SqlDbType.NChar, 1)).Value = cGrosir.Trim
                .Parameters.Add(New SqlParameter("@cust_name", SqlDbType.NChar, 50)).Value = cCustName.Trim
                .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                .Parameters.Add(New SqlParameter("@nomor_kasir", SqlDbType.NChar, 5)).Value = My.Settings.kodekasir.Trim
                .Parameters.Add(New SqlParameter("@shift", SqlDbType.NChar, 1)).Value = frmLogin.cboShift.Text.Trim





                '/// parameter structured \\\
                '0   plu
                '1   nama barang
                '2   harga
                '3   qty
                '4   disc
                '5   disc total
                '6   satuan
                '7   sub total
                '8   item
                '9   harga pokok
                '10  margin
                '11  ppn keluar
                '12  short name

                'data tampung
                Dim dtDataTampung As New DataTable()
                dtDataTampung.Columns.Add("nomor tampung", GetType([String])).MaxLength = 25
                dtDataTampung.Columns.Add("nama barang", GetType([String])).MaxLength = 50
                dtDataTampung.Columns.Add("kode barang", GetType([String])).MaxLength = 25
                dtDataTampung.Columns.Add("paket", GetType([String])).MaxLength = 1
                dtDataTampung.Columns.Add("grosir", GetType([String])).MaxLength = 1
                dtDataTampung.Columns.Add("qty", GetType(Decimal))
                dtDataTampung.Columns.Add("kemasan", GetType([String])).MaxLength = 15
                dtDataTampung.Columns.Add("satuan", GetType([String])).MaxLength = 50
                dtDataTampung.Columns.Add("harga jual", GetType(Decimal))
                dtDataTampung.Columns.Add("disc", GetType(Decimal))
                dtDataTampung.Columns.Add("discount", GetType(Decimal))
                dtDataTampung.Columns.Add("sub total", GetType(Decimal))

                For Each DataRows In grdPenjualan.Rows
                    dtDataTampung.Rows.Add(New Object() {
                                                            cNomorTampung.Trim,
                                                            DataRows.Cells(0).Value.ToString.Trim,
                                                            DataRows.Cells(1).Value.ToString.Trim,
                                                            DataRows.Cells(2).Value.ToString.Trim,
                                                            DataRows.Cells(10).Value.ToString.Trim,
                                                            Val(Decimal.Parse(DataRows.Cells(3).Value.ToString.Trim)),
                                                            DataRows.Cells(4).Value.ToString.Trim,
                                                            DataRows.Cells(5).Value.ToString.Trim,
                                                            Val(Decimal.Parse(DataRows.Cells(6).Value.ToString.Trim)),
                                                            Val(Decimal.Parse(DataRows.Cells(7).Value.ToString.Trim)),
                                                            Val(Decimal.Parse(DataRows.Cells(8).Value.ToString.Trim)),
                                                            Val(Decimal.Parse(DataRows.Cells(9).Value.ToString.Trim))
                                                         })
                Next
                .Parameters.Add("@PenjualanTampungDetailTVP", SqlDbType.Structured).Value = dtDataTampung
                .ExecuteNonQuery()

            End With
            'Catch ex As Exception
            'MsgBox(ex.Message)
            'End Try
        End Using
        Conn.Close()
        DefaultSetting()
        btnBaru.Text = "F2 - &Baru"
    End Sub

    Private Sub LoadTampung()
        GenerateNomorNota()

        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn

                    .CommandText = "select [tunai],[paket],[grosir] from penjualan_tampung " &
                                   "where [nomor tampung] = '" & cKodeTampung.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        'tunai
                        If rDr.Item(0).ToString.Trim = "Y" Then
                            chkTunai.Checked = True
                        ElseIf rDr.Item(0).ToString.Trim = "T" Then
                            chkInvoice.Checked = True
                        End If

                        'paket
                        If rDr.Item(1).ToString.Trim = "Y" Then
                            chkPaket.Checked = True
                        ElseIf rDr.Item(1).ToString.Trim = "T" Then
                            chkEcer.Checked = True
                        End If

                        'grosir
                        If rDr.Item(2).ToString.Trim = "Y" Then
                            chkGrosir.Checked = True
                        ElseIf rDr.Item(2).ToString.Trim = "T" Then
                            chkToko.Checked = True
                        End If

                    End While
                    rDr.Close()

                    .CommandText = "select [nama barang],[kode barang],[paket]," &
                                   "[qty],[kemasan],[satuan],[harga jual],[disc]," &
                                   "[discount],[sub total],[grosir] " &
                                   "from penjualan_tampung_detail where [nomor tampung] = '" & cKodeTampung.Trim & "'"
                    rDr = .ExecuteReader
                    grdPenjualan.Rows.Clear()

                    While rDr.Read
                        grdPenjualan.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                            rDr.Item(1).ToString.Trim,
                                                            rDr.Item(2).ToString.Trim,
                                                            Val(rDr.Item(3).ToString.Trim),
                                                            rDr.Item(4).ToString.Trim,
                                                            rDr.Item(5).ToString.Trim,
                                                            FormatNumber(Decimal.Parse(rDr.Item(6).ToString.Trim), 0),
                                                            FormatNumber(Decimal.Parse(rDr.Item(7).ToString.Trim), 0),
                                                            FormatNumber(Decimal.Parse(rDr.Item(8).ToString.Trim), 0),
                                                            FormatNumber(Decimal.Parse(rDr.Item(9).ToString.Trim), 0),
                                                            rDr.Item(10).ToString.Trim})
                    End While
                    rDr.Close()

                    .CommandText = "update penjualan_tampung set [status]= 'Y' where [nomor tampung] = '" & cKodeTampung.Trim & "'"
                    .ExecuteNonQuery()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
        HitungTotal()
        txtKodeBarang.Focus()
    End Sub
#End Region
#Region "Cetak Struk"
    Private Sub CetakStruk()
        Dim nSubTotal As Decimal = 0
        Dim cPrint As String = ""
        If My.Settings.paper_width.Trim = "80mm" Then
            cPrint =
           clsPublic.AlignToCenter80(My.Settings.namatoko.Trim) & vbCrLf &
           clsPublic.AlignToCenter80(My.Settings.alamattoko.Trim) & vbCrLf &
           clsPublic.AlignToCenter80(My.Settings.kota.Trim) & vbCrLf &
           clsPublic.AlignToCenter80(My.Settings.notelp.Trim) & vbCrLf &
           clsPublic.AlignToCenter80(My.Settings.npwp.Trim) & vbCrLf &
            "-------------------------------------------------" & vbCrLf &
            "   Nomor  : " & txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim & vbCrLf &
            "-------------------------------------------------" & vbCrLf

            nSubTotal = 0

            For Each DataRow In grdPenjualan.Rows
                cPrint = cPrint & "   " &
                SubStringItem80(DataRow.Cells(0).Value.ToString.Trim) & vbCrLf &
                "   " &
                SubStringQty80(FormatNumber(Decimal.Parse(DataRow.Cells(3).Value), 0)) & " " &
                SubStringTotal80(FormatNumber(Decimal.Parse(DataRow.Cells(9).Value), 0)) & vbCrLf
            Next

            cPrint = cPrint &
            "-------------------------------------------------" & vbCrLf &
            "   Sub Total : " & SubStringTotal80(FormatNumber(Val(Decimal.Parse(nTotal)), 0)) & vbCrLf &
            "   Bayar     : " & SubStringTotal80(FormatNumber(Val(nBayar), 0)) & vbCrLf &
            "   Kembali   : " & SubStringTotal80(FormatNumber(Val(nKembali), 0)) & vbCrLf


            If cCaraBayar.Trim = "Kartu" Then
                cPrint = cPrint &
            "   Card Payment " & FormatNumber(Val(nBayar), 2) & vbCrLf &
            "   Card Type    " & cJenisKartu.Trim & vbCrLf &
            "   Nomor Kartu  " & cCreditCardNumber.Trim & vbCrLf &
            "   Nomor Appr.  " & cApprCode.Trim & vbCrLf
            End If

            cPrint = cPrint &
            "-------------------------------------------------" & vbCrLf &
             clsPublic.AlignToCenter80("Terima kasih atas kunjungan anda") & vbCrLf &
            "-------------------------------------------------" & vbCrLf &
            " " & vbCrLf

        ElseIf My.Settings.paper_width.Trim = "55mm" Then
            cPrint =
           clsPublic.AlignToCenter(My.Settings.namatoko.Trim) & vbCrLf &
           clsPublic.AlignToCenter(My.Settings.alamattoko.Trim) & vbCrLf &
           clsPublic.AlignToCenter(My.Settings.kota.Trim) & vbCrLf &
           clsPublic.AlignToCenter(My.Settings.notelp.Trim) & vbCrLf &
           clsPublic.AlignToCenter(My.Settings.npwp.Trim) & vbCrLf &
            "--------------------------------" & vbCrLf &
            "Nomor  : " & txtNomorNota.Text.Trim & "." & txtNomorNota2.Text.Trim & vbCrLf &
            "--------------------------------" & vbCrLf

            nSubTotal = 0


            For Each DataRow In grdPenjualan.Rows

                cPrint = cPrint &
                    SubStringItem(DataRow.Cells(0).Value.ToString.Trim) & vbCrLf &
                    "   " &
                    SubStringQty(FormatNumber(Decimal.Parse(DataRow.Cells(3).Value), 0)) & " " &
                    SubStringTotal(FormatNumber(Decimal.Parse(DataRow.Cells(9).Value), 0)) & vbCrLf
            Next

            cPrint = cPrint &
                "--------------------------------" & vbCrLf &
                "Sub Total : " & SubStringTotal(FormatNumber(Val(Decimal.Parse(nTotal)), 0)) & vbCrLf

            If cCaraBayar.Trim = "Cash" Then
                cPrint = cPrint &
                "Bayar     : " & SubStringTotal(FormatNumber(Val(nBayar), 2)) & vbCrLf &
                "Kembali   : " & SubStringTotal(FormatNumber(Val(nKembali), 2)) & vbCrLf
            End If

            If cCaraBayar.Trim = "Kartu" Then
                cPrint = cPrint &
                "Card Payment " & FormatNumber(Val(nBayar), 2) & vbCrLf &
                "Card Type    " & cJenisKartu.Trim & vbCrLf &
                "Nomor Kartu  " & cCreditCardNumber.Trim & vbCrLf &
                "Nomor Appr.  " & cApprCode.Trim & vbCrLf
            End If

            cPrint = cPrint &
                "--------------------------------" & vbCrLf &
                "Terima kasih atas kunjungan anda" & vbCrLf &
                " " & vbCrLf
        End If
        RawPrinterHelper.SendStringToPrinter(PrinterName, cPrint)
        CutPapers()
    End Sub

    Private Function SubStringPcs(ByVal cPcs As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cPcs) >= 3 Then
            cPcs = Mid(cPcs.Trim, 1, 3)
            Return cPcs.Trim
        Else
            For x = 1 To 3 - Val(Len(cPcs.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cPcs.Trim & cReturnString
        End If
    End Function
    Private Function SubStringPlu(ByVal cPlu As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cPlu) = 15 Then
            Return cPlu.Trim
        Else
            For x = 1 To 15 - Val(Len(cPlu.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cPlu.Trim & cReturnString
        End If
    End Function

    Private Function SubStringItem80(ByVal cQty As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cQty) = 50 Then
            Return cQty.Trim
        Else
            For x = 1 To 50 - Val(Len(cQty.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cQty.Trim & cReturnString
        End If
    End Function
    Private Function SubStringItem(ByVal cQty As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cQty) = 20 Then
            Return cQty.Trim
        Else
            For x = 1 To 20 - Val(Len(cQty.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cQty.Trim & cReturnString
        End If
    End Function

    Private Function SubStringQty80(ByVal cQty As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cQty) = 11 Then
            Return cQty.Trim
        Else
            For x = 1 To 11 - Val(Len(cQty.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cQty.Trim
        End If
    End Function
    Private Function SubStringQty(ByVal cQty As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cQty) = 3 Then
            Return cQty.Trim
        Else
            For x = 1 To 8 - Val(Len(cQty.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cQty.Trim
        End If
    End Function

    Private Function SubStringHarga(ByVal cHarga As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cHarga) = 6 Then
            Return cHarga.Trim
        Else
            For x = 1 To 6 - Val(Len(cHarga.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cHarga.Trim
        End If
    End Function

    Private Function SubStringDiscount(ByVal cDiscount As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cDiscount) = 7 Then
            Return cDiscount.Trim
        Else
            For x = 1 To 7 - Val(Len(cDiscount.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cDiscount.Trim
        End If
    End Function
    Private Function SubStringTotal80(ByVal cTotal As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cTotal) = 30 Then
            Return cTotal.Trim
        Else
            For x = 1 To 30 - Val(Len(cTotal.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cTotal.Trim
        End If
    End Function
    Private Function SubStringTotal(ByVal cTotal As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cTotal) = 15 Then
            Return cTotal.Trim
        Else
            For x = 1 To 20 - Val(Len(cTotal.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cTotal.Trim
        End If
    End Function
#End Region
#Region "Rekap Transaksi"
    Private Sub Rekap()
        Try
            Dim conn As SqlConnection = clsPublic.KoneksiSQL
            conn.Open()


            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = "select [grup id] from users where [user id] = '" & cUserId.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    Dim nTotal As Integer = 0

                    While rDr.Read
                        If rDr.Item(0).ToString.Trim = "ADMIN" Then
                            Using cfrmRekapAdmin As New frmRekapAdmin
                                cfrmRekapAdmin.ShowDialog()
                            End Using
                        ElseIf rDr.Item(0).ToString.Trim = "KASIR" Then
                            Using cfrmRekapKasir As New frmRekapKasir
                                cfrmRekapKasir.cUserId = cUserId.Trim
                                cfrmRekapKasir.cUserName = cNamaKasir.Trim
                                cfrmRekapKasir.ShowDialog()
                            End Using
                        End If
                    End While
                    rDr.Close()

                End With
            End Using
            conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRekap_Click(sender As Object, e As EventArgs) Handles btnRekap.Click
        Rekap()
    End Sub

#End Region
#Region "Proses Edit Transaksi"
    Private Sub EditData()
        Try
            Dim cLenKode As Integer = 0
            Dim cQuery As String = "SELECT penjualan.[nomor nota],penjualan.[tgl nota]," &
                                   "penjualan.[tunai],penjualan.[paket],penjualan.[grosir],penjualan.[kode cust],penjualan.[kode sales] " &
                                   "FROM penjualan WHERE penjualan.[nomor nota] = '" & cNomorNota.Trim & "'"

            Dim conn As SqlConnection = clsPublic.KoneksiSQL
            conn.Open()

            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = cQuery.Trim
                    Dim rDr As SqlDataReader = .ExecuteReader
                    Dim nTotal As Integer = 0

                    While rDr.Read

                        cLenKode = Len(rDr.Item(0).ToString.Trim) - 9
                        txtNomorNota.Text = Mid(rDr.Item(0).ToString.Trim, 1, 8)
                        txtNomorNota2.Text = Mid(rDr.Item(0).ToString.Trim, 10, Integer.Parse(cLenKode))

                        dtTransaksi.DateTime = Date.Parse(rDr.Item(1).ToString.Trim)

                        'resep
                        If rDr.Item(2).ToString.Trim = "Y" Then
                            chkTunai.Checked = True
                        ElseIf rDr.Item(2).ToString.Trim = "T" Then
                            chkInvoice.Checked = True
                        End If

                        'tunai
                        If rDr.Item(3).ToString.Trim = "Y" Then
                            chkPaket.Checked = True
                        ElseIf rDr.Item(3).ToString.Trim = "T" Then
                            chkEcer.Checked = True
                        End If

                        If rDr.Item(4).ToString.Trim = "Y" Then
                            chkGrosir.Checked = True
                        ElseIf rDr.Item(4).ToString.Trim = "T" Then
                            chkToko.Checked = True
                        End If

                        txtKodeCostumer.Text = rDr.Item(5).ToString.Trim
                        txtKodeSales.Text = rDr.Item(6).ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "SELECT barang.[nama barang],penjualan_detail.[kode barang]," &
                                   "penjualan_detail.[paket],penjualan_detail.[qty]," &
                                   "penjualan_detail.[kemasan],penjualan_detail.[satuan]," &
                                   "penjualan_detail.[harga jual],penjualan_detail.[disc]," &
                                   "penjualan_detail.[discount],penjualan_detail.[sub total]," &
                                   "penjualan_detail.[grosir] " &
                                   "FROM penjualan_detail penjualan_detail INNER JOIN barang  " &
                                   "barang ON penjualan_detail.[kode barang] = barang.[kode barang] " &
                                   "WHERE penjualan_detail.[nomor nota] = '" & cNomorNota.Trim & "'"

                    rDr = .ExecuteReader
                    While rDr.Read
                        grdPenjualan.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                            rDr.Item(1).ToString.Trim,
                                                            rDr.Item(2).ToString.Trim,
                                                            FormatNumber(Val(rDr.Item(3).ToString.Trim), 0),
                                                            rDr.Item(4).ToString.Trim,
                                                            rDr.Item(5).ToString.Trim,
                                                            FormatNumber(Val(rDr.Item(6).ToString.Trim), 0),
                                                            FormatNumber(Val(rDr.Item(7).ToString.Trim), 0),
                                                            FormatNumber(Val(rDr.Item(8).ToString.Trim), 0),
                                                            FormatNumber(Val(rDr.Item(9).ToString.Trim), 0),
                                                            rDr.Item(10).ToString.Trim})

                    End While
                    rDr.Close()

                    .CommandText = "select [nama] from customer where [kode cust] = '" & txtKodeCostumer.Text.Trim & "'"
                    rDr = .ExecuteReader
                    While rDr.Read
                        txtCustomer.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "select [nama] from sales where [kode sales] = '" & txtKodeSales.Text.Trim & "'"
                    rDr = .ExecuteReader
                    While rDr.Read
                        txtSales.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()

                End With
            End Using
            conn.Close()
            HitungTotal()
        Catch ex As Exception

        End Try
    End Sub



#End Region
End Class