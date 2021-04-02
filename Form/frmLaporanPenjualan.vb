Imports System.Data.SqlClient
Public Class frmLaporanPenjualan
    Private Sub frmLaporanPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.MdiParent = frmMenuUtama

        With cboJenisLaporan
            .Properties.Items.Add("rekap periodik")
            .Properties.Items.Add("rekap periodik per kasir")
            .Properties.Items.Add("rekap periodik per kasir per produsen")
            .Properties.Items.Add("rekap periodik per kasir per kategori")
            .Properties.Items.Add("rekap periodik per kasir per item")
            .Properties.Items.Add("rekap periodik per kasir per jenis per cara bayar")
        End With

        With cboCaraBayar
            .Properties.Items.Add("tunai")
            .Properties.Items.Add("kredit")
        End With

        With cboJenis
            .Properties.Items.Add("paket")
            .Properties.Items.Add("non paket")
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

                    .CommandText = "select [nama produsen] from produsen order by [nama produsen] asc"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cboProdusen.Properties.Items.Add(rDr.Item(0).ToString.Trim)
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


    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Dim cResep As String = ""
        Dim cTunai As String = ""

        If cboJenisLaporan.Text.Trim = "rekap periodik" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL2Parameter("rekap_penjualan_periodik.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & ")", Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL3Parameter("rekap_penjualan_periodik_kasir.rpt", "{penjualan.tgl nota} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & " ) and {penjualan.user id} = '" & cboUserId.Text.Trim & "'",
                                  cboUserId.Text.Trim, Date.Parse(dtAwal.DateTime), Date.Parse(dtAkhir.DateTime))
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per jenis per cara bayar" Then

            If cboJenis.Text.Trim = "resep" Then
                cResep = "Y"
            Else
                cResep = "T"
            End If

            If cboCaraBayar.Text.Trim = "tunai" Then
                cTunai = "Y"
            Else
                cTunai = "T"
            End If

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_penjualan_cara_bayar_jenis.rpt", "{penjualan.tgl resep} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & " ) and {penjualan.user id} = '" & cboUserId.Text.Trim & "' and " &
                                  "{penjualan.resep} = '" & cResep.Trim & "' and {penjualan.tunai} = '" & cTunai.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per produsen" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_penjualan_per_produsen.rpt", "{penjualan.tgl resep} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & " ) and {penjualan.user id} = '" & cboUserId.Text.Trim & "' and " &
                                  "{obat.kode produsen} = '" & txtProdusen.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per kategori" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_penjualan_per_kategori.rpt", "{penjualan.tgl resep} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & " ) and {penjualan.user id} = '" & cboUserId.Text.Trim & "' and " &
                                  "{obat.kode kategori} = '" & txtKategori.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
        ElseIf cboJenisLaporan.Text.Trim = "rekap periodik per kasir per item" Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            frmCetakLaporan.LoadReportSQL("rekap_penjualan_per_item.rpt", "{penjualan.tgl resep} In Date(" & dtAwal.DateTime.Year & ", " &
                                  dtAwal.DateTime.Month & ", " &
                                  dtAwal.DateTime.Day & ") To Date (" &
                                  dtAkhir.DateTime.Year & "," &
                                  dtAkhir.DateTime.Month & "," &
                                  dtAkhir.DateTime.Day & " ) and {penjualan.user id} = '" & cboUserId.Text.Trim & "' and " &
                                  "{penjualan_detail.kode obat} = '" & txtKodeObat.Text.Trim & "'")
            frmCetakLaporan.WindowState = FormWindowState.Maximized
            xForm.Close()

            frmCetakLaporan.ShowDialog()
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
                                   "from produsen where [nama produsen] = '" & cboProdusen.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtProdusen.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()

                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKategori.SelectedIndexChanged
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [kode kategori] " &
                                   "from kategori where [nama kategori] = '" & cboKategori.Text.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtKategori.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboNamaObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboNamaObat.KeyPress
        If Asc(e.KeyChar) = 13 Then


            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama obat] " &
                                   "from obat where [nama obat] like '%" &
                                    cboNamaObat.Text.Trim & "%'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    cboNamaObat.Properties.Items.Clear()
                    While rDr.Read
                        cboNamaObat.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub cboNamaObat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNamaObat.SelectedIndexChanged
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "select [kode obat] " &
                               "from obat where [nama obat] = '" &
                                cboNamaObat.Text.Trim & "'"

                Dim rDr As SqlDataReader = .ExecuteReader

                While rDr.Read
                    txtKodeObat.Text = rDr.Item(0).ToString.Trim
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
    End Sub
End Class