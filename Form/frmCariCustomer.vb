Imports System.Data.SqlClient
Public Class frmCariCustomer
    Private Sub frmCariPasien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCariCustomer.Focus()
    End Sub

    Private Sub txtCariPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariCustomer.KeyPress
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
                        .CommandText = "select [nama],[kode cust] " &
                                       "from customer where [nama] like '%" &
                                       txtCariCustomer.Text.Trim & "%' order by [nama] asc"
                        grdCustomer.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdCustomer.Rows.Add(New Object() {
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
            grdCustomer.Focus()
        End If
    End Sub

    Private Sub grdPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCustomer.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class