<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuUtama
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Transition1 As DevExpress.Utils.Animation.Transition = New DevExpress.Utils.Animation.Transition()
        Dim SlideFadeTransition1 As DevExpress.Utils.Animation.SlideFadeTransition = New DevExpress.Utils.Animation.SlideFadeTransition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenuUtama))
        Me.pnlApp = New System.Windows.Forms.Panel()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnPersediaan = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPenjualan = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPembelian = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLaporanPenjualan = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLaporanPembelian = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLaporanStok = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLaporanStokMinimum = New DevExpress.XtraBars.BarButtonItem()
        Me.txtUserLogin = New DevExpress.XtraBars.BarStaticItem()
        Me.btnEntryUsers = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPembayaranPiutang = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage3 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.tmMenu = New DevExpress.Utils.Animation.TransitionManager(Me.components)
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlApp
        '
        Me.pnlApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlApp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlApp.Location = New System.Drawing.Point(0, 157)
        Me.pnlApp.Name = "pnlApp"
        Me.pnlApp.Size = New System.Drawing.Size(771, 360)
        Me.pnlApp.TabIndex = 3
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.btnPersediaan, Me.btnPenjualan, Me.btnPembelian, Me.BarButtonItem1, Me.BarButtonItem2, Me.btnLaporanPenjualan, Me.btnLaporanPembelian, Me.btnLaporanStok, Me.btnLaporanStokMinimum, Me.txtUserLogin, Me.btnEntryUsers, Me.btnPembayaranPiutang})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 13
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2, Me.RibbonPage3})
        Me.RibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.RibbonControl.Size = New System.Drawing.Size(771, 157)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'btnPersediaan
        '
        Me.btnPersediaan.Caption = "PERSEDIAAN"
        Me.btnPersediaan.Id = 1
        Me.btnPersediaan.ImageOptions.Image = Global.eRetail.My.Resources.Resources.stock48
        Me.btnPersediaan.Name = "btnPersediaan"
        Me.btnPersediaan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnPenjualan
        '
        Me.btnPenjualan.Caption = "PENJUALAN"
        Me.btnPenjualan.Id = 2
        Me.btnPenjualan.ImageOptions.Image = Global.eRetail.My.Resources.Resources.product_colour48
        Me.btnPenjualan.Name = "btnPenjualan"
        Me.btnPenjualan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnPembelian
        '
        Me.btnPembelian.Caption = "PEMBELIAN"
        Me.btnPembelian.Id = 3
        Me.btnPembelian.ImageOptions.Image = Global.eRetail.My.Resources.Resources.stock_distribution48
        Me.btnPembelian.Name = "btnPembelian"
        Me.btnPembelian.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 5
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'btnLaporanPenjualan
        '
        Me.btnLaporanPenjualan.Caption = "Laporan Penjualan"
        Me.btnLaporanPenjualan.Id = 6
        Me.btnLaporanPenjualan.ImageOptions.LargeImage = Global.eRetail.My.Resources.Resources.rekap481
        Me.btnLaporanPenjualan.Name = "btnLaporanPenjualan"
        '
        'btnLaporanPembelian
        '
        Me.btnLaporanPembelian.Caption = "Laporan Pembelian"
        Me.btnLaporanPembelian.Id = 7
        Me.btnLaporanPembelian.ImageOptions.LargeImage = Global.eRetail.My.Resources.Resources.laporanpembelian48
        Me.btnLaporanPembelian.Name = "btnLaporanPembelian"
        '
        'btnLaporanStok
        '
        Me.btnLaporanStok.Caption = "Laporan Keadaan Stok"
        Me.btnLaporanStok.Id = 8
        Me.btnLaporanStok.ImageOptions.LargeImage = Global.eRetail.My.Resources.Resources.reportstok48
        Me.btnLaporanStok.Name = "btnLaporanStok"
        '
        'btnLaporanStokMinimum
        '
        Me.btnLaporanStokMinimum.Caption = "Laporan Stok Minimum"
        Me.btnLaporanStokMinimum.Id = 9
        Me.btnLaporanStokMinimum.ImageOptions.LargeImage = Global.eRetail.My.Resources.Resources.penerbitan_khs
        Me.btnLaporanStokMinimum.Name = "btnLaporanStokMinimum"
        '
        'txtUserLogin
        '
        Me.txtUserLogin.Caption = "User Login : "
        Me.txtUserLogin.Id = 10
        Me.txtUserLogin.Name = "txtUserLogin"
        '
        'btnEntryUsers
        '
        Me.btnEntryUsers.Caption = "ENTRY USERS"
        Me.btnEntryUsers.Id = 11
        Me.btnEntryUsers.ImageOptions.Image = Global.eRetail.My.Resources.Resources.users48
        Me.btnEntryUsers.Name = "btnEntryUsers"
        Me.btnEntryUsers.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnPembayaranPiutang
        '
        Me.btnPembayaranPiutang.Caption = "BAYAR PIUTANG"
        Me.btnPembayaranPiutang.Id = 12
        Me.btnPembayaranPiutang.ImageOptions.Image = Global.eRetail.My.Resources.Resources.payment48
        Me.btnPembayaranPiutang.Name = "btnPembayaranPiutang"
        Me.btnPembayaranPiutang.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "ENTRY DATA"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnPersediaan)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnEntryUsers)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "TRANSAKSI"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnPenjualan)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnPembelian)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnPembayaranPiutang)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPage3
        '
        Me.RibbonPage3.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup3})
        Me.RibbonPage3.Name = "RibbonPage3"
        Me.RibbonPage3.Text = "LAPORAN"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnLaporanPenjualan)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnLaporanPembelian)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnLaporanStok)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnLaporanStokMinimum)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.txtUserLogin)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 517)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(771, 22)
        '
        'DefaultLookAndFeel1
        '
        Me.DefaultLookAndFeel1.LookAndFeel.SkinName = "Office 2019 Colorful"
        '
        'tmMenu
        '
        Me.tmMenu.FrameCount = 500
        Transition1.BarWaitingIndicatorProperties.Caption = ""
        Transition1.BarWaitingIndicatorProperties.Description = ""
        Transition1.Control = Me.pnlApp
        Transition1.LineWaitingIndicatorProperties.AnimationElementCount = 5
        Transition1.LineWaitingIndicatorProperties.Caption = ""
        Transition1.LineWaitingIndicatorProperties.Description = ""
        Transition1.RingWaitingIndicatorProperties.AnimationElementCount = 5
        Transition1.RingWaitingIndicatorProperties.Caption = ""
        Transition1.RingWaitingIndicatorProperties.Description = ""
        Transition1.TransitionType = SlideFadeTransition1
        Transition1.WaitingIndicatorProperties.Caption = "Mohon tunggu..."
        Transition1.WaitingIndicatorProperties.Description = ""
        Me.tmMenu.Transitions.Add(Transition1)
        '
        'frmMenuUtama
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 539)
        Me.Controls.Add(Me.pnlApp)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMenuUtama"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "ePoint Of Sale Ver 5.1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents btnPersediaan As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPenjualan As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnPembelian As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage3 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLaporanPenjualan As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLaporanPembelian As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLaporanStok As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLaporanStokMinimum As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents pnlApp As Panel
    Friend WithEvents tmMenu As DevExpress.Utils.Animation.TransitionManager
    Friend WithEvents txtUserLogin As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents btnEntryUsers As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPembayaranPiutang As DevExpress.XtraBars.BarButtonItem
End Class
