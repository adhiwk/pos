Imports System.Data.SqlClient
Public Class frmRekapAdmin
    Private Sub frmRekapAdmin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue.ToString = "27" Then
            Me.Dispose()
        ElseIf e.KeyValue.ToString = "113" Then
            FilterData()
        ElseIf e.KeyValue.ToString = "114" Then
            CetakRekapAdmin()
        End If
    End Sub

    Private Sub frmRekapAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUser()
        dtTransaksi.DateTime = Now.Date
        cboUserId.Text = ""
        txtNamaKasir.Text = ""
        txtNamaKasir.Properties.ReadOnly = True
    End Sub

    Private Sub LoadUser()
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "select [user id] from users order by [user id] asc"
                    cboUserId.Properties.Items.Clear()
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        cboUserId.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub cboUserId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUserId.SelectedIndexChanged
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "select [nama user] from users where [user id] = '" & cboUserId.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        txtNamaKasir.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        FilterData
    End Sub

    Private Sub FilterData()
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "SELECT rekap_kasir.[kode kasir],rekap_kasir.[shift]," &
                                       "rekap_kasir.[modal],rekap_kasir.[kas aktual] " &
                                       "FROM rekap_kasir WHERE rekap_kasir.[user id] = '" & cboUserId.Text.Trim & "' and " &
                                       "day(rekap_kasir.[tgl transaksi]) = " & dtTransaksi.DateTime.Day & " and " &
                                       "month(rekap_kasir.[tgl transaksi]) = " & dtTransaksi.DateTime.Month & " and " &
                                       "year(rekap_kasir.[tgl transaksi]) = " & dtTransaksi.DateTime.Year


                    Dim rDr As SqlDataReader = .ExecuteReader
                    grdRekapAdmin.Rows.Clear()
                    While rDr.Read
                        grdRekapAdmin.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                                 rDr.Item(1).ToString.Trim,
                                                                 txtNamaKasir.Text.Trim,
                                                                 Val(rDr.Item(2).ToString.Trim),
                                                                 Val(rDr.Item(3).ToString.Trim)})
                    End While
                    rDr.Close()

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub

    Private Sub CetakRekapAdmin()
        Dim cPrint As String =
            "Report Kasir  : " & FormatDateTime(dtTransaksi.DateTime, DateFormat.ShortDate).ToString.Trim & vbCrLf &
            "Kassa         : " & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & vbCrLf &
            "SHIFT         : " & grdRekapAdmin.CurrentRow.Cells.Item(1).Value.ToString.Trim & vbCrLf &
            "NAMA OPERATOR : " & grdRekapAdmin.CurrentRow.Cells.Item(2).Value.ToString.Trim & vbCrLf &
            "----------------------------------------" & vbCrLf
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn

                    Dim nJmlTransaksi As Integer = 0
                    Dim nTotalTunai As Decimal = 0
                    Dim nTotalKartu As Decimal = 0
                    Dim nGrandTunai As Decimal = 0
                    Dim nTotalPenjualan As Decimal = 0
                    Dim nTotalPengeluaran As Decimal = 0
                    Dim nTotalDiskon As Decimal = 0
                    Dim nGrandTotal As Decimal = 0
                    Dim nVariant As Decimal = 0
                    Dim nRetur As Decimal = 0

                    'hitung jumlah transaksi
                    .CommandText = "SELECT count(penjualan.[nomor resep]) as [Jumlah Transaksi] " &
                                   "From penjualan penjualan WHERE Day(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(penjualan.[tgl resep])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And penjualan.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And penjualan.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"



                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nJmlTransaksi = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While

                    rDr.Close()

                    'total retur
                    '.CommandText = "SELECT SUM(penjualan_retur.[qty retur] * penjualan_retur.[harga jual]) as [total retur] " &
                    '               "From penjualan_retur WHERE Day(penjualan_retur.[tgl retur]) = " & dtTransaksi.DateTime.Day & " " &
                    '               "And month(penjualan_retur.[tgl retur]) = " & dtTransaksi.DateTime.Month & " " &
                    '               "And year(penjualan_retur.[tgl retur])  = " & dtTransaksi.DateTime.Year & " " &
                    '               "And penjualan_retur.[user id] = '" & cboUserId.Text.Trim & "' " &
                    '               "And penjualan_retur.[nomor kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    'rDr = .ExecuteReader

                    'While rDr.Read
                    '    If rDr.HasRows = True Then
                    '        nRetur = Val(rDr.Item(0).ToString.Trim)
                    '    End If
                    'End While
                    'rDr.Close()


                    'hitung jumlah transaksi total tunai
                    .CommandText = "SELECT sum(penjualan_detail.[qty] * penjualan_detail.[harga jual]) as [cash actual] " &
                                   "FROM penjualan_detail penjualan_detail " &
                                   "INNER Join penjualan penjualan " &
                                   "ON penjualan_detail.[nomor resep] = penjualan.[nomor resep] WHERE Day(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(penjualan.[tgl resep])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And penjualan.[cara bayar] = 'Tunai' And penjualan.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And penjualan.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    rDr = .ExecuteReader


                    While rDr.Read
                        If rDr.HasRows = True Then
                            nTotalTunai = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    nTotalTunai = Val(nTotalTunai) - Val(nRetur)

                    'cetak
                    cPrint = cPrint &
                    "JML TRANSAKSI   : " & SubStringHarga(FormatNumber(Val(nJmlTransaksi), 0)) & vbCrLf &
                    "TOTAL TUNAI     : " & SubStringHarga(FormatNumber(Val(nTotalTunai), 2)) & vbCrLf &
                    "TOTAL KREDIT    : " & SubStringHarga(FormatNumber(Val(0), 2)) & vbCrLf

                    'hitung jumlah transaksi pembayaran menggunakan kartu
                    .CommandText = "SELECT sum(penjualan_detail.[qty] * penjualan_detail.[harga jual]) as [cash actual] " &
                                   "FROM penjualan_detail penjualan_detail " &
                                   "INNER Join penjualan penjualan " &
                                   "ON penjualan_detail.[nomor resep] = penjualan.[nomor resep] WHERE Day(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(penjualan.[tgl resep])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And penjualan.[cara bayar] = 'Kartu' And penjualan.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And penjualan.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nTotalKartu = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()





                    cPrint = cPrint &
                    "TOTAL KARTU     : " & SubStringHarga(FormatNumber(Val(nTotalKartu), 2)) & vbCrLf &
                    "----------------------------------------" & vbCrLf &
                    "TUNAI Shift " & grdRekapAdmin.CurrentRow.Cells.Item(1).Value.ToString.Trim & "   : " & SubStringHarga(FormatNumber(Val(nTotalTunai), 2)) & vbCrLf &
                    "----------------------------------------" & vbCrLf &
                    "MODAL AWAL      : " & SubStringHarga(FormatNumber(Val(grdRekapAdmin.CurrentRow.Cells.Item(3).Value.ToString.Trim), 2)) & vbCrLf &
                    "MODAL LACI      : " & SubStringHarga(FormatNumber(Val(0), 2)) & vbCrLf &
                    "----------------------------------------" & vbCrLf &
                    "GRAND TUNAI     : " & SubStringHarga(FormatNumber(Val(nTotalTunai), 2)) & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf

                    'total penjualan
                    .CommandText = "SELECT sum((penjualan_detail.[qty] * penjualan_detail.[harga jual]) - penjualan_detail.[discount]) as [total jual] " &
                                   "FROM penjualan_detail penjualan_detail " &
                                   "INNER Join penjualan penjualan " &
                                   "ON penjualan_detail.[nomor resep] = penjualan.[nomor resep] WHERE Day(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(penjualan.[tgl resep])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And penjualan.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And penjualan.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nTotalPenjualan = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    cPrint = cPrint &
                    "TOTAL PENJUALAN : " & SubStringHarga(FormatNumber(Val(nTotalPenjualan), 2)) & vbCrLf


                    'total pengeluaran
                    .CommandText = "SELECT SUM(pengeluaran.[pengeluaran]) as [total pengeluaran] " &
                                   "From pengeluaran WHERE Day(pengeluaran.[tgl transaksi]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(pengeluaran.[tgl transaksi]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(pengeluaran.[tgl transaksi])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And pengeluaran.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And pengeluaran.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nTotalPengeluaran = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    cPrint = cPrint &
                    "DANA KELUAR     : " & SubStringHarga(FormatNumber(Val(nTotalPengeluaran), 2)) & vbCrLf


                    cPrint = cPrint &
                    "RETUR           : " & SubStringHarga(FormatNumber(Val(nRetur), 2)) & vbCrLf



                    'total diskon
                    .CommandText = "SELECT sum(penjualan_detail.[discount]) as [total discount] " &
                                   "FROM penjualan_detail penjualan_detail " &
                                   "INNER Join penjualan penjualan " &
                                   "ON penjualan_detail.[nomor resep] = penjualan.[nomor resep] WHERE Day(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Day & " " &
                                   "And month(penjualan.[tgl resep]) = " & dtTransaksi.DateTime.Month & " " &
                                   "And year(penjualan.[tgl resep])  = " & dtTransaksi.DateTime.Year & " " &
                                   "And penjualan.[user id] = '" & cboUserId.Text.Trim & "' " &
                                   "And penjualan.[kode kasir] = '" & grdRekapAdmin.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"

                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nTotalDiskon = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    cPrint = cPrint &
                    "DISKON          : " & SubStringHarga(FormatNumber(Val(nTotalDiskon), 2)) & vbCrLf
                    nGrandTotal = Val(nTotalPenjualan) - (Val(nTotalPengeluaran) + Val(nTotalDiskon))

                    cPrint = cPrint &
                    "GRAND TOTAL     : " & SubStringHarga(FormatNumber(Val(nGrandTotal), 2)) & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    "KAS AKTUAL      : " & SubStringHarga(FormatNumber(Val(grdRekapAdmin.CurrentRow.Cells.Item(4).Value.ToString), 2)) & vbCrLf

                    nVariant = Val(grdRekapAdmin.CurrentRow.Cells.Item(4).Value.ToString) - (Val(nTotalTunai) - Val(nTotalPengeluaran))

                    cPrint = cPrint &
                    "VARIANT         : " & SubStringHarga(FormatNumber(Val(nVariant), 2)) & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " Supervisor                   Kasir" & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    "(............)            (...........)" & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf &
                    " " & vbCrLf

                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, cPrint)
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub
    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        CetakRekapAdmin()
    End Sub

    Private Function SubStringHarga(ByVal cHarga As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cHarga) = 21 Then
            Return cHarga.Trim
        Else
            For x = 1 To 21 - Val(Len(cHarga.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cHarga.Trim
        End If
    End Function
End Class