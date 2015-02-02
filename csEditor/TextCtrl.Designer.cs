namespace csEditor
{
    partial class TextCtrl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.myCaret = new csEditor.MyCaret();
            this.timCaretAnimation = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.CausesValidation = false;
            this.vScrollBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(229, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 256);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // hScrollBar
            // 
            this.hScrollBar.CausesValidation = false;
            this.hScrollBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 239);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(229, 17);
            this.hScrollBar.TabIndex = 1;
            this.hScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar_ValueChanged);
            // 
            // myCaret
            // 
            this.myCaret.BackColor = System.Drawing.Color.Black;
            this.myCaret.Location = new System.Drawing.Point(12, 18);
            this.myCaret.Name = "myCaret";
            this.myCaret.Size = new System.Drawing.Size(2, 15);
            this.myCaret.TabIndex = 2;
            // 
            // timCaretAnimation
            // 
            this.timCaretAnimation.Interval = 10;
            this.timCaretAnimation.Tick += new System.EventHandler(this.timCaretAnimation_Tick);
            // 
            // TextCtrl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.myCaret);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Size = new System.Drawing.Size(246, 256);
            this.FontChanged += new System.EventHandler(this.TextCtrl_FontChanged);
            this.ForeColorChanged += new System.EventHandler(this.TextCtrl_ForeColorChanged);
            this.Enter += new System.EventHandler(this.TextCtrl_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextCtrl_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextCtrl_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextCtrl_KeyUp);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.TextCtrl_Layout);
            this.Leave += new System.EventHandler(this.TextCtrl_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextCtrl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextCtrl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextCtrl_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TextCtrl_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private MyCaret myCaret;
        private System.Windows.Forms.Timer timCaretAnimation;
    }
}
