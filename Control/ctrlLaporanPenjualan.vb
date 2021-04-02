Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class ctrlLaporanPenjualan
#Region "Form Action"
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        frmMenuUtama.tmMenu.StartTransition(frmMenuUtama.pnlApp)
        Me.Hide()
        frmMenuUtama.tmMenu.EndTransition()
    End Sub

    Private Sub ctrlLaporanPenjualan_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        With cboJenisLaporan
            .Properties.Items.Add("rekap periodik")
            .Properties.Items.Add("rekap periodik per kasir")
            .Properties.Items.Add("rekap periodik per kasir per kategori")
            .Properties.Items.Add("rekap periodik per kasir per item")
        End With

        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [user id] " &
                                   "from users order by [user id] asc"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cboUserId.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()


                    .CommandText = "select [nama kategori] from kategori order by [nama kategori] asc"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cboKategori.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()

                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
        dtAwal.Properties.ReadOnly = True
        dtAkhir.Properties.ReadOnly = True
        cboUserId.Properties.ReadOnly = True
        cboBarang.Properties.ReadOnly = True
        cboKategori.Properties.ReadOnly = True
    End Sub
    Private Sub cboNamaObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then


            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama barang] " &
                                   "from barang where [nama barang] like '%" &
                                    cboBarang.Text.Trim & "%'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    cboBarang.Properties.Items.Clear()
                    While rDr.Read
                        cboBarang.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub cboNamaObat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBarang.SelectedIndexChanged
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "select [plu] " &
                               "from barang where [nama barang] = '" &
                                cboBarang.Text.Trim & "'"

                Dim rDr As SqlDataReader = .ExecuteReader

                While rDr.Read
                    txtKodeBarang.Text = rDr.Item(0).ToString.Trim
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
    End Sub

#End Region
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

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If cboJenisLaporan.Text.Trim = "rekap periodik" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL2Parameter("rekap_penjualan_periodik.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ")", Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            xForm.Close()
        ElseIf cboJenisLaporan.Text.trim = "rekap periodik per kasir" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL2Parameter("rekap_penjualan_periodik_kasir.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") " &
                                  "And {penjualan.user id} = '" & cboUserId.Text.Trim & "'", Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            xForm.Close()
        ElseIf cboJenisLaporan.Text.trim = "rekap periodik per kasir per kategori" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL2Parameter("rekap_penjualan_periodik_kategori.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") " &
                                  "And {barang.kode kategori} = '" & txtKategori.Text.Trim & "'", Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            xForm.Close()
        ElseIf cboJenisLaporan.Text.trim = "rekap periodik per kasir per item" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            LoadReportSQL2Parameter("rekap_penjualan_periodik_item.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ") " &
                                  "And {barang.plu} = '" & txtKodeBarang.Text.Trim & "'", Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            xForm.Close()
        End If
    End Sub

    Private Sub cboJenisLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisLaporan.SelectedIndexChanged
        If cboJenisLaporan.Text.Trim = "rekap periodik" Then
            dtAwal.Properties.ReadOnly = False
            dtAkhir.Properties.ReadOnly = False
            cboUserId.Properties.ReadOnly = True
            cboKategori.Properties.ReadOnly = True
            cboBarang.Properties.ReadOnly = True
            cboUserId.Text = ""
            cboKategori.Text = ""
            txtKategori.Text = ""
            cboBarang.Text = ""
            txtKodeBarang.Text = ""
        ElseIf cboJenisLaporan.Text.trim = "rekap periodik per kasir" Then
            dtAwal.Properties.ReadOnly = False
            dtAkhir.Properties.ReadOnly = False
            cboUserId.Properties.ReadOnly = False
            cboKategori.Properties.ReadOnly = True
            cboBarang.Properties.ReadOnly = True
            cboUserId.Text = ""
            cboKategori.Text = ""
            txtKategori.Text = ""
            cboBarang.Text = ""
            txtKodeBarang.Text = ""
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per kategori" Then
            dtAwal.Properties.ReadOnly = False
            dtAkhir.Properties.ReadOnly = False
            cboUserId.Properties.ReadOnly = False
            cboKategori.Properties.ReadOnly = False
            cboBarang.Properties.ReadOnly = True
            cboUserId.Text = ""
            cboKategori.Text = ""
            txtKategori.Text = ""
            cboBarang.Text = ""
            txtKodeBarang.Text = ""
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per item" Then
            dtAwal.Properties.ReadOnly = False
            dtAkhir.Properties.ReadOnly = False
            cboUserId.Properties.ReadOnly = False
            cboKategori.Properties.ReadOnly = True
            cboBarang.Properties.ReadOnly = False
            cboUserId.Text = ""
            cboKategori.Text = ""
            txtKategori.Text = ""
            cboBarang.Text = ""
            txtKodeBarang.Text = ""
        End If
    End Sub
#End Region
End Class
