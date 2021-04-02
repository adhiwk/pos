Imports System.Data.SqlClient
Public Class ctrlPembayaranPiutang
    Dim lnew As Boolean
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        lnew = True
        btnTambah.Enabled = False
        btnBatal.Enabled = True
        btnSimpan.Enabled = True
        grdBayarPiutang.Rows.Clear()
        ClearScreen()
        EnableText()
        dtBayar.DateTime = Now.Date
        txtKodeTransaksi.Text = Format(Date.Parse(Now), "yyMMdd.HHmmss")
        txtKodeCust.Focus()
    End Sub

    Private Sub ctrlPembayaranPiutang_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        DefaultSetting()
    End Sub

    Private Sub ClearScreen()
        txtKodeTransaksi.Text = ""
        txtKodeCust.Text = ""
        txtNamaCust.Text = ""
        dtBayar.DateTime = Now.Date
        txtNoFaktur.Text = ""
        dtTglFaktur.DateTime = Now.Date
        txtBesarPiutang.Text = 0
        txtDibayar.Text = 0
        txtSisa.Text = 0
    End Sub

    Private Sub DisableText()
        txtKodeTransaksi.Properties.ReadOnly = True
        txtKodeCust.Properties.ReadOnly = True
        txtNamaCust.Properties.ReadOnly = True
        dtBayar.Properties.ReadOnly = True
        tmJam.Properties.ReadOnly = True
        txtNoFaktur.Properties.ReadOnly = True
        dtTglFaktur.Properties.ReadOnly = True
        txtBesarPiutang.Properties.ReadOnly = True
        txtDibayar.Properties.ReadOnly = True
        txtSisa.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        'txtKodeTransaksi.Properties.ReadOnly = False
        txtKodeCust.Properties.ReadOnly = False
        dtBayar.Properties.ReadOnly = False
        tmJam.Properties.ReadOnly = False
        txtNoFaktur.Properties.ReadOnly = False
        dtTglFaktur.Properties.ReadOnly = False
        'txtBesarPiutang.Properties.ReadOnly = False
        txtDibayar.Properties.ReadOnly = False
        'txtSisa.Properties.ReadOnly = False
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnTambah.Enabled = True
        btnUbah.Enabled = False
        btnHapus.Enabled = False
        btnBatal.Enabled = False
        btnSimpan.Enabled = False

        grdBayarPiutang.Rows.Clear()
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        DefaultSetting()
    End Sub

    Private Sub txtKodeCust_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKodeCust.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama] " &
                                   "from customer where [kode cust] = '" &
                                   txtKodeCust.Text.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtNamaCust.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
            dtBayar.Focus()
        End If
    End Sub

    Private Sub tmTick_Tick(sender As Object, e As EventArgs) Handles tmTick.Tick
        tmJam.EditValue = DateAndTime.TimeString
    End Sub

    Private Sub txtNoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoFaktur.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Using cfrmListPiutang As New frmListPiutang
                    cfrmListPiutang.cKodeCust = txtKodeCust.Text.Trim
                    cfrmListPiutang.cNamaCust = txtNamaCust.Text.Trim
                    cfrmListPiutang.ShowDialog()
                    txtNoFaktur.Text = cfrmListPiutang.grdPiutang.CurrentRow.Cells.Item(0).Value.ToString.Trim
                    dtTglFaktur.DateTime = Date.Parse(cfrmListPiutang.grdPiutang.CurrentRow.Cells.Item(1).Value.ToString.Trim)
                    txtBesarPiutang.Text = Decimal.Parse(cfrmListPiutang.grdPiutang.CurrentRow.Cells.Item(3).Value.ToString.Trim)
                    txtDibayar.Focus()
                End Using
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        frmMenuUtama.tmMenu.StartTransition(frmMenuUtama.pnlApp)
        Me.Hide()
        frmMenuUtama.tmMenu.EndTransition()
    End Sub

    Private Sub txtDibayar_EditValueChanged(sender As Object, e As EventArgs) Handles txtDibayar.EditValueChanged
        Try
            If Decimal.Parse(txtDibayar.Text.Trim) > Decimal.Parse(txtBesarPiutang.Text.Trim) Then
                MsgBox("besar pembayaran tidak boleh melebihi besar piutang", MsgBoxStyle.Exclamation, "error")
                txtDibayar.Text = 0
                txtSisa.Text = 0
                txtDibayar.Focus()
                Exit Sub
            Else
                txtSisa.Text = Decimal.Parse(txtBesarPiutang.Text.Trim) - Decimal.Parse(txtDibayar.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtKodeCust.Text.Trim = "" Then
                MsgBox("kode customer tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
                txtKodeCust.Focus()
                Exit Sub
            End If

            If txtNoFaktur.Text.Trim = "" Then
                MsgBox("anda belum memasukkan nomor faktur penjualan", MsgBoxStyle.Exclamation, "error")
                txtNoFaktur.Focus()
                Exit Sub
            End If

            If txtDibayar.Text.Trim = 0 Then
                MsgBox("nominal pembayaran tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
                txtDibayar.Focus()
                Exit Sub
            End If

            grdBayarPiutang.Rows.Add(New Object() {txtNoFaktur.Text.Trim,
                                     Format(Date.Parse(dtBayar.DateTime), "yyyy-MM-dd") & " " & tmJam.Text.Trim,
                                     txtBesarPiutang.Text.Trim,
                                     txtDibayar.Text.Trim,
                                     txtSisa.Text.Trim})

            txtNoFaktur.Text = ""
            dtTglFaktur.DateTime = Now.Date
            txtBesarPiutang.Text = 0
            txtDibayar.Text = 0
            txtSisa.Text = 0
            txtNoFaktur.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            grdBayarPiutang.Rows.RemoveAt(grdBayarPiutang.CurrentCell.RowIndex)
            txtNoFaktur.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lnew Then
            SaveData
        Else
            UpdateData
        End If
        DefaultSetting()
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        lnew = False
        btnUbah.Enabled = False
        btnTambah.Enabled = False
        btnHapus.Enabled = False
        btnBatal.Enabled = True
        btnSimpan.Enabled = True
        EnableText()
        txtKodeCust.Focus()
    End Sub

#Region "Entry Data"
    Private Sub SaveData()
        If txtKodeTransaksi.Text.Trim = "" Then
            MsgBox("nomor transaksi tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
            txtKodeTransaksi.Text = Format(Date.Parse(Now), "yyMMdd.HHmmss")
            Exit Sub
        End If

        If txtKodeCust.Text.Trim = "" Then
            MsgBox("kode customer tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
            txtKodeCust.Focus()
            Exit Sub
        End If

        If clsPublic.VerifyData(txtKodeTransaksi.Text.Trim, "select [kode bayar] from pembayaran_piutang " &
                               "where [kode bayar] = '" & txtKodeTransaksi.Text.Trim & "'") Then
            MsgBox("nomor transaksi sudah tersimpan dalam database", MsgBoxStyle.Critical, "error")
            txtKodeTransaksi.Text = Format(Date.Parse(Now), "yyMMdd.HHmmss")
            Exit Sub
        End If

        If grdBayarPiutang.Rows.Count <= 0 Then
            MsgBox("data pembayaran piutang masih kosong, data penjualan gagal disimpan", MsgBoxStyle.Critical, "error")
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "add_pembayaran_piutang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "ADD"
                    Dim dtBayarPiutang As New DataTable()
                    dtBayarPiutang.Columns.Add("kode bayar", GetType([String])).MaxLength = 15
                    dtBayarPiutang.Columns.Add("kode cust", GetType([String])).MaxLength = 5
                    dtBayarPiutang.Columns.Add("nomor nota", GetType([String])).MaxLength = 15
                    dtBayarPiutang.Columns.Add("tgl", GetType([String])).MaxLength = 20
                    dtBayarPiutang.Columns.Add("bayar", GetType(Decimal))

                    For Each DataRow In grdBayarPiutang.Rows
                        'masukkan baris dari grid kedalam
                        'tabel detail pembayaran
                        dtBayarPiutang.Rows.Add(New Object() {
                        txtKodeTransaksi.Text.Trim,
                        txtKodeCust.Text.Trim,
                        DataRow.Cells.Item(0).Value.ToString.Trim,
                        DataRow.Cells.Item(1).Value.ToString.Trim,
                        Decimal.Parse(DataRow.Cells.Item(3).Value.ToString.Trim)})
                    Next
                    .Parameters.Add("@pembayaran_piutang_TVP", SqlDbType.Structured).Value = dtBayarPiutang
                    .ExecuteNonQuery()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("data berhasil disimpan", MsgBoxStyle.Information, "informasi")
    End Sub
    Private Sub UpdateData()
        If txtKodeTransaksi.Text.Trim = "" Then
            MsgBox("nomor transaksi tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
            txtKodeTransaksi.Text = Format(Date.Parse(Now), "yyMMdd.HHmmss")
            Exit Sub
        End If

        If txtKodeCust.Text.Trim = "" Then
            MsgBox("kode customer tidak boleh kosong", MsgBoxStyle.Exclamation, "error")
            txtKodeCust.Focus()
            Exit Sub
        End If

        If grdBayarPiutang.Rows.Count <= 0 Then
            MsgBox("data pembayaran piutang masih kosong, data penjualan gagal disimpan", MsgBoxStyle.Critical, "error")
            Exit Sub
        End If

        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "update_pembayaran_piutang"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = SqlDbType.NChar
                    .Parameters(0).Direction = ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_bayar", SqlDbType.NChar, 15)).Value = txtKodeTransaksi.Text.Trim
                    Dim dtBayarPiutang As New DataTable()
                    dtBayarPiutang.Columns.Add("kode bayar", GetType([String])).MaxLength = 15
                    dtBayarPiutang.Columns.Add("kode cust", GetType([String])).MaxLength = 5
                    dtBayarPiutang.Columns.Add("nomor nota", GetType([String])).MaxLength = 15
                    dtBayarPiutang.Columns.Add("tgl", GetType([String])).MaxLength = 20
                    dtBayarPiutang.Columns.Add("bayar", GetType(Decimal))

                    For Each DataRow In grdBayarPiutang.Rows
                        'masukkan baris dari grid kedalam
                        'tabel detail pembayaran
                        dtBayarPiutang.Rows.Add(New Object() {
                        txtKodeTransaksi.Text.Trim,
                        txtKodeCust.Text.Trim,
                        DataRow.Cells.Item(0).Value.ToString.Trim,
                        DataRow.Cells.Item(1).Value.ToString.Trim,
                        Decimal.Parse(DataRow.Cells.Item(3).Value.ToString.Trim)})
                    Next
                    .Parameters.Add("@pembayaran_piutang_TVP", SqlDbType.Structured).Value = dtBayarPiutang
                    .ExecuteNonQuery()
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        MsgBox("data berhasil diupdate", MsgBoxStyle.Information, "informasi")
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cmsg As String = MsgBox("apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "konfirmasi")
        If cmsg = vbOK Then
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "delete_pembayaran_piutang"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_bayar", SqlDbType.NChar, 15)).Value = txtKodeTransaksi.Text.Trim
                        .ExecuteNonQuery()
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        grdTransaksi.Rows.RemoveAt(grdTransaksi.CurrentCell.RowIndex)
        DefaultSetting()
    End Sub
#End Region
#Region "Filter Data"
    Private Sub btnSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnSearch.KeyPress
        Dim cQuery As String = ""
        If cboCariPembayaran.Text.Trim = "NAMA CUSTOMER" Then
            cQuery = "SELECT pembayaran_piutang.[kode bayar]," &
                      "pembayaran_piutang.[tgl]," &
                      "customer.[nama]," &
                      "pembayaran_piutang.[nomor nota]," &
                      "piutang.[piutang]," &
                      "pembayaran_piutang.[bayar] " &
                      "FROM (pembayaran_piutang pembayaran_piutang " &
                      "INNER JOIN customer customer ON " &
                      "pembayaran_piutang.[kode cust] = customer.[kode cust]) " &
                      "INNER JOIN piutang piutang On pembayaran_piutang.[nomor nota] = piutang.[nomor nota] " &
                      "WHERE customer.[nama] LIKE '%" & btnSearch.Text.Trim & "%' " &
                      "ORDER BY pembayaran_piutang.[tgl] DESC"
        ElseIf cboCariPembayaran.Text.Trim = "TANGGAL BAYAR (yyyy-mm-dd)" Then
            cQuery = "SELECT pembayaran_piutang.[kode bayar]," &
                     "pembayaran_piutang.[tgl]," &
                     "customer.[nama]," &
                     "pembayaran_piutang.[nomor nota]," &
                     "piutang.[piutang]," &
                     "pembayaran_piutang.[bayar] " &
                     "FROM (pembayaran_piutang pembayaran_piutang " &
                     "INNER JOIN customer customer ON " &
                     "pembayaran_piutang.[kode cust] = customer.[kode cust]) " &
                     "INNER JOIN piutang piutang On pembayaran_piutang.[nomor nota] = piutang.[nomor nota] " &
                     "WHERE pembayaran_piutang.[tgl] = '" & btnSearch.Text.Trim & "' " &
                     "ORDER BY pembayaran_piutang.[tgl] DESC"
        ElseIf cboCariPembayaran.Text.Trim = "NOMOR FAKTUR" Then
            cQuery = "SELECT pembayaran_piutang.[kode bayar]," &
                     "pembayaran_piutang.[tgl]," &
                     "customer.[nama]," &
                     "pembayaran_piutang.[nomor nota]," &
                     "piutang.[piutang]," &
                     "pembayaran_piutang.[bayar] " &
                     "FROM (pembayaran_piutang pembayaran_piutang " &
                     "INNER JOIN customer customer ON " &
                     "pembayaran_piutang.[kode cust] = customer.[kode cust]) " &
                     "INNER JOIN piutang piutang On pembayaran_piutang.[nomor nota] = piutang.[nomor nota] " &
                     "WHERE pembayaran_piutang.[nomor nota] = '" & btnSearch.Text.Trim & "' " &
                     "ORDER BY pembayaran_piutang.[tgl] DESC"
        Else
            cQuery = "SELECT top 10 pembayaran_piutang.[kode bayar]," &
                    "pembayaran_piutang.[tgl]," &
                    "customer.[nama]," &
                    "pembayaran_piutang.[nomor nota]," &
                    "piutang.[piutang]," &
                    "pembayaran_piutang.[bayar] " &
                    "FROM (pembayaran_piutang pembayaran_piutang " &
                    "INNER JOIN customer customer ON " &
                    "pembayaran_piutang.[kode cust] = customer.[kode cust]) " &
                    "INNER JOIN piutang piutang On pembayaran_piutang.[nomor nota] = piutang.[nomor nota] " &
                    "ORDER BY pembayaran_piutang.[tgl] DESC"
        End If

        grdTransaksi.Rows.Clear()
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = cQuery.Trim
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    grdTransaksi.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                          Format(Date.Parse(rDr.Item(1).ToString.Trim), "dd-MM-yyyy HH:mm:ss"),
                                          rDr.Item(2).ToString.Trim,
                                          rDr.Item(3).ToString.Trim,
                                          FormatNumber(Decimal.Parse(rDr.Item(4).ToString.Trim), 0),
                                          FormatNumber(Decimal.Parse(rDr.Item(5).ToString.Trim), 0),
                                          FormatNumber(Decimal.Parse(HitungSisa(rDr.Item(3).ToString.Trim,
                                                       Decimal.Parse(rDr.Item(4).ToString.Trim))), 0)
                                          })
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
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
                    nSisa = nPiutang - Decimal.Parse(rDr.Item(0).ToString.Trim)
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
        Return nSisa
    End Function

    Private Sub MasterTemplate_Click(sender As Object, e As EventArgs) Handles grdTransaksi.Click
        Try
            ShowRecord(grdTransaksi.CurrentRow.Cells.Item(0).Value.ToString.Trim)
            btnUbah.Enabled = True
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowRecord(ByVal cNomorBayar As String)
        Dim Conn As SqlConnection = clsPublic.KoneksiSQL
        Conn.Open()
        Using Cmd As New SqlCommand()
            With Cmd
                .Connection = Conn
                .CommandText = "SELECT pembayaran_piutang.[kode bayar]," &
                               "pembayaran_piutang.[tgl]," &
                               "customer.[nama]," &
                               "pembayaran_piutang.[kode cust] " &
                               "From pembayaran_piutang pembayaran_piutang " &
                               "INNER Join customer customer " &
                               "On pembayaran_piutang.[kode cust] = customer.[kode cust] " &
                               "WHERE pembayaran_piutang.[kode bayar] = '" & cNomorBayar.Trim & "'"
                Dim rDr As SqlDataReader = .ExecuteReader
                While rDr.Read
                    txtKodeTransaksi.Text = rDr.Item(0).ToString.Trim
                    dtBayar.DateTime = rDr.Item(1).ToString.Trim
                    tmJam.Time = rDr.Item(1).ToString.Trim
                    txtKodeCust.Text = rDr.Item(3).ToString.Trim
                    txtNamaCust.Text = rDr.Item(2).ToString.Trim
                End While
                rDr.Close()

                .CommandText = "SELECT pembayaran_piutang.[nomor nota]," &
                                "pembayaran_piutang.[tgl]," &
                                "piutang.[piutang]," &
                                "pembayaran_piutang.[bayar] " &
                                "FROM pembayaran_piutang " &
                                "pembayaran_piutang INNER JOIN " &
                                "piutang piutang ON " &
                                "pembayaran_piutang.[nomor nota] = piutang.[nomor nota] " &
                                "WHERE pembayaran_piutang.[kode bayar] = '" &
                                cNomorBayar.Trim & "'"

                grdBayarPiutang.Rows.Clear()
                rDr = .ExecuteReader
                While rDr.Read
                    grdBayarPiutang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                             Format(Date.Parse(rDr.Item(1).ToString.Trim), "yyyy-MM-dd HH:mm:ss"),
                                             FormatNumber(Decimal.Parse(rDr.Item(2).ToString.Trim), 0),
                                             FormatNumber(Decimal.Parse(rDr.Item(3).ToString.Trim), 0),
                                             FormatNumber(Decimal.Parse(HitungSisa(rDr.Item(0).ToString.Trim,
                                                       Decimal.Parse(rDr.Item(2).ToString.Trim))), 0)})
                End While
                rDr.Close()
            End With
        End Using
        Conn.Close()
    End Sub
#End Region
End Class
