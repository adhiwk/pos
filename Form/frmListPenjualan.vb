Imports System.Data.SqlClient
Public Class frmListPenjualan
    Friend nUbah As Boolean
    Friend cCrudAction As String = ""
    Friend cNomorNota = ""
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        Try
            Dim cQuery As String = ""
            If chkNoNota.Checked = True Then
                cQuery = "select [tgl nota],[nomor nota]," &
                         "[tunai],[paket],[grosir],[total],[kode cust] from penjualan where " &
                         "[nomor nota] = '" & txtCariNota.Text.Trim & "'"
            ElseIf chkTanggal.Checked = True Then
                cQuery = "select [tgl nota],[nomor nota]," &
                         "[tunai],[paket],[grosir],[total],[kode cust] from penjualan where " &
                         "year([tgl nota]) = '" & dtTanggal.DateTime.Year.ToString.Trim & "' and " &
                         "month([tgl nota]) = '" & dtTanggal.DateTime.Month.ToString.Trim & "' and " &
                         "day([tgl nota]) = '" & dtTanggal.DateTime.Day.ToString.Trim & "'"
            End If

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = cQuery.Trim

                    Dim rDr As SqlDataReader = .ExecuteReader
                    grdListPencarian.Rows.Clear()
                    While rDr.Read
                        grdListPencarian.Rows.Add(New Object() {FormatDateTime(rDr.Item(0).ToString.Trim, DateFormat.GeneralDate),
                                                             rDr.Item(1).ToString.Trim,
                                                             rDr.Item(2).ToString.Trim,
                                                             rDr.Item(3).ToString.Trim,
                                                             rDr.Item(4).ToString.Trim,
                                                             FormatNumber(Val(rDr.Item(5).ToString.Trim), 2),
                                                             LoadCustName(rDr.Item(6).ToString.Trim)
                                                             })
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Function LoadCustName(ByVal cCustCode As String) As String
        Dim cCustName As String = ""
        Try
            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()

            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select [nama] from customer where [kode cust] = '" & cCustCode.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cCustName = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
        Return cCustName.Trim
    End Function
    Private Sub chkNoResep_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoNota.CheckedChanged
        If chkNoNota.Checked = True Then
            chkTanggal.Checked = False
            txtCariNota.Enabled = True
            dtTanggal.Enabled = False
        End If
    End Sub

    Private Sub chkTanggal_CheckedChanged(sender As Object, e As EventArgs) Handles chkTanggal.CheckedChanged
        If chkTanggal.Checked = True Then
            chkNoNota.Checked = False
            txtCariNota.Enabled = False
            dtTanggal.Enabled = True
        End If
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "delete") Then
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
                        .CommandText = "delete_penjualan"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = SqlDbType.NChar
                        .Parameters(0).Direction = ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@nomor_nota", SqlDbType.NChar, 15)).Value = grdListPencarian.CurrentRow.Cells.Item(1).Value.ToString.Trim
                        .ExecuteNonQuery()

                        grdListPencarian.Rows.RemoveAt(grdListPencarian.CurrentCell.RowIndex)

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        If Not RolesBasedAccessControl(frmLogin.txtUserId.Text.Trim, "penjualan", "update") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "Akses ditolak")
            Exit Sub
        End If

        nUbah = True
        cCrudAction = "crud-edit"
        cNomorNota = grdListPencarian.CurrentRow.Cells.Item(1).Value.ToString.Trim
        Me.Dispose()
    End Sub

    Private Sub frmListPenjualan_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If nUbah = False Then
            cCrudAction = ""
        End If
    End Sub

    Private Sub frmListPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtTanggal.DateTime = Now.Date
    End Sub
End Class