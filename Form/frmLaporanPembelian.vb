Imports System.Data.SqlClient
Public Class frmLaporanPembelian


    Dim nSubTotal As Decimal = 0
    Dim nTotalDiscount As Decimal = 0
    Dim nTotalPajak As Decimal = 0

    Private Sub frmLaporanPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboJenisLaporan.Properties.Items.Clear()
        cboNamaSuplier.Properties.Items.Clear()
        cboProdusen.Properties.Items.Clear()

        With cboJenisLaporan
            .Properties.Items.Add("laporan periodik pembelian")
            .Properties.Items.Add("laporan periodik pembelian by suplier")
            .Properties.Items.Add("laporan periodik pembelian detail")
            .Properties.Items.Add("laporan periodik pembelian detail by suplier")
            .Properties.Items.Add("laporan periodik pembelian detail by produsen")
            .Properties.Items.Add("laporan periodik pembelian detail by item")
        End With

        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama suplier] " &
                                   "from suplier order by [nama suplier] asc"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cboNamaSuplier.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()

                    .CommandText = "select [nama produsen] from produsen order by [nama produsen] asc"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cboProdusen.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboNamaSuplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNamaSuplier.SelectedIndexChanged
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode suplier] " &
                               "from suplier where [nama suplier] = '" &
                               cboNamaSuplier.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtKodeSuplier.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Dim cQuery As String

        If cboJenisLaporan.Text.Trim = "laporan periodik pembelian" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            cQuery = "select [nomor faktur],[tgl faktur],[kode suplier]," &
                     "[biaya lain],[total pajak],[uang muka],[saldo terhutang] " &
                     "from pembelian where [tgl faktur] between '" &
                     dtAwal.DateTime.Year.ToString.Trim & "/" &
                     dtAwal.DateTime.Month.ToString.Trim & "/" &
                     dtAwal.DateTime.Day.ToString.Trim & "' and '" &
                     dtAkhir.DateTime.Year.ToString.Trim & "/" &
                     dtAkhir.DateTime.Month.ToString.Trim & "/" &
                     dtAkhir.DateTime.Day.ToString.Trim & "'"

            LoadData(cQuery)


            frmCetakLaporan.LoadReportSQL("rekap_pembelian_summary.rpt", "")
            frmCetakLaporan.WindowState = FormWindowState.Maximized

            xForm.Dispose()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian by suplier" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            cQuery = "select [nomor faktur],[tgl faktur],[kode suplier]," &
                     "[biaya lain],[total pajak],[uang muka],[saldo terhutang] " &
                     "from pembelian where [tgl faktur] between '" &
                     dtAwal.DateTime.Year.ToString.Trim & "/" &
                     dtAwal.DateTime.Month.ToString.Trim & "/" &
                     dtAwal.DateTime.Day.ToString.Trim & "' and '" &
                     dtAkhir.DateTime.Year.ToString.Trim & "/" &
                     dtAkhir.DateTime.Month.ToString.Trim & "/" &
                     dtAkhir.DateTime.Day.ToString.Trim & "' and " &
                     "[kode suplier] = '" & txtKodeSuplier.Text.Trim & "'"



            LoadData(cQuery)


            frmCetakLaporan.LoadReportSQL("rekap_pembelian_summary.rpt", "")
            frmCetakLaporan.WindowState = FormWindowState.Maximized

            xForm.Dispose()

            frmCetakLaporan.ShowDialog()

        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_pembelian_detail_all.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ")")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail by suplier" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_pembelian_by_suplier.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") and {pembelian.kode suplier} = '" & txtKodeSuplier.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail by item" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_pembelian_by_suplier.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") and {pembelian_detail.kode obat} = '" & txtKodeObat.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail by produsen" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_pembelian_by_produsen.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") and {obat.kode produsen} = '" & txtKodeProdusen.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        End If
    End Sub

    Private Sub LoadData(ByVal cQueryString As String)
        Dim strDate As String = ""
        Dim dDate As Date = Now.Date
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn

                    .CommandText = "delete from pembelian_rekap"
                    .ExecuteNonQuery()

                    .CommandText = cQueryString.Trim
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        HitungSubTotal(rDr.Item(0).ToString.Trim)

                        dDate = DateTime.Parse(rDr.Item(1).ToString.Trim)
                        strDate = dDate.Year.ToString.Trim & "-" & dDate.Month.ToString.Trim & "-" & dDate.Day.ToString.Trim

                        'wait for 100 milisecond
                        For x = 1 To 100
                        Next x

                        SaveData(rDr.Item(0).ToString.Trim,
                                 strDate.Trim,
                                 rDr.Item(2).ToString.Trim,
                                 Decimal.Parse(Val(nSubTotal)),
                                 Decimal.Parse(Val(nTotalDiscount)),
                                 Decimal.Parse(Val(nTotalPajak)),
                                 Decimal.Parse(Val(rDr.Item(5).ToString.Trim)),
                                 Decimal.Parse(Val(rDr.Item(6).ToString.Trim)))


                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub HitungSubTotal(ByVal cNomorFaktur As String)
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select sum(([qty]*[harga pokok pembelian]) - discount) as [sub total]," &
                                   "sum([discount]) as [total discount]," &
                                   "sum([besar pajak]) as [total pajak] " &
                                   "from pembelian_detail  where [nomor faktur] = '" &
                                    cNomorFaktur.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        nSubTotal = Val(Decimal.Parse(rDr.Item(0).ToString.Trim))
                        nTotalDiscount = Val(Decimal.Parse(rDr.Item(1).ToString.Trim))
                        nTotalPajak = Val(Decimal.Parse(rDr.Item(2).ToString.Trim))
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveData(ByVal cNomorFaktur As String,
                         ByVal cTglFaktur As String,
                         ByVal cKodeSuplier As String,
                         ByVal nSubTotal As Decimal,
                         ByVal nDiscount As Decimal,
                         ByVal nTotalPajak As Decimal,
                         ByVal nTerbayar As Decimal,
                         ByVal nSisaHutang As Decimal)


        'MsgBox("insert into pembelian_rekap " &
        '                           "([nomor faktur],[tgl faktur],[kode suplier],[sub total]," &
        '                           "[discount],[total pajak],[terbayar],[sisa hutang]) values ('" &
        '                           cNomorFaktur.Trim & "','" &
        '                           cTglFaktur.Trim & "','" &
        '                           cKodeSuplier.Trim & "'," &
        '                           Decimal.Parse(Val(nSubTotal)) & "," &
        '                           Decimal.Parse(Val(nDiscount)) & "," &
        '                           Decimal.Parse(Val(nTotalPajak)) & "," &
        '                           Decimal.Parse(Val(nTerbayar)) & "," &
        '                           Decimal.Parse(Val(nSisaHutang)) & ")")


        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    '.CommandText = "insert into pembelian_rekap " &
                    '                 "([nomor faktur],[tgl faktur],[kode suplier],[sub total]," &
                    '                 "[discount],[total pajak],[terbayar],[sisa hutang]) values ('" &
                    '                   cNomorFaktur.Trim & "','" &
                    '                   cTglFaktur.Trim & "','" &
                    '                   cKodeSuplier.Trim & "'," &
                    '                   Decimal.Parse(Val(nSubTotal)) & "," &
                    '                   Decimal.Parse(Val(nDiscount)) & "," &
                    '                   Decimal.Parse(Val(nTotalPajak)) & "," &
                    '                   Decimal.Parse(Val(nTerbayar)) & "," &
                    '                   Decimal.Parse(Val(nSisaHutang)) & ")"


                    .CommandText = "add_pembelian_rekap"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output


                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = cNomorFaktur.Trim
                    .Parameters.Add(New SqlParameter("@tgl_faktur", SqlDbType.NChar, 10)).Value = cTglFaktur.Trim
                    .Parameters.Add(New SqlParameter("@kode_suplier", SqlDbType.NChar, 5)).Value = cKodeSuplier.Trim
                    .Parameters.Add(New SqlParameter("@sub_total", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(Val(nSubTotal))
                    .Parameters.Add(New SqlParameter("@discount", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(Val(nDiscount))
                    .Parameters.Add(New SqlParameter("@total_pajak", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(Val(nTotalPajak))
                    .Parameters.Add(New SqlParameter("@terbayar", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(Val(nTerbayar))
                    .Parameters.Add(New SqlParameter("@sisa_hutang", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(Val(nSisaHutang))

                    .ExecuteNonQuery()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub txtNamaObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaObat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariObat As New frmCariObat
                Try
                    cfrmCariObat.txtCariBarang.Text = txtNamaObat.Text.Trim
                    cfrmCariObat.ShowDialog()
                    txtNamaObat.Text = cfrmCariObat.grdCariBarang.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeObat.Text = cfrmCariObat.grdCariBarang.CurrentRow.Cells.Item(1).Value.ToString.Trim

                Catch ex As Exception

                End Try
            End Using
        End If
    End Sub

    Private Sub cboProdusen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProdusen.SelectedIndexChanged
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode produsen] " &
                                   "from produsen where [nama produsen] = '" &
                                   cboProdusen.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtKodeProdusen.Text = rDr.Item(0).ToString.Trim
                    End While

                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class