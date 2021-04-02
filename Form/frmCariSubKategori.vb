Imports System.Data.SqlClient
Public Class frmCariSubKategori
    Friend cKodeKategori As String
    Private Sub txtCariSubKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariSubKategori.KeyPress
        If Asc(e.KeyChar) = 13 Then

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            Try

                Dim Conn As SqlConnection = clsPublic.KoneksiSQL
                Conn.Open()
                Using Cmd As New SqlCommand()
                    With Cmd

                        .Connection = Conn
                        .CommandText = "select [nama sub kategori],[kode sub kategori] " &
                                       "from sub_kategori where [nama sub kategori] like '%" &
                                       txtCariSubKategori.Text.Trim & "%' " &
                                       " and [kode kategori] = '" & cKodeKategori.Trim &
                                       "' order by [nama sub kategori] asc"

                        grdSubKategori.Rows.Clear()

                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read

                            grdSubKategori.Rows.Add(New Object() {
                                                               rDr.Item(0).ToString.Trim,
                                                               rDr.Item(1).ToString.Trim
                                                    })
                        End While

                        rDr.Close()
                    End With
                End Using
                Conn.Close()
            Catch ex As Exception

            End Try
            xForm.Dispose()
            grdSubKategori.Focus()
        End If
    End Sub

    Private Sub grdSubKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSubKategori.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub frmCariSubKategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class