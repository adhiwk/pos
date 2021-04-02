Imports System.Data.SqlClient
Public Class frmListPiutang
    Friend cKodeCust As String
    Friend cNamaCust As String
    Private Sub frmListPiutang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Text = "LIST PIUTANG : " & cNamaCust.Trim
    End Sub

    Private Sub LoadDataHutang()
        grdPiutang.Rows.Clear()

        Dim nBayar As Decimal = 0
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                'untuk data yang sudah dibayar
                .CommandText = "SELECT piutang.[nomor nota]," &
                               "piutang.[tgl nota],piutang.[piutang]," &
                               "pembayaran_piutang.[bayar] " &
                               "FROM piutang piutang " &
                               "INNER JOIN pembayaran_piutang " &
                               "pembayaran_piutang ON piutang.[nomor nota] = pembayaran_piutang.[nomor nota] " &
                               "WHERE piutang.[kode cust] = '" & cKodeCust.Trim & "'"

                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    nBayar = Decimal.Parse(HitungSisa(rDr.Item(0).ToString.Trim, Decimal.Parse(rDr.Item(2).ToString.Trim)))
                    If Decimal.Parse(rDr.Item(2).ToString.Trim) > nBayar Then
                        grdPiutang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                            Format(Date.Parse(rDr.Item(1).ToString.Trim), "yyyy-MM-dd"),
                                            FormatNumber(Decimal.Parse(rDr.Item(2).ToString.Trim), 0),
                                            FormatNumber(nBayar, 0)})
                    End If
                End While
                rDr.Close()

                .CommandText = "SELECT [nomor nota],[tgl nota],[piutang] " &
                               "FROM  piutang WHERE [kode cust] = '" & cKodeCust.Trim &
                               "' And  [nomor nota] Not IN (SELECT [nomor nota] FROM pembayaran_piutang)"
                rDr = .ExecuteReader
                While rDr.Read
                    grdPiutang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                            Format(Date.Parse(rDr.Item(1).ToString.Trim), "yyyy-MM-dd"),
                                            FormatNumber(Decimal.Parse(rDr.Item(2).ToString.Trim), 0),
                                            FormatNumber(Decimal.Parse(rDr.Item(2).ToString.Trim), 0)})
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
    End Sub

    Private Sub frmListPiutang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LoadDataHutang()
    End Sub

    Private Function HitungSisa(ByVal cNomorNota As String, ByVal nPiutang As Decimal) As Decimal
        Dim nSisa As Decimal = 0
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "SELECT sum([bayar]) as jml_bayar from pembayaran_piutang " &
                               "WHERE [nomor nota] = '" & cNomorNota.Trim & "'"
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    nSisa = Decimal.Parse(nPiutang) - Decimal.Parse(rDr.Item(0).ToString.Trim)
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
        Return nSisa
    End Function

    Private Sub grdPiutang_Click(sender As Object, e As EventArgs) Handles grdPiutang.Click
        Me.Dispose()
    End Sub

    Private Sub grdPiutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdPiutang.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.Dispose()
        End If
    End Sub
End Class