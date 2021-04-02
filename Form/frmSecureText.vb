Public Class frmSecureText

    Private Sub frmSecureText_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnEncrypt_Click(sender As Object, e As EventArgs) Handles btnEncrypt.Click
        txtOutput.Text = Simple3Des.Encrypt(txtInput.Text.Trim)
    End Sub
End Class