Imports System.Data.SqlClient
Public Class frmRePrintNota
    Friend cUserId As String

    Dim cNamaToko As String = ""
    Dim cAlamatToko As String = ""
    Dim cKota As String = ""
    Dim cNPWP As String = ""
    Dim cNoTelp As String = ""

    Private Sub frmRePrintNota_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = "27" Then
            Me.Dispose()
        ElseIf e.KeyValue = "113" Then
            CetakNota(grdReprintNota.CurrentRow.Cells.Item(1).Value.ToString.Trim)
        End If
    End Sub

    Private Sub frmRePrintNota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chkNoNota.Checked = True
            chkTglNota.Checked = False
            LoadLastTransaction()
            grdReprintNota.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadLastTransaction()
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "select top 15 [nomor nota],[total] from penjualan " &
                                   "where day([tgl nota]) = day(GETDATE()) " &
                                   "and MONTH([tgl nota]) = MONTH(getdate()) " &
                                   "and YEAR([tgl nota]) = YEAR(getdate()) " &
                                   "and [user id] = '" & cUserId.Trim & "' order by [id] DESC"


                    Dim nUrut As Integer = 1
                    Dim rDr As SqlDataReader = .ExecuteReader
                    grdReprintNota.Rows.Clear()

                    While rDr.Read
                        grdReprintNota.Rows.Add(New Object() {Val(nUrut), rDr.Item(0).ToString.Trim,
                                                            FormatNumber(Val(rDr.Item(1).ToString.Trim), 0)})
                        nUrut = nUrut + 1
                    End While
                    rDr.Close()

                    .CommandText = "select [nama toko],[alamat toko],[kota],[npwp],[no telpon] from header_nota "
                    rDr = .ExecuteReader

                    While rDr.Read
                        cNamaToko = rDr.Item(0).ToString.Trim
                        cAlamatToko = rDr.Item(1).ToString.Trim
                        cKota = rDr.Item(2).ToString.Trim
                        cNPWP = rDr.Item(3).ToString.Trim
                        cNoTelp = rDr.Item(4).ToString.Trim
                    End While

                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
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

    Private Function SubStringQty(ByVal cQty As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cQty) = 3 Then
            Return cQty.Trim
        Else
            For x = 1 To 3 - Val(Len(cQty.Trim))
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

    Private Function SubStringTotal(ByVal cTotal As String) As String
        Dim cSpace As String = " "
        Dim cReturnString As String = ""
        If Len(cTotal) = 15 Then
            Return cTotal.Trim
        Else
            For x = 1 To 15 - Val(Len(cTotal.Trim))
                cReturnString = cReturnString & cSpace
            Next

            Return cReturnString & cTotal.Trim
        End If
    End Function

    Private Sub CetakNota(ByVal cNomorNota As String)

        Dim DPP As Decimal = 0
        Dim BKP As Decimal = 0
        Dim BTKP As Decimal = 0
        Dim PPN As Decimal = 0
        Dim nBayar, nKembali As Decimal
        Dim nTotal As Decimal

        Dim cPrint As String =
            clsPublic.AlignToCenter(cNamaToko.Trim) & vbCrLf &
            clsPublic.AlignToCenter(cAlamatToko.Trim) & vbCrLf &
            clsPublic.AlignToCenter(cNoTelp.Trim) & vbCrLf &
            clsPublic.AlignToCenter(cKota.Trim) & vbCrLf &
            clsPublic.AlignToCenter(cNPWP.Trim) & vbCrLf &
            "--------------------------------------" & vbCrLf &
            "No  : " & cNomorNota.Trim & " [ COPY ]" & vbCrLf &
            "Jam : " & Now.Hour & ":" & Now.Minute & ":" & Now.Second & vbCrLf &
            "--------------------------------------" & vbCrLf

        Dim nSubTotal As Decimal = 0
        Dim nPPN As Decimal = 0
        Dim nDpp As Decimal = 0

        nTotal = 0


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

        'select [plu],[short name],[qty],[harga jual],[disc_total],[ppn keluar]

        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "SELECT penjualan_detail.[plu],barang.[short name]," &
                                   "penjualan_detail.[qty],penjualan_detail.[harga jual]," &
                                   "penjualan_detail.[disc_total],penjualan_detail.[ppn keluar],penjualan_detail.[satuan jumlah] " &
                                   "FROM penjualan_detail penjualan_detail  INNER JOIN barang " &
                                   "barang ON penjualan_detail.[plu] = barang.[plu] " &
                                   "WHERE penjualan_detail.[nomor nota] = '" & grdReprintNota.CurrentRow.Cells.Item(1).Value.ToString.Trim & "'"


                    Dim nUrut As Integer = 1
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        'untuk perhitungan pajak
                        If Val(rDr.Item(5).ToString.Trim) > 0 Then
                            nDpp = (Val(rDr.Item(2).ToString.Trim) * Val(rDr.Item(3).ToString.Trim)) / 1.1
                            DPP = Val(DPP) + Val(nDpp)

                            BKP = Val(BKP) + (Val(rDr.Item(2).ToString.Trim) * Val(rDr.Item(3).ToString.Trim))
                        ElseIf Val(rDr.Item(5).ToString.Trim) = 0 Then
                            BTKP = Val(BTKP) + (Val(rDr.Item(2).ToString.Trim) * Val(rDr.Item(3).ToString.Trim))
                        End If


                        nPPN = Val(rDr.Item(2).ToString.Trim) * Val(rDr.Item(5).ToString.Trim)
                        PPN = Val(nPPN) + PPN

                        nSubTotal = (Val(rDr.Item(2).ToString.Trim) * Val(rDr.Item(3).ToString.Trim)) - Val(rDr.Item(4).ToString.Trim)
                        nTotal = Val(nTotal) + Val(nSubTotal)

                        'RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, DataRow.Cells(1).Value.ToString.Trim & vbCrLf)
                        'SubStringDiscount(FormatNumber(DataRow.Cells(5).Value.ToString.Trim, 0)) &

                        'RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, SubStringItem(rDr.Item(1).ToString.Trim) & " " &
                        '                             SubStringQty(FormatNumber(Val(rDr.Item(2).ToString.Trim), 2)) & " " &
                        '                             SubStringHarga(FormatNumber(Val(rDr.Item(3).ToString.Trim), 2)) &
                        '                             SubStringTotal(FormatNumber(Val(nSubTotal), 2)) & vbCrLf)

                        cPrint = cPrint & SubStringPlu(rDr.Item(0).ToString.Trim) & SubStringItem(rDr.Item(1).ToString.Trim) & vbCrLf

                        cPrint = cPrint & "   " &
                                                     SubStringQty(FormatNumber(Val(rDr.Item(2).ToString.Trim), 2)) & " " &
                                                     SubStringPcs(rDr.Item(6).ToString.Trim) & "   " &
                                                     SubStringHarga("@" & FormatNumber(Val(rDr.Item(3).ToString.Trim), 2)) & " " &
                                                     SubStringTotal(FormatNumber(Val(nSubTotal), 2)) & vbCrLf
                    End While
                    rDr.Close()


                    .CommandText = "select [bayar],[kembali] from penjualan where [nomor nota] = '" &
                                   grdReprintNota.CurrentRow.Cells.Item(1).Value.ToString.Trim & "'"
                    rDr = .ExecuteReader

                    While rDr.Read
                        nBayar = Val(rDr.Item(0).ToString.Trim)
                        nKembali = Val(rDr.Item(1).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()

        cPrint = cPrint &
            "---------------------------------------" & vbCrLf &
            "Sub Total : " & SubStringTotal(FormatNumber(Val(nTotal), 2)) & vbCrLf &
            "Bayar     : " & SubStringTotal(FormatNumber(Val(nBayar), 2)) & vbCrLf &
            "Kembali   : " & SubStringTotal(FormatNumber(Val(nKembali), 2)) & vbCrLf &
            " " & vbCrLf &
            "DPP  :" & SubStringTotal(FormatNumber(Val(DPP), 2)) & vbCrLf &
            "PPN  :" & SubStringTotal(FormatNumber(Val(PPN), 2)) & vbCrLf &
            "BKP  :" & SubStringTotal(FormatNumber(Val(BKP), 2)) & vbCrLf &
            "BTKP :" & SubStringTotal(FormatNumber(Val(BTKP), 2)) & vbCrLf &
            " " & vbCrLf &
            "    Terima kasih atas kunjungan anda  " & vbCrLf &
            " kritik dan saran SMS ke 087739854195 " & vbCrLf &
            " " & vbCrLf &
            " " & vbCrLf &
            " " & vbCrLf &
            " " & vbCrLf &
            " " & vbCrLf &
            " " & vbCrLf

        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, cPrint)
    End Sub


    Private Sub grdReprintNota_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdReprintNota.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                CetakNota(grdReprintNota.CurrentRow.Cells.Item(1).Value.ToString.Trim)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Try
            CetakNota(grdReprintNota.CurrentRow.Cells.Item(1).Value.ToString.Trim)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCariNomorNota_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariNomorNota.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim conn As SqlConnection = clsPublic.KoneksiSQL
            conn.Open()

            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = conn
                        .CommandText = "select [nomor nota],[total] from penjualan " &
                                       "where [nomor nota] = '" & txtCariNomorNota.Text.Trim & "' " &
                                       "and [user id] = '" & cUserId.Trim & "' order by [id] DESC"

                        Dim nUrut As Integer = 1
                        Dim rDr As SqlDataReader = .ExecuteReader
                        grdReprintNota.Rows.Clear()

                        While rDr.Read
                            grdReprintNota.Rows.Add(New Object() {Val(nUrut), rDr.Item(0).ToString.Trim,
                                                                FormatNumber(Val(rDr.Item(1).ToString.Trim), 0)})
                            nUrut = nUrut + 1
                        End While
                        rDr.Close()

                        .CommandText = "select [nama toko],[alamat toko],[kota],[npwp],[no telpon] from header_nota "
                        rDr = .ExecuteReader

                        While rDr.Read
                            cNamaToko = rDr.Item(0).ToString.Trim
                            cAlamatToko = rDr.Item(1).ToString.Trim
                            cKota = rDr.Item(2).ToString.Trim
                            cNPWP = rDr.Item(3).ToString.Trim
                            cNoTelp = rDr.Item(4).ToString.Trim
                        End While

                        rDr.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
            conn.Close()
        End If
    End Sub
End Class