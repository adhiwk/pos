Imports System.Data.SqlClient
Public Class frmPersediaan
    Friend cUserId As String
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmPersediaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frmMenuUtama
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        Dim ds As New DataSet
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama barang],[kode barang]," &
                                   "[harga pokok pembelian],[harga jual toko]," &
                                   "[harga jual grosir] from barang order by [nama barang] asc"

                    Dim da As New SqlDataAdapter(Cmd)
                    da.Fill(ds, "barang")
                    If ds.Tables("barang").Rows.Count > 0 Then
                        grdObat.DataSource = ds.Tables("barang")
                        grdObat.Columns(0).HeaderText = "NAMA BARANG"
                        grdObat.Columns(0).Width = 400
                        grdObat.Columns(1).HeaderText = "KODE BARANG"
                        grdObat.Columns(1).Width = 100
                        grdObat.Columns(2).HeaderText = "HARGA BELI"
                        grdObat.Columns(2).Width = 150
                        grdObat.Columns(2).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(2).FormatString = "{0:###,###.00}"
                        grdObat.Columns(3).HeaderText = "HARGA JUAL TOKO"
                        grdObat.Columns(3).Width = 150
                        grdObat.Columns(3).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(3).FormatString = "{0:###,###.00}"
                        grdObat.Columns(4).HeaderText = "HARGA JUAL GROSIR"
                        grdObat.Columns(4).Width = 150
                        grdObat.Columns(4).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(4).FormatString = "{0:###,###.00}"
                    End If

                    'Dim rDr As SqlDataReader = .ExecuteReader

                    'grdObat.Rows.Clear()

                    'While rDr.Read
                    '    grdObat.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                    '                                   rDr.Item(1).ToString.Trim,
                    '                                   FormatNumber(rDr.Item(2).ToString.Trim, 2),
                    '                                   FormatNumber(rDr.Item(3).ToString.Trim, 2),
                    '                                   FormatNumber(rDr.Item(4).ToString.Trim, 2),
                    '                                   FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 2)})
                    'End While
                    'rDr.Close()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        ds.Dispose()
        grdObat.Focus()

        xForm.Dispose()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If clsPublic.CekAkses("item_add", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        frmEntryPersediaan.Dispose()
        frmEntryPersediaan.cUserId = cUserId.Trim
        frmEntryPersediaan.lNew = True
        frmEntryPersediaan.Show()
        frmEntryPersediaan.BringToFront()
    End Sub

    Private Sub btnCari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnCari.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If clsPublic.CekAkses("item_view", frmLogin.txtUserId.Text.Trim) = False Then
                MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
                Exit Sub
            End If

            CariData()
        End If
    End Sub

    Private Sub CariData()
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        Dim ds As New DataSet
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama barang],[kode barang]," &
                                   "[harga pokok pembelian],[harga jual toko]," &
                                   "[harga jual grosir] from barang " &
                                   "where [kode barang] = '" & btnCari.Text.Trim & "'  order by [nama barang] asc"

                    Dim da As New SqlDataAdapter(Cmd)
                    da.Fill(ds, "barang")
                    If ds.Tables("barang").Rows.Count > 0 Then
                        grdObat.DataSource = ds.Tables("barang")
                        grdObat.Columns(0).HeaderText = "NAMA BARANG"
                        grdObat.Columns(0).Width = 400
                        grdObat.Columns(1).HeaderText = "KODE BARANG"
                        grdObat.Columns(1).Width = 100
                        grdObat.Columns(2).HeaderText = "HARGA BELI"
                        grdObat.Columns(2).Width = 150
                        grdObat.Columns(2).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(2).FormatString = "{0:###,###.00}"
                        grdObat.Columns(3).HeaderText = "HARGA JUAL TOKO"
                        grdObat.Columns(3).Width = 150
                        grdObat.Columns(3).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(3).FormatString = "{0:###,###.00}"
                        grdObat.Columns(4).HeaderText = "HARGA JUAL GROSIR"
                        grdObat.Columns(4).Width = 150
                        grdObat.Columns(4).TextAlignment = ContentAlignment.MiddleRight
                        grdObat.Columns(4).FormatString = "{0:###,###.00}"
                    Else
                        .CommandText = "select [nama barang],[kode barang]," &
                                  "[harga pokok pembelian],[harga jual toko]," &
                                  "[harga jual grosir] from barang " &
                                  "where [nama barang] like '%" & btnCari.Text.Trim & "%'  order by [nama barang] asc"

                        da = New SqlDataAdapter(Cmd)
                        da.Fill(ds, "barang")
                        grdObat.DataSource = ds.Tables("barang")
                    End If

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        grdObat.Focus()
        ds.Dispose()

        'Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        'Conn.Open()

        'Using Cmd As New SqlCommand()
        '    Try
        '        With Cmd
        '            .Connection = Conn
        '            .CommandText = "select [nama barang],[kode barang]," &
        '                           "[harga pokok pembelian],[harga jual toko]," &
        '                           "[harga jual grosir] from barang " &
        '                           "where [nama barang] Like '" & btnCari.Text.Trim & "%' " &
        '                           "order by [nama barang] asc"
        '            Dim rDr As SqlDataReader = .ExecuteReader

        '            grdObat.Rows.Clear()

        '            While rDr.Read
        '                grdObat.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
        '                                               rDr.Item(1).ToString.Trim,
        '                                               FormatNumber(rDr.Item(2).ToString.Trim, 2),
        '                                               FormatNumber(rDr.Item(3).ToString.Trim, 2),
        '                                               FormatNumber(rDr.Item(4).ToString.Trim, 2),
        '                                               FormatNumber(clsPublic.CekStok(rDr.Item(1).ToString.Trim), 2)})
        '            End While
        '            rDr.Close()
        '        End With
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'End Using
        'Conn.Close()

        xForm.Dispose()
    End Sub

    Private Sub btnCari_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnCari.ButtonClick
        CariData()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If clsPublic.CekAkses("item_delete", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd

                        .Connection = Conn
                        .CommandText = "delete_barang"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_barang", SqlDbType.NChar, 25)).Value = grdObat.CurrentRow.Cells.Item(1).Value.ToString.Trim
                        .ExecuteNonQuery()

                        grdObat.Rows.RemoveAt(grdObat.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        'Using cfrmEntryPersediaan As New frmEntryPersediaan
        '    cfrmEntryPersediaan.lNew = False
        '    cfrmEntryPersediaan.cUserId = cUserId.Trim
        '    cfrmEntryPersediaan.cKodeObat = grdObat.CurrentRow.Cells.Item(1).Value.ToString.Trim
        '    cfrmEntryPersediaan.ShowDialog()
        '    'cfrmEntryPersediaan.BringToFront()

        '    grdObat.CurrentRow.Cells.Item(2).Value = FormatNumber(Decimal.Parse(cfrmEntryPersediaan.txtHargaPembelian.Text.Trim), 2)
        '    grdObat.CurrentRow.Cells.Item(3).Value = FormatNumber(Decimal.Parse(cfrmEntryPersediaan.txtHargaJualBebas.Text.Trim), 2)
        '    grdObat.CurrentRow.Cells.Item(4).Value = FormatNumber(Decimal.Parse(cfrmEntryPersediaan.txtHargaJualResep.Text.Trim), 2)
        '    grdObat.CurrentRow.Cells.Item(5).Value = FormatNumber(clsPublic.CekStok(grdObat.CurrentRow.Cells.Item(1).Value.ToString.Trim), 2)
        'End Using

        If clsPublic.CekAkses("item_update", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        frmEntryPersediaan.Dispose()
        frmEntryPersediaan.lNew = False
        frmEntryPersediaan.cUserId = cUserId.Trim
        frmEntryPersediaan.cKodeObat = grdObat.CurrentRow.Cells.Item(1).Value.ToString.Trim
        frmEntryPersediaan.Show()
        frmEntryPersediaan.BringToFront()

        'grdObat.CurrentRow.Cells.Item(2).Value = FormatNumber(Decimal.Parse(frmEntryPersediaan.txtHargaPembelian.Text.Trim), 2)
        'grdObat.CurrentRow.Cells.Item(3).Value = FormatNumber(Decimal.Parse(frmEntryPersediaan.txtHargaJualToko.Text.Trim), 2)
        'grdObat.CurrentRow.Cells.Item(4).Value = FormatNumber(Decimal.Parse(frmEntryPersediaan.txtHargaJualGrosir.Text.Trim), 2)
        'grdObat.CurrentRow.Cells.Item(5).Value = FormatNumber(clsPublic.CekStok(grdObat.CurrentRow.Cells.Item(1).Value.ToString.Trim), 2)

    End Sub

    Private Sub frmPersediaan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnHargaBertingkat_Click(sender As Object, e As EventArgs) Handles btnHargaBertingkat.Click
        Using cfrmHargaBertingkat As New frmHargaBertingkat
            cfrmHargaBertingkat.ShowDialog()
        End Using
    End Sub
End Class