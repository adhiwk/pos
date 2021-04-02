Imports System.Data.SqlClient
Public Class frmRekapKasir
    Friend cUserId As String
    Friend cUserName As String

    Dim cNamaToko As String
    Dim cAlamatToko As String
    Dim cKota As String
    Dim cNPWP As String
    Dim cNoTelp As String

    Private Sub frmRekapKasir_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue.ToString.Trim = 27 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub frmRekapKasir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtTransaksi.DateTime = Now.Date
        txtNamaKasir.Text = cUserName.Trim
        'dtTransaksi.Properties.ReadOnly = True
        txtNamaKasir.Properties.ReadOnly = True
        txtModal.Text = ""
        txtModal.Focus()
    End Sub

    Private Sub btnRekap_Click(sender As Object, e As EventArgs) Handles btnRekap.Click

        'cek jika data rekapan tgl tersebut
        'atas user tersebut sudah ada dalam database
        'tolak rekap ulang
        If clsPublic.VerifyData(cUserId.Trim, "select [user id] from rekap_kasir where [user id] = '" &
                                cUserId.Trim & "' and [kode kasir] = '" & My.Settings.kodekasir.Trim & "' and " &
                                "day([tgl transaksi]) = " & dtTransaksi.DateTime.Day & " and " &
                                "month([tgl transaksi]) = " & dtTransaksi.DateTime.Month & " and " &
                                "year([tgl transaksi]) = " & dtTransaksi.DateTime.Year) Then
            MsgBox("Anda sudah melakukan rekap untuk transaksi hari ini", MsgBoxStyle.Critical, "Error")
        Else

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "add_rekap_kasir"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                        .Parameters.Add(New SqlParameter("@user_id", SqlDbType.NChar, 15)).Value = cUserId.Trim
                        .Parameters.Add(New SqlParameter("@tgl_transaksi", SqlDbType.NChar, 10)).Value = clsPublic.ConvertTanggal(dtTransaksi)
                        .Parameters.Add(New SqlParameter("@kode_kasir", SqlDbType.NChar, 3)).Value = My.Settings.kodekasir.Trim
                        .Parameters.Add(New SqlParameter("@shift", SqlDbType.NChar, 10)).Value = frmLogin.cboShift.Text.Trim
                        .Parameters.Add(New SqlParameter("@modal", SqlDbType.Money)).Value = Val(txtModal.Text.Trim)
                        .Parameters.Add(New SqlParameter("@kas_aktual", SqlDbType.Money)).Value = Val(txtKasAktual.Text.Trim)
                        .ExecuteNonQuery()

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using


            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "select [nama toko],[alamat toko],[kota],[npwp],[no telpon] from header_nota"
                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            cNamaToko = rdr.Item(0).ToString.Trim
                            cAlamatToko = rdr.Item(1).ToString.Trim
                            cKota = rdr.Item(2).ToString.Trim
                            cNPWP = rdr.Item(3).ToString.Trim
                            cNoTelp = rdr.Item(4).ToString.Trim
                        End While
                        rdr.Close()

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using

            Conn.Close()

            Dim cPrint =
               clsPublic.AlignToCenter(cNamaToko.Trim) & vbCrLf &
               clsPublic.AlignToCenter(cAlamatToko.Trim) & vbCrLf &
               clsPublic.AlignToCenter(cNoTelp.Trim) & vbCrLf &
               clsPublic.AlignToCenter(cKota.Trim) & vbCrLf &
               clsPublic.AlignToCenter(cNPWP.Trim) & vbCrLf &
                "Report Kasir : " & FormatDateTime(dtTransaksi.DateTime, DateFormat.ShortDate).ToString.Trim & vbCrLf &
                "Kassa " & My.Settings.kodekasir.Trim & vbCrLf &
                "SHIFT : " & frmLogin.cboShift.Text.Trim & vbCrLf &
                "KAS AKTUAL      : " & FormatNumber(Val(txtKasAktual.Text.Trim), 2) & vbCrLf &
                " " & vbCrLf &
                " " & vbCrLf &
                " Supervisor                   Kasir" & vbCrLf &
                " " & vbCrLf &
                " " & vbCrLf &
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

        End If

        Me.Dispose()
    End Sub

    Private Sub RekapFromDatabase()
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "     PT. IJOROYOROYO RETAILINDO      " & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "     Jl. PAKEM-CANGKRINGAN KM 5      " & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "               SLEMAN                " & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "   No. NPWP : 03.292.652.9-542.000   " & vbCrLf)

        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "Report Kasir : " & FormatDateTime(Now.Date, DateFormat.ShortDate).ToString.Trim & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "Kassa " & My.Settings.kodekasir.Trim & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "SHIFT : " & frmLogin.cboShift.Text.Trim & vbCrLf)
        RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "KASIR : " & cUserName.Trim & vbCrLf)

        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    '.CommandText = "SELECT penjualan.[nomor nota],penjualan.[tgl nota]," &
                    '                   "penjualan.[cara bayar],penjualan_detail.[qty]," &
                    '                   "penjualan_detail.[harga jual] " &
                    '                   "FROM penjualan penjualan INNER JOIN penjualan_detail " &
                    '                   "penjualan_detail ON penjualan.[nomor nota]  = penjualan_detail.[nomor nota] " &
                    '                   "WHERE day(penjualan.[tgl nota]) = " & Now.Day & "and month(penjualan.[tgl nota]) =" &
                    '                   Now.Month & " and year(penjualan.[tgl nota])  =" & Now.Year & " and penjualan.[cara bayar] = 'Cash'" &
                    '                   " and penjualan.[user id] = '" & cUserId.Trim & "'"


                    .CommandText = "SELECT SUM(penjualan_detail.[qty] * penjualan_detail.[harga jual]) as [cash actual] " &
                                   "From penjualan penjualan INNER Join penjualan_detail " &
                                   "penjualan_detail ON penjualan.[nomor nota]  = penjualan_detail.[nomor nota] " &
                                   "WHERE Day(penjualan.[tgl nota]) = Day(getdate()) " &
                                   "And month(penjualan.[tgl nota]) = month(getdate()) " &
                                   "And year(penjualan.[tgl nota])  = year(getdate()) " &
                                   "And penjualan.[cara bayar] = 'Cash' And penjualan.[user id] = '" & cUserId.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    Dim nTotal As Integer = 0

                    While rDr.Read
                        If rDr.HasRows = True Then
                            'nTotal = Val(nTotal) + (Val(rDr.Item(3).ToString.Trim) * Val(rDr.Item(4).ToString.Trim))
                            nTotal = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While

                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "KAS AKTUAL      : " & FormatNumber(Val(nTotal), 2) & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " Supervisor                   Kasir" & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, "(............)            (...........)" & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)
                    RawPrinterHelper.SendStringToPrinter(My.Settings.printername.Trim, " " & vbCrLf)

                    rDr.Close()

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub
End Class