<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLaporanPembelian
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtKodeProdusen = New DevExpress.XtraEditors.TextEdit()
        Me.cboProdusen = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtKodeObat = New DevExpress.XtraEditors.TextEdit()
        Me.txtNamaObat = New DevExpress.XtraEditors.TextEdit()
        Me.dtAkhir = New DevExpress.XtraEditors.DateEdit()
        Me.dtAwal = New DevExpress.XtraEditors.DateEdit()
        Me.txtKodeSuplier = New DevExpress.XtraEditors.TextEdit()
        Me.cboNamaSuplier = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboJenisLaporan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.btnTutup = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCetak = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtKodeProdusen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboProdusen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKodeObat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNamaObat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAkhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAwal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKodeSuplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNamaSuplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboJenisLaporan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtKodeProdusen)
        Me.LayoutControl1.Controls.Add(Me.cboProdusen)
        Me.LayoutControl1.Controls.Add(Me.txtKodeObat)
        Me.LayoutControl1.Controls.Add(Me.txtNamaObat)
        Me.LayoutControl1.Controls.Add(Me.btnTutup)
        Me.LayoutControl1.Controls.Add(Me.btnCetak)
        Me.LayoutControl1.Controls.Add(Me.dtAkhir)
        Me.LayoutControl1.Controls.Add(Me.dtAwal)
        Me.LayoutControl1.Controls.Add(Me.txtKodeSuplier)
        Me.LayoutControl1.Controls.Add(Me.cboNamaSuplier)
        Me.LayoutControl1.Controls.Add(Me.cboJenisLaporan)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(452, 141, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(397, 339)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtKodeProdusen
        '
        Me.txtKodeProdusen.Location = New System.Drawing.Point(316, 35)
        Me.txtKodeProdusen.Name = "txtKodeProdusen"
        Me.txtKodeProdusen.Size = New System.Drawing.Size(76, 20)
        Me.txtKodeProdusen.StyleController = Me.LayoutControl1
        Me.txtKodeProdusen.TabIndex = 14
        '
        'cboProdusen
        '
        Me.cboProdusen.Location = New System.Drawing.Point(85, 35)
        Me.cboProdusen.Name = "cboProdusen"
        Me.cboProdusen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboProdusen.Size = New System.Drawing.Size(221, 20)
        Me.cboProdusen.StyleController = Me.LayoutControl1
        Me.cboProdusen.TabIndex = 13
        '
        'txtKodeObat
        '
        Me.txtKodeObat.Location = New System.Drawing.Point(316, 95)
        Me.txtKodeObat.Name = "txtKodeObat"
        Me.txtKodeObat.Size = New System.Drawing.Size(76, 20)
        Me.txtKodeObat.StyleController = Me.LayoutControl1
        Me.txtKodeObat.TabIndex = 12
        '
        'txtNamaObat
        '
        Me.txtNamaObat.Location = New System.Drawing.Point(85, 95)
        Me.txtNamaObat.Name = "txtNamaObat"
        Me.txtNamaObat.Size = New System.Drawing.Size(221, 20)
        Me.txtNamaObat.StyleController = Me.LayoutControl1
        Me.txtNamaObat.TabIndex = 11
        '
        'dtAkhir
        '
        Me.dtAkhir.EditValue = New Date(2017, 7, 31, 0, 0, 0, 0)
        Me.dtAkhir.Location = New System.Drawing.Point(283, 125)
        Me.dtAkhir.Name = "dtAkhir"
        Me.dtAkhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAkhir.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAkhir.Size = New System.Drawing.Size(109, 20)
        Me.dtAkhir.StyleController = Me.LayoutControl1
        Me.dtAkhir.TabIndex = 8
        '
        'dtAwal
        '
        Me.dtAwal.EditValue = New Date(2017, 7, 31, 0, 0, 0, 0)
        Me.dtAwal.Location = New System.Drawing.Point(85, 125)
        Me.dtAwal.Name = "dtAwal"
        Me.dtAwal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAwal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAwal.Size = New System.Drawing.Size(108, 20)
        Me.dtAwal.StyleController = Me.LayoutControl1
        Me.dtAwal.TabIndex = 7
        '
        'txtKodeSuplier
        '
        Me.txtKodeSuplier.Location = New System.Drawing.Point(316, 65)
        Me.txtKodeSuplier.Name = "txtKodeSuplier"
        Me.txtKodeSuplier.Size = New System.Drawing.Size(76, 20)
        Me.txtKodeSuplier.StyleController = Me.LayoutControl1
        Me.txtKodeSuplier.TabIndex = 6
        '
        'cboNamaSuplier
        '
        Me.cboNamaSuplier.Location = New System.Drawing.Point(85, 65)
        Me.cboNamaSuplier.Name = "cboNamaSuplier"
        Me.cboNamaSuplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNamaSuplier.Size = New System.Drawing.Size(221, 20)
        Me.cboNamaSuplier.StyleController = Me.LayoutControl1
        Me.cboNamaSuplier.TabIndex = 5
        '
        'cboJenisLaporan
        '
        Me.cboJenisLaporan.Location = New System.Drawing.Point(85, 5)
        Me.cboJenisLaporan.Name = "cboJenisLaporan"
        Me.cboJenisLaporan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJenisLaporan.Size = New System.Drawing.Size(307, 20)
        Me.cboJenisLaporan.StyleController = Me.LayoutControl1
        Me.cboJenisLaporan.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem10, Me.LayoutControlItem11})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(397, 339)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cboJenisLaporan
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(397, 30)
        Me.LayoutControlItem1.Text = "Jenis Laporan"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cboNamaSuplier
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Nama Suplier"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtKodeSuplier
        Me.LayoutControlItem3.Location = New System.Drawing.Point(311, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(86, 30)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 190)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(397, 149)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.dtAwal
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(198, 30)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(198, 30)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(198, 30)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Dari Tanggal"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.dtAkhir
        Me.LayoutControlItem5.Location = New System.Drawing.Point(198, 120)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(199, 30)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(199, 30)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(199, 30)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Sampai Tanggal"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtNamaObat
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem8.Text = "Nama Obat"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.txtKodeObat
        Me.LayoutControlItem9.Location = New System.Drawing.Point(311, 90)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(86, 30)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.cboProdusen
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(311, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Nama Produsen"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.txtKodeProdusen
        Me.LayoutControlItem11.Location = New System.Drawing.Point(311, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(86, 30)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'btnTutup
        '
        Me.btnTutup.ImageOptions.Image = Global.eRetail.My.Resources.Resources.close24
        Me.btnTutup.Location = New System.Drawing.Point(203, 155)
        Me.btnTutup.Name = "btnTutup"
        Me.btnTutup.Size = New System.Drawing.Size(189, 30)
        Me.btnTutup.StyleController = Me.LayoutControl1
        Me.btnTutup.TabIndex = 10
        Me.btnTutup.Text = "&Tutup"
        '
        'btnCetak
        '
        Me.btnCetak.ImageOptions.Image = Global.eRetail.My.Resources.Resources.printer24
        Me.btnCetak.Location = New System.Drawing.Point(5, 155)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(188, 30)
        Me.btnCetak.StyleController = Me.LayoutControl1
        Me.btnCetak.TabIndex = 9
        Me.btnCetak.Text = "&Cetak"
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnCetak
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(198, 40)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnTutup
        Me.LayoutControlItem7.Location = New System.Drawing.Point(198, 150)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(199, 40)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'frmLaporanPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 339)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLaporanPembelian"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Laporan Pembelian"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtKodeProdusen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboProdusen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKodeObat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNamaObat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAkhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAwal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKodeSuplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNamaSuplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboJenisLaporan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents cboJenisLaporan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnTutup As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCetak As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtAkhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtAwal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtKodeSuplier As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboNamaSuplier As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKodeObat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNamaObat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKodeProdusen As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboProdusen As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
End Class
