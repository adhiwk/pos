Imports System.Data.SqlClient
Public Class frmCariObat
    Private Sub frmCariObat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCariBarang.Focus()
    End Sub

    Private Sub txtCariBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariBarang.KeyPress
        If Asc(e.KeyChar) = 13 Then

            If txtCariBarang.Text.Trim = "" Then
                Exit Sub
            End If

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            Try

                Dim Conn As SqlConnection = clsPublic.KoneksiSQL
                Conn.Open()
                Using Cmd As New SqlCommand()
                    With Cmd
                        .Connection = Conn
                        .CommandText = "select [nama barang],[kode barang],[plu] " &
                                       "from barang where [nama barang] like '%" &
                                       txtCariBarang.Text.Trim & "%' order by [nama barang] asc"
                        grdCariBarang.Rows.Clear()
                        Dim rDr As SqlDataReader = .ExecuteReader
                        While rDr.Read
                            grdCariBarang.Rows.Add(New Object() {rDr.Item(0).ToString.Trim,
                                                                 rDr.Item(1).ToString.Trim,
                                                                 rDr.Item(2).ToString.Trim,
                                                                 clsPublic.CekStok(rDr.Item(2).ToString.Trim)})
                        End While
                        rDr.Close()
                    End With
                End Using
                Conn.Close()
            Catch ex As Exception

            End Try
            xForm.Dispose()
            grdCariBarang.Focus()
        End If
    End Sub

    Private Sub grdCariObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCariBarang.KeyPress
        If Asc(e.KeyChar.ToString) = 13 Then
            Try
                'Pilih = True
                Me.Hide()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class