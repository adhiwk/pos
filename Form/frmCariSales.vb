Imports System.Data.SqlClient
Public Class frmCariSales
    Private Sub frmCariSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCariSales.Focus()
    End Sub

    Private Sub txtCariSales_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariSales.KeyPress
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
                        .CommandText = "select [nama],[kode sales] " &
                                       "from sales where [nama] like '%" &
                                       txtCariSales.Text.Trim & "%' order by [nama] asc"
                        grdSales.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdSales.Rows.Add(New Object() {
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
            grdSales.Focus()
        End If
    End Sub

    Private Sub grdPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSales.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class