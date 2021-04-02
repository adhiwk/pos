Imports System.Data.SqlClient
Public Class frmLogin
    Friend cNamaKasir As String = ""
    Friend cMonth As String = ""
    Friend cYear As String = ""
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetDate()
        Panel1.BackgroundImage = Image.FromFile(Application.StartupPath & "\background.jpg")
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
    End Sub


    Private Sub GetDate()
        Dim cTgl As Date = Now.Date
        Try

            Dim Conn As SqlConnection = clsPublic.KoneksiSQL
            Conn.Open()
            Using Cmd As New SqlCommand()
                With Cmd
                    .Connection = Conn
                    .CommandText = "select getdate()"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cTgl = Date.Parse(rDr.Item(0).ToString.Trim)

                        cMonth = cTgl.Month.ToString.Trim
                        cYear = cTgl.Year.ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "select [shift] from shift_kerja order by [shift] asc"
                    rDr = .ExecuteReader
                    cboShift.Properties.Items.Clear()
                    While rDr.Read
                        cboShift.Properties.Items.Add(rDr.Item(0).ToString)
                    End While
                    rDr.Close()
                End With
            End Using
            Conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If cboShift.Text.Trim = "" Then
            MsgBox("Pilih shift terlebih dahulu", MsgBoxStyle.Critical, "Error")
            cboShift.Focus()
            Exit Sub
        End If

        Dim strBarang As String =
            "select [user id],[password],[nama user] from users where " &
            "[user id] = '" & txtUserId.Text.Trim & "' and [password] = '" &
            Simple3Des.GetMd5Hash(txtPassword.Text.Trim) & "'"

        Dim conn As SqlConnection = clsPublic.KoneksiSQL
        conn.Open()

        Try
            Using cmd As New SqlCommand()
                With cmd
                    .Connection = conn
                    .CommandText = strBarang
                    Dim rDr As SqlDataReader = .ExecuteReader

                    While rDr.Read
                        If rDr.HasRows = True Then
                            If rDr.Item(0).ToString.Trim = txtUserId.Text.Trim Then
                                If rDr.Item(1).ToString.Trim = Simple3Des.GetMd5Hash(txtPassword.Text.Trim) Then
                                    frmMenuUtama.cUserId = txtUserId.Text.Trim
                                    frmMenuUtama.cNamaKasir = rDr.Item(2).ToString.Trim
                                    frmMenuUtama.cBulan = cMonth.Trim
                                    frmMenuUtama.cTahun = cYear.Trim
                                    frmMenuUtama.Show()
                                    Me.Hide()
                                Else
                                    MsgBox("User Id atau Password yang anda masukkan salah", MsgBoxStyle.Critical, "Error")
                                    Exit Sub
                                End If
                            Else
                                MsgBox("User Id atau Password yang anda masukkan salah", MsgBoxStyle.Critical, "Error")
                                Exit Sub
                            End If
                        End If
                    End While

                    If rDr.HasRows = False Then
                        MsgBox("User Id atau Password yang anda masukkan salah", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class