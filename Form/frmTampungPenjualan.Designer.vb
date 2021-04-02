<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTampungPenjualan
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdTampung = New Telerik.WinControls.UI.RadGridView()
        Me.grdTampungDetail = New Telerik.WinControls.UI.RadGridView()
        Me.btnLoadTampung = New DevExpress.XtraEditors.SimpleButton()
        Me.btnTampung = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtCustName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdTampung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTampung.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTampungDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTampungDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtCustName)
        Me.LayoutControl1.Controls.Add(Me.grdTampung)
        Me.LayoutControl1.Controls.Add(Me.grdTampungDetail)
        Me.LayoutControl1.Controls.Add(Me.btnLoadTampung)
        Me.LayoutControl1.Controls.Add(Me.btnTampung)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(669, 213, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(684, 457)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdTampung
        '
        Me.grdTampung.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.grdTampung.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdTampung.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdTampung.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.grdTampung.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdTampung.Location = New System.Drawing.Point(5, 91)
        '
        '
        '
        Me.grdTampung.MasterTemplate.AllowAddNewRow = False
        Me.grdTampung.MasterTemplate.AllowEditRow = False
        GridViewTextBoxColumn1.EnableExpressionEditor = False
        GridViewTextBoxColumn1.HeaderText = "KD TAMPUNG"
        GridViewTextBoxColumn1.Name = "column1"
        GridViewTextBoxColumn1.Width = 188
        GridViewTextBoxColumn2.EnableExpressionEditor = False
        GridViewTextBoxColumn2.HeaderText = "TANGGAL"
        GridViewTextBoxColumn2.Name = "column3"
        GridViewTextBoxColumn2.Width = 83
        GridViewTextBoxColumn3.EnableExpressionEditor = False
        GridViewTextBoxColumn3.HeaderText = "USER ID"
        GridViewTextBoxColumn3.Name = "column4"
        GridViewTextBoxColumn3.Width = 73
        GridViewTextBoxColumn4.EnableExpressionEditor = False
        GridViewTextBoxColumn4.HeaderText = "CUST. NAME"
        GridViewTextBoxColumn4.Name = "column2"
        GridViewTextBoxColumn4.Width = 281
        Me.grdTampung.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4})
        Me.grdTampung.MasterTemplate.EnableAlternatingRowColor = True
        Me.grdTampung.MasterTemplate.EnableGrouping = False
        Me.grdTampung.MasterTemplate.ShowRowHeaderColumn = False
        Me.grdTampung.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.grdTampung.Name = "grdTampung"
        Me.grdTampung.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdTampung.ShowGroupPanel = False
        Me.grdTampung.Size = New System.Drawing.Size(674, 176)
        Me.grdTampung.TabIndex = 6
        '
        'grdTampungDetail
        '
        Me.grdTampungDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.grdTampungDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdTampungDetail.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdTampungDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.grdTampungDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdTampungDetail.Location = New System.Drawing.Point(5, 277)
        '
        '
        '
        Me.grdTampungDetail.MasterTemplate.AllowAddNewRow = False
        Me.grdTampungDetail.MasterTemplate.AllowEditRow = False
        GridViewTextBoxColumn5.EnableExpressionEditor = False
        GridViewTextBoxColumn5.HeaderText = "KODE BARANG"
        GridViewTextBoxColumn5.Name = "column1"
        GridViewTextBoxColumn5.Width = 112
        GridViewTextBoxColumn6.EnableExpressionEditor = False
        GridViewTextBoxColumn6.HeaderText = "NAMA BARANG"
        GridViewTextBoxColumn6.Name = "column2"
        GridViewTextBoxColumn6.Width = 240
        GridViewTextBoxColumn7.EnableExpressionEditor = False
        GridViewTextBoxColumn7.HeaderText = "QTY"
        GridViewTextBoxColumn7.Name = "column3"
        GridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn7.Width = 49
        GridViewTextBoxColumn8.EnableExpressionEditor = False
        GridViewTextBoxColumn8.HeaderText = "HARGA"
        GridViewTextBoxColumn8.Name = "column4"
        GridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn8.Width = 124
        GridViewTextBoxColumn9.EnableExpressionEditor = False
        GridViewTextBoxColumn9.HeaderText = "SUB TOTAL"
        GridViewTextBoxColumn9.Name = "column5"
        GridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn9.Width = 127
        Me.grdTampungDetail.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewTextBoxColumn9})
        Me.grdTampungDetail.MasterTemplate.EnableAlternatingRowColor = True
        Me.grdTampungDetail.MasterTemplate.ShowRowHeaderColumn = False
        Me.grdTampungDetail.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.grdTampungDetail.Name = "grdTampungDetail"
        Me.grdTampungDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdTampungDetail.ShowGroupPanel = False
        Me.grdTampungDetail.Size = New System.Drawing.Size(674, 175)
        Me.grdTampungDetail.TabIndex = 5
        '
        'btnLoadTampung
        '
        Me.btnLoadTampung.ImageOptions.Image = Global.eRetail.My.Resources.Resources.load_tampung48
        Me.btnLoadTampung.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnLoadTampung.Location = New System.Drawing.Point(564, 5)
        Me.btnLoadTampung.Name = "btnLoadTampung"
        Me.btnLoadTampung.Size = New System.Drawing.Size(115, 76)
        Me.btnLoadTampung.StyleController = Me.LayoutControl1
        Me.btnLoadTampung.TabIndex = 1
        Me.btnLoadTampung.TabStop = False
        Me.btnLoadTampung.Text = "F3 - &Lanjut Transaksi"
        '
        'btnTampung
        '
        Me.btnTampung.ImageOptions.Image = Global.eRetail.My.Resources.Resources.add_tampung48
        Me.btnTampung.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnTampung.Location = New System.Drawing.Point(439, 5)
        Me.btnTampung.Name = "btnTampung"
        Me.btnTampung.Size = New System.Drawing.Size(115, 76)
        Me.btnTampung.StyleController = Me.LayoutControl1
        Me.btnTampung.TabIndex = 4
        Me.btnTampung.TabStop = False
        Me.btnTampung.Text = "F2 - &Tampung"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(684, 457)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.btnTampung
        Me.LayoutControlItem1.Location = New System.Drawing.Point(434, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.btnLoadTampung
        Me.LayoutControlItem2.Location = New System.Drawing.Point(559, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(125, 86)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.grdTampungDetail
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 272)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(684, 185)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 30)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(434, 56)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.grdTampung
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 86)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(684, 186)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(64, 5)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(365, 20)
        Me.txtCustName.StyleController = Me.LayoutControl1
        Me.txtCustName.TabIndex = 7
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtCustName
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(434, 30)
        Me.LayoutControlItem5.Text = "Cust. Name"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(56, 13)
        '
        'frmTampungPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 457)
        Me.Controls.Add(Me.LayoutControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTampungPenjualan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TAMPUNG PENJUALAN"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdTampung.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTampung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTampungDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTampungDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents btnLoadTampung As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnTampung As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdTampungDetail As Telerik.WinControls.UI.RadGridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents grdTampung As Telerik.WinControls.UI.RadGridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCustName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
End Class
