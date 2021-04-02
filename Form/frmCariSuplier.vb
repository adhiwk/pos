Imports System.Data.SqlClient
Public Class frmCariSuplier
    Private Sub txtCariSuplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariSuplier.KeyPress
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
                        .CommandText = "select [nama suplier],[kode suplier] " &
                                       "from suplier where [nama suplier] like '%" &
                                       txtCariSuplier.Text.Trim & "%' order by [nama suplier] asc"
                        grdCariSuplier.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdCariSuplier.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                               rDr.Item(1).ToString.Trim})
                        End While
                        rDr.Close()
                    End With
                End Using
                Conn.Close()
            Catch ex As Exception

            End Try
            xForm.Dispose()
            grdCariSuplier.Focus()
        End If
    End Sub

    Private Sub grdCariSuplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCariSuplier.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class