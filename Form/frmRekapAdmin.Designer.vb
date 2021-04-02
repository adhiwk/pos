<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRekapAdmin
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
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.grdRekapAdmin = New Telerik.WinControls.UI.RadGridView()
        Me.btnCetak = New DevExpress.XtraEditors.SimpleButton()
        Me.btnFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNamaKasir = New DevExpress.XtraEditors.TextEdit()
        Me.cboUserId = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.dtTransaksi = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.grdRekapAdmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRekapAdmin.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNamaKasir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUserId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransaksi.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTransaksi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.grdRekapAdmin)
        Me.LayoutControl1.Controls.Add(Me.btnCetak)
        Me.LayoutControl1.Controls.Add(Me.btnFilter)
        Me.LayoutControl1.Controls.Add(Me.txtNamaKasir)
        Me.LayoutControl1.Controls.Add(Me.cboUserId)
        Me.LayoutControl1.Controls.Add(Me.dtTransaksi)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(720, 203, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(518, 402)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'grdRekapAdmin
        '
        Me.grdRekapAdmin.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.grdRekapAdmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdRekapAdmin.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdRekapAdmin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.grdRekapAdmin.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdRekapAdmin.Location = New System.Drawing.Point(5, 65)
        '
        '
        '
        Me.grdRekapAdmin.MasterTemplate.AllowAddNewRow = False
        Me.grdRekapAdmin.MasterTemplate.AllowEditRow = False
        GridViewTextBoxColumn6.EnableExpressionEditor = False
        GridViewTextBoxColumn6.HeaderText = "KASSA"
        GridViewTextBoxColumn6.Name = "column1"
        GridViewTextBoxColumn6.Width = 70
        GridViewTextBoxColumn7.EnableExpressionEditor = False
        GridViewTextBoxColumn7.HeaderText = "SHIFT"
        GridViewTextBoxColumn7.Name = "column2"
        GridViewTextBoxColumn7.Width = 83
        GridViewTextBoxColumn8.EnableExpressionEditor = False
        GridViewTextBoxColumn8.HeaderText = "NAMA OPERATOR"
        GridViewTextBoxColumn8.Name = "column3"
        GridViewTextBoxColumn8.Width = 148
        GridViewTextBoxColumn9.EnableExpressionEditor = False
        GridViewTextBoxColumn9.HeaderText = "MODAL AWAL"
        GridViewTextBoxColumn9.Name = "column4"
        GridViewTextBoxColumn9.Width = 94
        GridViewTextBoxColumn10.EnableExpressionEditor = False
        GridViewTextBoxColumn10.HeaderText = "KAS AKTUAL"
        GridViewTextBoxColumn10.Name = "column5"
        GridViewTextBoxColumn10.Width = 98
        Me.grdRekapAdmin.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewTextBoxColumn9, GridViewTextBoxColumn10})
        Me.grdRekapAdmin.MasterTemplate.EnableAlternatingRowColor = True
        Me.grdRekapAdmin.MasterTemplate.ShowRowHeaderColumn = False
        Me.grdRekapAdmin.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.grdRekapAdmin.Name = "grdRekapAdmin"
        Me.grdRekapAdmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdRekapAdmin.ShowGroupPanel = False
        Me.grdRekapAdmin.Size = New System.Drawing.Size(508, 251)
        Me.grdRekapAdmin.TabIndex = 9
        Me.grdRekapAdmin.Text = "RadGridView1"
        '
        'btnCetak
        '
        'Me.btnCetak.Image = Global.SmartRetail_Kasir.My.Resources.Resources.printer48
        Me.btnCetak.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnCetak.Location = New System.Drawing.Point(423, 326)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(90, 71)
        Me.btnCetak.StyleController = Me.LayoutControl1
        Me.btnCetak.TabIndex = 8
        Me.btnCetak.Text = "F3 - &Cetak"
        '
        'btnFilter
        '
        'Me.btnFilter.Image = Global.SmartRetail_Kasir.My.Resources.Resources.cari48
        Me.btnFilter.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnFilter.Location = New System.Drawing.Point(323, 326)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(90, 71)
        Me.btnFilter.StyleController = Me.LayoutControl1
        Me.btnFilter.TabIndex = 7
        Me.btnFilter.Text = "F2 - &Filter"
        '
        'txtNamaKasir
        '
        Me.txtNamaKasir.EnterMoveNextControl = True
        Me.txtNamaKasir.Location = New System.Drawing.Point(279, 35)
        Me.txtNamaKasir.Name = "txtNamaKasir"
        Me.txtNamaKasir.Size = New System.Drawing.Size(222, 20)
        Me.txtNamaKasir.StyleController = Me.LayoutControl1
        Me.txtNamaKasir.TabIndex = 6
        '
        'cboUserId
        '
        Me.cboUserId.EnterMoveNextControl = True
        Me.cboUserId.Location = New System.Drawing.Point(96, 35)
        Me.cboUserId.Name = "cboUserId"
        Me.cboUserId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboUserId.Size = New System.Drawing.Size(82, 20)
        Me.cboUserId.StyleController = Me.LayoutControl1
        Me.cboUserId.TabIndex = 5
        '
        'dtTransaksi
        '
        Me.dtTransaksi.EditValue = New Date(2016, 7, 30, 0, 0, 0, 0)
        Me.dtTransaksi.EnterMoveNextControl = True
        Me.dtTransaksi.Location = New System.Drawing.Point(96, 5)
        Me.dtTransaksi.Name = "dtTransaksi"
        Me.dtTransaksi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtTransaksi.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtTransaksi.Size = New System.Drawing.Size(82, 20)
        Me.dtTransaksi.StyleController = Me.LayoutControl1
        Me.dtTransaksi.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.EmptySpaceItem2, Me.LayoutControlItem2, Me.EmptySpaceItem3, Me.LayoutControlItem3, Me.LayoutControlItem6, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(518, 402)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.dtTransaksi
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Tanggal Transaksi"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(86, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(183, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(335, 30)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cboUserId
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(183, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "User Id"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(86, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(506, 30)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(12, 30)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtNamaKasir
        Me.LayoutControlItem3.Location = New System.Drawing.Point(183, 30)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Nama Kasir"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(86, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.btnFilter
        Me.LayoutControlItem4.Location = New System.Drawing.Point(318, 321)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btnCetak
        Me.LayoutControlItem5.Location = New System.Drawing.Point(418, 321)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(100, 81)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.grdRekapAdmin
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(518, 261)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 321)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(318, 81)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'frmRekapAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 402)
        Me.Controls.Add(Me.LayoutControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRekapAdmin"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rekap Penjualan Admin"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.grdRekapAdmin.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRekapAdmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNamaKasir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUserId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransaksi.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTransaksi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents dtTransaksi As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNamaKasir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboUserId As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdRekapAdmin As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnCetak As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
