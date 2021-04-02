Imports System.Data.SqlClient
Public Class frmListPembelian
    Friend nUbah As Boolean
    Friend cCrudAction As String
    Friend cNomorFaktur As String
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "pembelian", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        Try
            Dim cQuery As String = ""
            If chkNomorFaktur.Checked = True Then
                cQuery = "SELECT pembelian.[tgl faktur],pembelian.[nomor faktur]," &
                          "suplier.[nama suplier],pembelian.[total pajak],pembelian.[uang muka],pembelian.[saldo terhutang] " &
                          "FROM pembelian pembelian INNER JOIN suplier suplier " &
                          "ON pembelian.[kode suplier] = suplier.[kode suplier] " &
                          "WHERE pembelian.[nomor faktur] = '" & txtCariPembelian.Text.Trim & "'"
            ElseIf chkNamaSuplier.Checked = True Then
                cQuery = "SELECT pembelian.[tgl faktur],pembelian.[nomor faktur]," &
                         "suplier.[nama suplier],pembelian.[total pajak],pembelian.[uang muka],pembelian.[saldo terhutang] " &
                         "FROM pembelian pembelian INNER JOIN suplier suplier " &
                         "ON pembelian.[kode suplier] = suplier.[kode suplier] " &
                         "WHERE suplier.[nama suplier] like '%" & txtCariPembelian.Text.Trim & "%' " &
                         "ORDER BY pembelian.[tgl faktur] ASC"
            ElseIf chkTglFaktur.Checked = True Then
                cQuery = "SELECT pembelian.[tgl faktur],pembelian.[nomor faktur]," &
                         "suplier.[nama suplier],pembelian.[total pajak],pembelian.[uang muka],pembelian.[saldo terhutang] " &
                         "FROM pembelian pembelian INNER JOIN suplier suplier " &
                         "ON pembelian.[kode suplier] = suplier.[kode suplier] " &
                         "WHERE  year(pembelian.[tgl faktur]) = '" & dtTransaksi.DateTime.Year.ToString.Trim & "' AND " &
                         "month(pembelian.[tgl faktur]) = '" & dtTransaksi.DateTime.Month.ToString.Trim & "' AND " &
                         "day(pembelian.[tgl faktur]) = '" & dtTransaksi.DateTime.Day.ToString.Trim & "' " &
                         "ORDER BY pembelian.[tgl faktur] ASC"
            End If

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = cQuery.Trim

                    Dim rDr As SqlDataReader = .ExecuteReader
                    grdListPembelian.Rows.Clear()
                    While rDr.Read
                        'tgl faktur - nomor faktur - nama suplier - total - pajak terhutang - pembayaran - saldo terhutang
                        grdListPembelian.Rows.Add(New Object() {FormatDateTime(rDr.Item(0).ToString.Trim, DateFormat.ShortDate),
                                                             rDr.Item(1).ToString.Trim,
                                                             rDr.Item(2).ToString.Trim,
                                                             FormatNumber(HitungSubTotal(rDr.Item(1).ToString.Trim), 2),
                                                             FormatNumber(Val(Decimal.Parse(rDr.Item(3).ToString.Trim)), 2),
                                                             FormatNumber(Val(Decimal.Parse(rDr.Item(4).ToString.Trim)), 2),
                                                             FormatNumber(Val(Decimal.Parse(rDr.Item(5).ToString.Trim)), 2)
                                                             })
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Function HitungSubTotal(cNomorFaktur As String) As Decimal
        Dim nSubTotal As Decimal = 0
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select sum([qty] * [harga pokok pembelian]) as [total] " &
                                   "from pembelian_detail where [nomor faktur] = '" & cNomorFaktur.Trim & "'"

                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        nSubTotal = Decimal.Parse(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try

        Return nSubTotal
    End Function

    Private Sub chkNomorFaktur_CheckedChanged(sender As Object, e As EventArgs) Handles chkNomorFaktur.CheckedChanged
        If chkNomorFaktur.Checked = True Then
            chkNamaSuplier.Checked = False
            chkTglFaktur.Checked = False
            txtCariPembelian.Enabled = True
            dtTransaksi.Enabled = False
        End If
    End Sub

    Private Sub chkNamaSuplier_CheckedChanged(sender As Object, e As EventArgs) Handles chkNamaSuplier.CheckedChanged
        If chkNamaSuplier.Checked = True Then
            chkNomorFaktur.Checked = False
            chkTglFaktur.Checked = False
            txtCariPembelian.Enabled = True
            dtTransaksi.Enabled = False
        End If
    End Sub

    Private Sub chkTglFaktur_CheckedChanged(sender As Object, e As EventArgs) Handles chkTglFaktur.CheckedChanged
        If chkTglFaktur.Checked = True Then
            chkNamaSuplier.Checked = False
            chkNomorFaktur.Checked = False
            txtCariPembelian.Enabled = False
            dtTransaksi.Enabled = True
        End If
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "pembelian", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
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
                        .CommandText = "delete_pembelian"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@nomor_faktur", SqlDbType.NChar, 15)).Value = grdListPembelian.CurrentRow.Cells.Item(1).Value.ToString.Trim
                        .ExecuteNonQuery()

                        grdListPembelian.Rows.RemoveAt(grdListPembelian.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "pembelian", "update") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        If clsPublic.CekAkses("pembelian_update", frmLogin.txtUserId.Text.Trim) = False Then
            MsgBox("Anda tidak memiliki akses untuk melakukan proses ini", vbExclamation, "Akses ditolak")
            Exit Sub
        End If

        nUbah = True
        cCrudAction = "crud-edit"
        cNomorFaktur = grdListPembelian.CurrentRow.Cells.Item(1).Value.ToString.Trim
        Me.Dispose()
    End Sub

    Private Sub frmListPembelian_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If nUbah = False Then
            cCrudAction = ""
        End If
    End Sub

    Private Sub frmListPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cCrudAction = ""
        cNomorFaktur = ""
    End Sub
End Class