Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class ctrlLaporanStok
    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd

                .Connection = Conn
                .CommandText = "delete from barang_laporan_stok"
                .ExecuteNonQuery()

                If cboJenisLaporan.Text.Trim = "Laporan Stok Keseluruhan" Then
                    .CommandText = "select [plu],[harga pokok pembelian] from barang order by [nama barang] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read

                        SaveStok(rDr.Item(0).ToString.Trim,
                                 Decimal.Parse(rDr.Item(1).ToString.Trim),
                                 Decimal.Parse(clsPublic.CekStok(rDr.Item(0).ToString.Trim)))

                        For x = 1 To 100

                        Next
                    End While
                    rDr.Close()
                End If
            End With
        End Using
        Conn.Close()

        xForm.Close()
    End Sub

    Private Function SaveStok(ByVal cPLU As String,
                                ByVal nHpp As Decimal,
                                ByVal nStok As Decimal) As Boolean

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "add_stock_report"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@plu", SqlDbType.NChar, 15)).Value = cPLU.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(nHpp)
                    .Parameters.Add(New SqlParameter("@jumlah_stok", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(nStok)
                    .ExecuteNonQuery()

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()

        Return True
    End Function

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        LoadReportSQL("laporan_stok.rpt", "")

        xForm.Dispose()
    End Sub

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

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        frmMenuUtama.tmMenu.StartTransition(frmMenuUtama.pnlApp)
        Me.Hide()
        frmMenuUtama.tmMenu.EndTransition()
    End Sub

    Private Sub ctrlLaporanStok_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
    End Sub
End Class
