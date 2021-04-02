Public Class frmSplashScreen
    Sub New()
        InitializeComponent()
        Me.labelCopyright.Text = "Copyright © 1998-" & DateTime.Now.Year.ToString()
        peImage.Image = Image.FromFile(Application.StartupPath & "\background.jpg")
        peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum
End Class
