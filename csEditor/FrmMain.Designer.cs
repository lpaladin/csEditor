namespace csEditor
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSearchNReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLineNum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLineHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLineHeight = new System.Windows.Forms.ToolStripTextBox();
            this.mnuLineHeightConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDefaultColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectionColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFontDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDebug = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.cmnuEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuSearchNReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBracketMatch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearchNReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtCmdToParse = new System.Windows.Forms.ToolStripTextBox();
            this.btnParse = new System.Windows.Forms.ToolStripButton();
            this.ttCalcResult = new System.Windows.Forms.ToolTip(this.components);
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.txtMain = new csEditor.TextCtrl();
            this.mnuMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.cmnuEdit.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuFormat,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(803, 25);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSaveAs,
            this.toolStripSeparator1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(44, 21);
            this.mnuFile.Text = "文件";
            // 
            // mnuNew
            // 
            this.mnuNew.Image = global::csEditor.Properties.Resources.NewDocumentHS;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(122, 22);
            this.mnuNew.Text = "新建…";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::csEditor.Properties.Resources.OpenSelectedItemHS;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(122, 22);
            this.mnuOpen.Text = "打开…";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Image = global::csEditor.Properties.Resources.saveHS;
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(122, 22);
            this.mnuSaveAs.Text = "另存为…";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Image = global::csEditor.Properties.Resources._1385_Disable_16x16_72;
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(122, 22);
            this.mnuExit.Text = "退出";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUndo,
            this.mnuRedo,
            this.toolStripSeparator2,
            this.mnuCut,
            this.mnuCopy,
            this.mnuPaste,
            this.toolStripSeparator7,
            this.mnuSearchNReplace});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(44, 21);
            this.mnuEdit.Text = "编辑";
            // 
            // mnuUndo
            // 
            this.mnuUndo.Image = global::csEditor.Properties.Resources._112_ArrowReturnLeft_Blue_16x16_72;
            this.mnuUndo.Name = "mnuUndo";
            this.mnuUndo.Size = new System.Drawing.Size(137, 22);
            this.mnuUndo.Text = "撤销";
            this.mnuUndo.Visible = false;
            // 
            // mnuRedo
            // 
            this.mnuRedo.Image = global::csEditor.Properties.Resources._112_ArrowReturnRight_Blue_16x16_72;
            this.mnuRedo.Name = "mnuRedo";
            this.mnuRedo.Size = new System.Drawing.Size(137, 22);
            this.mnuRedo.Text = "重做";
            this.mnuRedo.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(134, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // mnuCut
            // 
            this.mnuCut.Image = global::csEditor.Properties.Resources.CutHS;
            this.mnuCut.Name = "mnuCut";
            this.mnuCut.Size = new System.Drawing.Size(137, 22);
            this.mnuCut.Text = "剪切";
            this.mnuCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Image = global::csEditor.Properties.Resources.CopyHS;
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(137, 22);
            this.mnuCopy.Text = "复制";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Image = global::csEditor.Properties.Resources.PasteHS;
            this.mnuPaste.Name = "mnuPaste";
            this.mnuPaste.Size = new System.Drawing.Size(137, 22);
            this.mnuPaste.Text = "粘贴";
            this.mnuPaste.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(134, 6);
            // 
            // mnuSearchNReplace
            // 
            this.mnuSearchNReplace.Image = global::csEditor.Properties.Resources.FindHS;
            this.mnuSearchNReplace.Name = "mnuSearchNReplace";
            this.mnuSearchNReplace.Size = new System.Drawing.Size(137, 22);
            this.mnuSearchNReplace.Text = "查找 / 替换";
            this.mnuSearchNReplace.Click += new System.EventHandler(this.mnuSearchNReplace_Click);
            // 
            // mnuFormat
            // 
            this.mnuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWordWrap,
            this.mnuLineNum,
            this.toolStripSeparator6,
            this.mnuFont});
            this.mnuFormat.Name = "mnuFormat";
            this.mnuFormat.Size = new System.Drawing.Size(44, 21);
            this.mnuFormat.Text = "格式";
            // 
            // mnuWordWrap
            // 
            this.mnuWordWrap.CheckOnClick = true;
            this.mnuWordWrap.Image = global::csEditor.Properties.Resources.HtmlBalanceBracesHS;
            this.mnuWordWrap.Name = "mnuWordWrap";
            this.mnuWordWrap.Size = new System.Drawing.Size(124, 22);
            this.mnuWordWrap.Text = "自动换行";
            this.mnuWordWrap.Visible = false;
            this.mnuWordWrap.Click += new System.EventHandler(this.mnuWordWrap_Click);
            // 
            // mnuLineNum
            // 
            this.mnuLineNum.CheckOnClick = true;
            this.mnuLineNum.Image = global::csEditor.Properties.Resources.List_NumberedHS;
            this.mnuLineNum.Name = "mnuLineNum";
            this.mnuLineNum.Size = new System.Drawing.Size(124, 22);
            this.mnuLineNum.Text = "行号";
            this.mnuLineNum.Click += new System.EventHandler(this.mnuLineNum_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // mnuFont
            // 
            this.mnuFont.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLineHeight,
            this.mnuDefaultColor,
            this.mnuSelectionColor,
            this.mnuFontDialog});
            this.mnuFont.Image = global::csEditor.Properties.Resources.FontHS;
            this.mnuFont.Name = "mnuFont";
            this.mnuFont.Size = new System.Drawing.Size(124, 22);
            this.mnuFont.Text = "字体";
            // 
            // mnuLineHeight
            // 
            this.mnuLineHeight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLineHeight,
            this.mnuLineHeightConfirm});
            this.mnuLineHeight.Name = "mnuLineHeight";
            this.mnuLineHeight.Size = new System.Drawing.Size(133, 22);
            this.mnuLineHeight.Text = "行高";
            // 
            // txtLineHeight
            // 
            this.txtLineHeight.Name = "txtLineHeight";
            this.txtLineHeight.Size = new System.Drawing.Size(100, 23);
            this.txtLineHeight.Text = "15";
            // 
            // mnuLineHeightConfirm
            // 
            this.mnuLineHeightConfirm.Image = global::csEditor.Properties.Resources._109_AllAnnotations_Default_16x16_72;
            this.mnuLineHeightConfirm.Name = "mnuLineHeightConfirm";
            this.mnuLineHeightConfirm.Size = new System.Drawing.Size(160, 22);
            this.mnuLineHeightConfirm.Text = "确认更改";
            this.mnuLineHeightConfirm.Click += new System.EventHandler(this.mnuLineHeightConfirm_Click);
            // 
            // mnuDefaultColor
            // 
            this.mnuDefaultColor.Name = "mnuDefaultColor";
            this.mnuDefaultColor.Size = new System.Drawing.Size(133, 22);
            this.mnuDefaultColor.Text = "默认颜色...";
            this.mnuDefaultColor.Click += new System.EventHandler(this.mnuDefaultColor_Click);
            // 
            // mnuSelectionColor
            // 
            this.mnuSelectionColor.BackColor = System.Drawing.Color.LightCoral;
            this.mnuSelectionColor.Name = "mnuSelectionColor";
            this.mnuSelectionColor.Size = new System.Drawing.Size(133, 22);
            this.mnuSelectionColor.Text = "选中颜色...";
            this.mnuSelectionColor.Click += new System.EventHandler(this.mnuSelectionColor_Click);
            // 
            // mnuFontDialog
            // 
            this.mnuFontDialog.Name = "mnuFontDialog";
            this.mnuFontDialog.Size = new System.Drawing.Size(133, 22);
            this.mnuFontDialog.Text = "字体...";
            this.mnuFontDialog.Click += new System.EventHandler(this.mnuFontDialog_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 21);
            this.mnuHelp.Text = "帮助";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::csEditor.Properties.Resources._109_AllAnnotations_Info_16x16_72;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(110, 22);
            this.mnuAbout.Text = "关于…";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblDebug});
            this.ssMain.Location = new System.Drawing.Point(0, 518);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(803, 22);
            this.ssMain.TabIndex = 1;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(741, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "就绪。";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDebug
            // 
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(47, 17);
            this.lblDebug.Text = "Debug";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.txtMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(803, 463);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 27);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(803, 488);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // cmnuEdit
            // 
            this.cmnuEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuCut,
            this.cmnuCopy,
            this.cmnuPaste,
            this.toolStripSeparator8,
            this.cmnuSearchNReplace,
            this.mnuBracketMatch});
            this.cmnuEdit.Name = "cmnuEdit";
            this.cmnuEdit.Size = new System.Drawing.Size(161, 120);
            // 
            // cmnuCut
            // 
            this.cmnuCut.Image = global::csEditor.Properties.Resources.CutHS;
            this.cmnuCut.Name = "cmnuCut";
            this.cmnuCut.Size = new System.Drawing.Size(160, 22);
            this.cmnuCut.Text = "剪切";
            this.cmnuCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // cmnuCopy
            // 
            this.cmnuCopy.Image = global::csEditor.Properties.Resources.CopyHS;
            this.cmnuCopy.Name = "cmnuCopy";
            this.cmnuCopy.Size = new System.Drawing.Size(160, 22);
            this.cmnuCopy.Text = "复制";
            this.cmnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // cmnuPaste
            // 
            this.cmnuPaste.Image = global::csEditor.Properties.Resources.PasteHS;
            this.cmnuPaste.Name = "cmnuPaste";
            this.cmnuPaste.Size = new System.Drawing.Size(160, 22);
            this.cmnuPaste.Text = "粘贴";
            this.cmnuPaste.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
            // 
            // cmnuSearchNReplace
            // 
            this.cmnuSearchNReplace.Image = global::csEditor.Properties.Resources.FindHS;
            this.cmnuSearchNReplace.Name = "cmnuSearchNReplace";
            this.cmnuSearchNReplace.Size = new System.Drawing.Size(160, 22);
            this.cmnuSearchNReplace.Text = "查找 / 替换";
            this.cmnuSearchNReplace.Click += new System.EventHandler(this.mnuSearchNReplace_Click);
            // 
            // mnuBracketMatch
            // 
            this.mnuBracketMatch.Name = "mnuBracketMatch";
            this.mnuBracketMatch.Size = new System.Drawing.Size(160, 22);
            this.mnuBracketMatch.Text = "匹配右面的括号";
            this.mnuBracketMatch.Click += new System.EventHandler(this.mnuBracketMatch_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator3,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator4,
            this.btnCut,
            this.btnCopy,
            this.toolStripButton8,
            this.toolStripSeparator10,
            this.btnSearchNReplace,
            this.toolStripSeparator5,
            this.btnAbout});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(396, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnNew
            // 
            this.btnNew.Image = global::csEditor.Properties.Resources.NewDocumentHS;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(52, 22);
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::csEditor.Properties.Resources.OpenSelectedItemHS;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(52, 22);
            this.btnOpen.Text = "打开";
            this.btnOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::csEditor.Properties.Resources.saveHS;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = global::csEditor.Properties.Resources._112_ArrowReturnLeft_Blue_16x16_72;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "撤销";
            this.btnUndo.Visible = false;
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = global::csEditor.Properties.Resources._112_ArrowReturnRight_Blue_16x16_72;
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(23, 22);
            this.btnRedo.Text = "还原";
            this.btnRedo.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::csEditor.Properties.Resources.CutHS;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Text = "剪切";
            this.btnCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::csEditor.Properties.Resources.CopyHS;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "复制";
            this.btnCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::csEditor.Properties.Resources.PasteHS;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "粘贴";
            this.toolStripButton8.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearchNReplace
            // 
            this.btnSearchNReplace.Image = global::csEditor.Properties.Resources.FindHS;
            this.btnSearchNReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearchNReplace.Name = "btnSearchNReplace";
            this.btnSearchNReplace.Size = new System.Drawing.Size(89, 22);
            this.btnSearchNReplace.Text = "查找 / 替换";
            this.btnSearchNReplace.Click += new System.EventHandler(this.mnuSearchNReplace_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::csEditor.Properties.Resources._109_AllAnnotations_Info_16x16_72;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(52, 22);
            this.btnAbout.Text = "关于";
            this.btnAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtCmdToParse,
            this.btnParse});
            this.toolStrip2.Location = new System.Drawing.Point(399, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(300, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel1.Text = "QuickParse™";
            // 
            // txtCmdToParse
            // 
            this.txtCmdToParse.Name = "txtCmdToParse";
            this.txtCmdToParse.Size = new System.Drawing.Size(100, 25);
            this.txtCmdToParse.ToolTipText = "在这里输入表达式";
            this.txtCmdToParse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmdToParse_KeyDown);
            this.txtCmdToParse.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtCmdToParse_MouseMove);
            // 
            // btnParse
            // 
            this.btnParse.Image = global::csEditor.Properties.Resources._112_RightArrowShort_Green_16x16_72;
            this.btnParse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(72, 22);
            this.btnParse.Text = "Parse！";
            this.btnParse.ToolTipText = "计算结果！";
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // ttCalcResult
            // 
            this.ttCalcResult.AutomaticDelay = 100;
            this.ttCalcResult.AutoPopDelay = 1000;
            this.ttCalcResult.InitialDelay = 10;
            this.ttCalcResult.IsBalloon = true;
            this.ttCalcResult.ReshowDelay = 20;
            this.ttCalcResult.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttCalcResult.ToolTipTitle = "计算结果";
            // 
            // ofd
            // 
            this.ofd.Title = "打开文件...";
            // 
            // sfd
            // 
            this.sfd.Title = "另存文件...";
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.BackColor = System.Drawing.Color.White;
            this.txtMain.ContextMenuStrip = this.cmnuEdit;
            this.txtMain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMain.LineHeight = 15;
            this.txtMain.Location = new System.Drawing.Point(3, 3);
            this.txtMain.Name = "txtMain";
            this.txtMain.SelectionColor = System.Drawing.Color.LightCoral;
            this.txtMain.ShowLineNum = false;
            this.txtMain.Size = new System.Drawing.Size(797, 457);
            this.txtMain.TabIndex = 0;
            this.txtMain.WordWrap = false;
            this.txtMain.LineChange += new csEditor.TextCtrl.OnLineChange(this.txtMain_LineChange);
            this.txtMain.RequestFind += new csEditor.TextCtrl.OnRequestFind(this.txtMain_RequestFind);
            this.txtMain.DebugMsgSend += new csEditor.TextCtrl.OnDebugMsgSend(this.txtMain_DebugMsgSend);
            this.txtMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtMain_MouseMove);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 540);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FrmMain";
            this.Text = "文本编辑器 - 未命名";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.cmnuEdit.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuCut;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuPaste;
        private System.Windows.Forms.ToolStripMenuItem mnuFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuWordWrap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuFont;
        private System.Windows.Forms.ToolStripMenuItem mnuLineNum;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtCmdToParse;
        private System.Windows.Forms.ToolStripButton btnParse;
        private System.Windows.Forms.ToolTip ttCalcResult;
        private TextCtrl txtMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchNReplace;
        private System.Windows.Forms.ContextMenuStrip cmnuEdit;
        private System.Windows.Forms.ToolStripButton btnSearchNReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuLineHeight;
        private System.Windows.Forms.ToolStripTextBox txtLineHeight;
        private System.Windows.Forms.ToolStripMenuItem mnuDefaultColor;
        private System.Windows.Forms.ToolStripMenuItem mnuFontDialog;
        private System.Windows.Forms.ToolStripMenuItem mnuLineHeightConfirm;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStripStatusLabel lblDebug;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectionColor;
        private System.Windows.Forms.ToolStripMenuItem cmnuCut;
        private System.Windows.Forms.ToolStripMenuItem cmnuCopy;
        private System.Windows.Forms.ToolStripMenuItem cmnuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem cmnuSearchNReplace;
        private System.Windows.Forms.ToolStripMenuItem mnuBracketMatch;
    }
}

