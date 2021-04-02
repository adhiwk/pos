Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class ctrlLaporanPembelian
    Dim nSubTotal As Decimal = 0
    Dim nTotalDiscount As Decimal = 0
    Dim nTotalPajak As Decimal = 0
#Region "Reporting"
    Private Function LoadReportSQL(ByVal cReportName As String, ByVal cFormula As String) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo

        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load("" & Application.StartupPath & "\laporan\" & cReportName)
        If cFormula.Trim <> "0" Then
            cryRpt.RecordSelectionFormula = cFormula.Trim
        End If

        With crConnectionInfo
            .ServerName = My.Settings.server.Trim
            .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
            .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
            .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.RefreshReport()
        Return True
    End Function

    Private Function LoadReportSQL2Parameter(ByVal cReportName As String, ByVal cFormula As String, dtAwal As Date, dtAkhir As Date) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load(String.Format("{0}\laporan\{1}", Application.StartupPath, cReportName))

        With crConnectionInfo
            .ServerName = My.Settings.server.Trim
            .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
            .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
            .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        cryRpt.SetParameterValue("dtAwal", Date.Parse(dtAwal))
        cryRpt.SetParameterValue("dtAkhir", Date.Parse(dtAkhir))

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.Refresh()
        Return True
    End Function

    Private Function LoadReportSQL3Parameter(ByVal cReportName As String, ByVal cFormula As String, cString As String, dtAwal As Date, dtAkhir As Date) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load(String.Format("{0}\laporan\{1}", Application.StartupPath, cReportName))

        With crConnectionInfo
            .ServerName = My.Settings.server.Trim
            .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
            .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
            .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        cryRpt.SetParameterValue("cString", cString.Trim)
        cryRpt.SetParameterValue("dtAwal", Date.Parse(dtAwal))
        cryRpt.SetParameterValue("dtAkhir", Date.Parse(dtAkhir))

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.Refresh()
        Return True
    End Function
#End Region
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
            LoadReportSQL("rekap_pembelian_summary.rpt", "")
            xForm.Dispose()
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
            LoadReportSQL("rekap_pembelian_summary.rpt", "")
            xForm.Dispose()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL("rekap_pembelian_detail_all.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ")")
            xForm.Close()
        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail by suplier" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL("rekap_pembelian_by_suplier.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") and {pembelian.kode suplier} = '" & txtKodeSuplier.Text.Trim & "'")
            xForm.Close()

        ElseIf cboJenisLaporan.Text.Trim = "laporan periodik pembelian detail by item" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL("rekap_pembelian_by_suplier.rpt", "{pembelian.tgl faktur} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") and {pembelian_detail.plu} = '" & txtKodeObat.Text.Trim & "'")
            xForm.Close()
        End If
    End Sub

    Private Sub ctrlLaporanPembelian_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        cboJenisLaporan.Properties.Items.Clear()
        cboNamaSuplier.Properties.Items.Clear()

        With cboJenisLaporan
            .Properties.Items.Add("laporan periodik pembelian")
            .Properties.Items.Add("laporan periodik pembelian by suplier")
            .Properties.Items.Add("laporan periodik pembelian detail")
            .Properties.Items.Add("laporan periodik pembelian detail by suplier")
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

    Private Sub txtNamaObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaObat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariObat As New frmCariObat
                Try
                    cfrmCariObat.txtCariBarang.Text = txtNamaObat.Text.Trim
                    cfrmCariObat.ShowDialog()
                    txtNamaObat.Text = cfrmCariObat.grdCariBarang.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    txtKodeObat.Text = cfrmCariObat.grdCariBarang.CurrentRow.Cells.Item(2).Value.ToString.Trim
                Catch ex As Exception

                End Try
            End Using
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
        frmMenuUtama.tmMenu.StartTransition(frmMenuUtama.pnlApp)
        Me.Hide()
        frmMenuUtama.tmMenu.EndTransition()
    End Sub
End Class
