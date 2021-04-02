Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmCetakLaporan
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Dispose()
    End Sub

    Public Function LoadReportSQL(ByVal cReportName As String, ByVal cFormula As String) As Boolean
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

        crViewer_.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer_.SelectionFormula = cFormula.Trim
        End If
        crViewer_.RefreshReport()
        Return True
    End Function

    Public Function LoadReportSQL2Parameter(ByVal cReportName As String, ByVal cFormula As String, dtAwal As Date, dtAkhir As Date) As Boolean
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

        crViewer_.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer_.SelectionFormula = cFormula.Trim
        End If
        crViewer_.Refresh()
        Return True
    End Function

    Public Function LoadReportSQL3Parameter(ByVal cReportName As String, ByVal cFormula As String, cString As String, dtAwal As Date, dtAkhir As Date) As Boolean
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

        crViewer_.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer_.SelectionFormula = cFormula.Trim
        End If
        crViewer_.Refresh()
        Return True
    End Function

    Private Sub FrmCetakLaporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class