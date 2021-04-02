Imports System.Data.SqlClient
Public Class frmCariDokter
    Private Sub txtCariDokter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDokter.KeyPress
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
                        .CommandText = "select [nama dokter],[kode dokter] " &
                                       "from dokter where [nama dokter] like '%" &
                                       txtCariDokter.Text.Trim & "%' order by [nama dokter] asc"
                        grdDokter.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdDokter.Rows.Add(New Object() {
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
            grdDokter.Focus()
        End If
    End Sub

    Private Sub grdDokter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdDokter.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub frmCariDokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class