Imports System.Data.SqlClient
Public Class frmLaporanStokMinimum
    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        Dim nStok As Decimal = 0

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "delete from obat_laporan_stok_minimum"
                .ExecuteNonQuery()


                .CommandText = "select [kode obat],[harga pokok pembelian],[stok minimum] from obat order by [nama obat] asc"
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read

                    If rDr.Item(2).ToString.Trim <> String.Empty Then

                        nStok = Val(Decimal.Parse(clsPublic.CekStok(rDr.Item(0).ToString.Trim)))

                        If Val(Decimal.Parse(rDr.Item(2).ToString.Trim)) >= Val(nStok) Then
                            SaveStok(rDr.Item(0).ToString.Trim,
                                     Decimal.Parse(rDr.Item(1).ToString.Trim),
                                     Decimal.Parse(rDr.Item(2).ToString.Trim),
                                     Decimal.Parse(nStok))
                        End If

                        For x = 1 To 100

                        Next
                    End If
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()

        xForm.Close()
    End Sub

    Private Function SaveStok(ByVal cKodeObat As String,
                                ByVal nHpp As Decimal,
                                ByVal nStokMinimum As Decimal,
                                ByVal nStok As Decimal) As Boolean

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd

                    .Connection = Conn
                    .CommandText = "laporan_stok_minimum"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_obat", SqlDbType.NChar, 10)).Value = cKodeObat.Trim
                    .Parameters.Add(New SqlParameter("@harga_pokok_pembelian", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(nHpp)
                    .Parameters.Add(New SqlParameter("@stok_minimum", SqlDbType.Decimal, 18, 2)).Value = Decimal.Parse(nStokMinimum)
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

        frmCetakLaporan.LoadReportSQL("laporan_stok_minimum.rpt", "")

        xForm.Dispose()
        frmCetakLaporan.WindowState = FormWindowState.Maximized
        frmCetakLaporan.ShowDialog()
    End Sub
End Class