Imports System.Data.SqlClient
Public Class frmLaporanStok
    Private Sub frmLaporanStok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With cboJenisLaporan
            .Properties.Items.Add("Laporan Stok Keseluruhan")
            .Properties.Items.Add("Laporan Stok Per Produsen")
        End With
        LoadDataProdusen()
    End Sub

    Private Sub LoadDataProdusen()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "select [nama produsen] from produsen " &
                               "order by [nama produsen] asc"

                cboProdusen.Properties.Items.Clear()
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    cboProdusen.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                End While
                rDr.Close()

            End With
        End Using
        Conn.Close()
    End Sub
    Private Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd

                .Connection = Conn
                .CommandText = "delete from obat_laporan_stok"
                .ExecuteNonQuery()

                If cboJenisLaporan.Text.Trim = "Laporan Stok Keseluruhan" Then
                    .CommandText = "select [kode obat],[harga pokok pembelian] from obat order by [nama obat] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read

                        SaveStok(rDr.Item(0).ToString.Trim,
                                 Decimal.Parse(rDr.Item(1).ToString.Trim),
                                 Decimal.Parse(clsPublic.CekStok(rDr.Item(0).ToString.Trim)))

                        For x = 1 To 100

                        Next
                    End While
                    rDr.Close()

                ElseIf cboJenisLaporan.Text.Trim = "Laporan Stok Per Produsen" Then
                    .CommandText = "select [kode obat],[harga pokok pembelian] from obat where [kode produsen] = '" &
                                    txtProdusen.Text.Trim & "' order by [nama obat] asc"

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

    Private Function SaveStok(ByVal cKodeObat As String,
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
                    .Parameters.Add(New SqlParameter("@kode_obat", SqlDbType.NChar, 10)).Value = cKodeObat.Trim
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

        frmCetakLaporan.LoadReportSQL("laporan_stok.rpt", "")

        xForm.Dispose()
        frmCetakLaporan.WindowState = FormWindowState.Maximized
        frmCetakLaporan.ShowDialog()
    End Sub

    Private Sub cboProdusen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProdusen.SelectedIndexChanged
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "select [kode produsen] from produsen " &
                               "where [nama produsen] = '" & cboProdusen.Text.Trim & "'"

                cboJenisLaporan.Properties.Items.Clear()
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    txtProdusen.Text = rDr.Item(0).ToString.Trim
                End While
                rDr.Close()

            End With
        End Using
        Conn.Close()
    End Sub
End Class