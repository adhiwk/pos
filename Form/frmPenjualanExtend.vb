Public Class frmPenjualanExtend
    Private Sub frmPenjualanExtend_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        peImage.Image = Image.FromFile(Application.StartupPath & "\handayani.jpg")
    End Sub
End Class