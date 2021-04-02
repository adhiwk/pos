Imports System.Data.SqlClient
Public Class frmCariProdusen
    Private Sub txtCariProdusen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariProdusen.KeyPress
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
                        .CommandText = "select [nama produsen],[kode produsen] " &
                                       "from produsen where [nama produsen] like '%" &
                                       txtCariProdusen.Text.Trim & "%' order by [nama produsen] asc"
                        grdCariProdusen.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdCariProdusen.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                               rDr.Item(1).ToString.Trim})
                        End While
                        rDr.Close()
                    End With
                End Using
                Conn.Close()
            Catch ex As Exception

            End Try
            xForm.Dispose()
            grdCariProdusen.Focus()
        End If
    End Sub

    Private Sub grdCariProdusen_Click(sender As Object, e As EventArgs) Handles grdCariProdusen.Click

    End Sub

    Private Sub grdCariProdusen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCariProdusen.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub frmCariProdusen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class