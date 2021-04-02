Imports System.Data.SqlClient
Public Class frmTampungPenjualan
    Friend cUserId As String
    Friend cShift As String
    Friend cTampungAction As String
    Friend cKodeTampung As String
    Friend cCustName As String
    Private Sub btnLoadTampung_Click(sender As Object, e As EventArgs) Handles btnLoadTampung.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "load_tampung") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If
        Try
            cTampungAction = "Tampung-Load"
            cKodeTampung = grdTampung.CurrentRow.Cells.Item(0).Value.ToString.Trim
            Me.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmTampungPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTampung()
        cTampungAction = ""
        cKodeTampung = ""
        txtCustName.Text = cCustName.Trim
        grdTampung.Focus()
    End Sub

    Private Sub btnTampung_Click(sender As Object, e As EventArgs) Handles btnTampung.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "add_tampung") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If txtCustName.Text.Trim = "" Then
            MsgBox("isi terlebih dahulu nama customer", MsgBoxStyle.Critical, "error")
            txtCustName.Focus()
            Exit Sub
        End If
        cTampungAction = "Tampung-Add"
        Me.Dispose()
    End Sub

    Private Sub LoadTampung()
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = "select [nomor tampung]," &
                                   "[tgl nota]," &
                                   "[user id]," &
                                   "[cust name] " &
                                   "from penjualan_tampung where [user id] = '" & cUserId.Trim & "' " &
                                   "And day([tgl nota]) = day(getdate()) " &
                                   "And month([tgl nota]) = month(getdate()) " &
                                   "And year([tgl nota]) = year(getdate()) And [status] = 'T'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    Dim nTotal As Integer = 0
                    Dim dTanggal As Date
                    While rDr.Read
                        dTanggal = Date.Parse(rDr.Item(1).ToString.Trim)
                        grdTampung.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                          FormatDateTime(dTanggal, DateFormat.ShortDate),
                                                          rDr.Item(2).ToString.Trim,
                                                          rDr.Item(3).ToString.Trim})
                    End While
                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub

    Private Sub frmTampungPenjualan_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = "27" Then
            cTampungAction = "Tampung-Cancel"
            Me.Dispose()
        ElseIf e.KeyValue.ToString = "113" Then
            cTampungAction = "Tampung-Add"
            Me.Hide()
        ElseIf e.KeyValue.ToString = "114" Then
            cTampungAction = "Tampung-Load"
            cKodeTampung = grdTampung.CurrentRow.Cells.Item(0).Value.ToString.Trim
            Me.Hide()
        End If
    End Sub

    Private Sub grdTampung_Click(sender As Object, e As EventArgs) Handles grdTampung.Click
        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = "select [kode barang],[nama barang],[qty],[harga jual],[sub total] from penjualan_tampung_detail " &
                                   "where [nomor tampung] = '" & grdTampung.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    grdTampungDetail.Rows.Clear()
                    While rDr.Read
                        grdTampungDetail.Rows.Add(New Object() {
                                                           rDr.Item(0).ToString.Trim,
                                                           rDr.Item(1).ToString.Trim,
                                                           FormatNumber(Val(rDr.Item(2).ToString.Trim), 0),
                                                           FormatNumber(Val(rDr.Item(3).ToString.Trim), 2),
                                                           FormatNumber(Val(rDr.Item(4).ToString.Trim), 2)
                                                  })
                    End While
                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        conn.Close()
    End Sub
End Class