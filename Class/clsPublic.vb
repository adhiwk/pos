Imports System.Data.SqlClient
Public Class clsPublic
    Shared Function KoneksiSQL() As SqlClient.SqlConnection
        Dim strServer As String = My.Settings.server.Trim
        Dim strDBname As String = Simple3Des.Decrypt(My.Settings.dbname.Trim)
        Dim strUser As String = Simple3Des.Decrypt(My.Settings.user.Trim)
        Dim strPass As String = Simple3Des.Decrypt(My.Settings.password.Trim)

        Dim connectionString As String _
        = "Data Source='" &
        strServer & "';Initial Catalog='" &
        strDBname & "';User ID='" &
        strUser & "'; Password='" & strPass & "'"

        Return New SqlConnection(connectionString)

    End Function

    Shared Function GetDate() As String
        Dim cTanggal As String = ""
        Dim conn As SqlConnection = clsPublic.KoneksiSQL()
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "select getdate() as [tanggal]"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            cTanggal = Format(Date.Parse(rDr.Item(0).ToString.Trim), "yyyy-MM-dd")
                        End If
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        Return cTanggal.Trim
    End Function

    Shared Function ConvertTanggal(ByVal dtTanggal As DevExpress.XtraEditors.DateEdit) As String
        Dim cBulan As String = ""
        Dim cTanggal As String = ""
        'Dim cTanggalKunjungan As String = ""
        If Len(dtTanggal.DateTime.Month.ToString.Trim) = 1 Then
            cBulan = "0" & dtTanggal.DateTime.Month.ToString.Trim
        Else
            cBulan = dtTanggal.DateTime.Month.ToString.Trim
        End If

        If Len(dtTanggal.DateTime.Day.ToString.Trim) = 1 Then
            cTanggal = "0" & dtTanggal.DateTime.Day.ToString.Trim
        Else
            cTanggal = dtTanggal.DateTime.Day.ToString.Trim
        End If
        ConvertTanggal = dtTanggal.DateTime.Year.ToString.Trim & "-" & cBulan.Trim & "-" & cTanggal.Trim
    End Function

    Shared Function VerifyData(ByVal cKode As String, cQuery As String) As Boolean
        Dim lFind As Boolean = False
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"


        Dim conn As SqlConnection = KoneksiSQL()
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = cQuery.Trim
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            If cKode.Trim = rDr.Item(0).ToString.Trim Then lFind = True
                        End If
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        xForm.Dispose()

        Return lFind
    End Function

    Shared Function xRand() As Long
        Dim r1 As Long = Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim RAND As Long = Math.Max(r1, r1 * 2)
        Return RAND
    End Function

    Shared Function ConvertNumeric(ByVal cNumeric As String) As Decimal
        Dim cDecimal As String = ""
        Dim nDecimal As Decimal
        For x = 1 To Len(cNumeric.Trim)
            If Mid(cNumeric.Trim, x, 1) <> "." Then
                cDecimal = cDecimal & Mid(cNumeric.Trim, x, 1)
            End If
        Next
        nDecimal = Val(cDecimal.Trim)
        Return Val(nDecimal)
    End Function

    Shared Function CekStok(ByVal cKodeBarang As String) As Decimal
        Dim nStok As Decimal = 0
        Dim nStokAwal As Decimal = 0
        Dim nQtyPembelian As Decimal = 0
        Dim nQtyPenjualan As Decimal = 0

        Dim conn As SqlConnection = KoneksiSQL()
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn

                    'load stok
                    .CommandText = "select sum([stok awal])  as [stok] from barang_stok_awal " &
                                   "where [plu] = '" & cKodeBarang.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nStokAwal = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    'load pembelian
                    .CommandText = "select sum([qty]) as [qty pembelian] " &
                                   "from pembelian_detail where [plu] = '" & cKodeBarang.Trim & "'"
                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nQtyPembelian = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()

                    'load penjualan
                    .CommandText = "select  sum([qty]) as [qty penjualan] " &
                                   "from penjualan_detail where [plu] = '" & cKodeBarang.Trim & "'"
                    rDr = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            nQtyPenjualan = Val(rDr.Item(0).ToString.Trim)
                        End If
                    End While
                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
        conn.Close()
        nStok = (Val(nStokAwal) + Val(nQtyPembelian)) - Val(nQtyPenjualan)
        Return Decimal.Parse(nStok)
    End Function

    Shared Function AlignToCenter(ByVal cString As String) As String
        Dim cSpasiKiri As String = ""
        Dim cSpasiKanan As String = ""
        Dim nLength As Integer = 0

        nLength = 30 - Val(Len(cString.Trim))

        If Val(nLength) Mod 2 = 0 Then
        Else
            nLength = Val(nLength) + 1
        End If

        For x = 1 To Val(nLength) / 2
            cSpasiKiri = " " & cSpasiKiri
            cSpasiKanan = " " & cSpasiKanan
        Next

        Return cSpasiKiri & cString.Trim & cSpasiKanan
    End Function

    Shared Function AlignToCenter80(ByVal cString As String) As String
        Dim cSpasiKiri As String = ""
        Dim cSpasiKanan As String = ""
        Dim nLength As Integer = 0

        nLength = 48 - Val(Len(cString.Trim))

        If Val(nLength) Mod 2 = 0 Then
        Else
            nLength = Val(nLength) + 1
        End If

        For x = 1 To Val(nLength) / 2
            cSpasiKiri = " " & cSpasiKiri
            cSpasiKanan = " " & cSpasiKanan
        Next

        Return cSpasiKiri & cString.Trim & cSpasiKanan
    End Function

    Shared Function CekAkses(ByVal cField As String, cUser As String) As Boolean
        Dim lAkses As Boolean
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"


        Dim conn As SqlConnection = KoneksiSQL()
        conn.Open()

        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = conn
                    .CommandText = "select [" & cField.Trim & "] from users " &
                        "where [user id] = '" & cUser.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        If rDr.Item(0).ToString.Trim = "Y" Then
                            lAkses = True
                        Else
                            lAkses = False
                        End If
                    End While
                    rDr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
        xForm.Dispose()
        Return lAkses
    End Function
End Class
