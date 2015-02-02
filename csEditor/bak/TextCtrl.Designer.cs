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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.myCaret = new csEditor.MyCaret();
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
            this.myCaret.BackColor = System.Drawing.Color.Transparent;
            this.myCaret.Location = new System.Drawing.Point(12, 18);
            this.myCaret.Name = "myCaret";
            this.myCaret.Size = new System.Drawing.Size(2, 15);
            this.myCaret.TabIndex = 2;
            // 
            // TextCtrl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.myCaret);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DoubleBuffered = true;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "TextCtrl";
            this.Size = new System.Drawing.Size(246, 256);
            this.Load += new System.EventHandler(this.TextCtrl_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextCtrl_DragDrop);
            this.Enter += new System.EventHandler(this.TextCtrl_Enter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextCtrl_KeyUp);
            this.Leave += new System.EventHandler(this.TextCtrl_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private MyCaret myCaret;
    }
}
