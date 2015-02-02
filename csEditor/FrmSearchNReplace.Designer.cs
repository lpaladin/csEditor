namespace csEditor
{
    partial class FrmSearchNReplace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.btnSearchDown = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.radbtnKMP = new System.Windows.Forms.RadioButton();
            this.radbtnBM = new System.Windows.Forms.RadioButton();
            this.grpAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(12, 9);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(65, 12);
            this.lblPattern.TabIndex = 0;
            this.lblPattern.Text = "查找内容：";
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(83, 6);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(241, 21);
            this.txtPattern.TabIndex = 0;
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(12, 36);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(65, 12);
            this.lblReplace.TabIndex = 0;
            this.lblReplace.Text = "替换内容：";
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(83, 33);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(241, 21);
            this.txtReplace.TabIndex = 3;
            // 
            // btnSearchDown
            // 
            this.btnSearchDown.Location = new System.Drawing.Point(330, 4);
            this.btnSearchDown.Name = "btnSearchDown";
            this.btnSearchDown.Size = new System.Drawing.Size(156, 23);
            this.btnSearchDown.TabIndex = 2;
            this.btnSearchDown.Text = "查找下一个";
            this.btnSearchDown.UseVisualStyleBackColor = true;
            this.btnSearchDown.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(330, 31);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "替换";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(411, 31);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btnReplaceAll.TabIndex = 5;
            this.btnReplaceAll.Text = "替换全部";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // grpAdvanced
            // 
            this.grpAdvanced.Controls.Add(this.radbtnBM);
            this.grpAdvanced.Controls.Add(this.radbtnKMP);
            this.grpAdvanced.Controls.Add(this.lblMode);
            this.grpAdvanced.Location = new System.Drawing.Point(12, 60);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Size = new System.Drawing.Size(469, 44);
            this.grpAdvanced.TabIndex = 6;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "高级";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(19, 21);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(65, 12);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "匹配算法：";
            // 
            // radbtnKMP
            // 
            this.radbtnKMP.AutoSize = true;
            this.radbtnKMP.Checked = true;
            this.radbtnKMP.Location = new System.Drawing.Point(115, 19);
            this.radbtnKMP.Name = "radbtnKMP";
            this.radbtnKMP.Size = new System.Drawing.Size(161, 16);
            this.radbtnKMP.TabIndex = 1;
            this.radbtnKMP.TabStop = true;
            this.radbtnKMP.Text = "Knuth-Morris-Pratt 算法";
            this.radbtnKMP.UseVisualStyleBackColor = true;
            // 
            // radbtnBM
            // 
            this.radbtnBM.AutoSize = true;
            this.radbtnBM.Location = new System.Drawing.Point(318, 19);
            this.radbtnBM.Name = "radbtnBM";
            this.radbtnBM.Size = new System.Drawing.Size(119, 16);
            this.radbtnBM.TabIndex = 1;
            this.radbtnBM.TabStop = true;
            this.radbtnBM.Text = "Boyer-Moore 算法";
            this.radbtnBM.UseVisualStyleBackColor = true;
            // 
            // FrmSearchNReplace
            // 
            this.AcceptButton = this.btnSearchDown;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 116);
            this.Controls.Add(this.grpAdvanced);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnSearchDown);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.lblReplace);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.lblPattern);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSearchNReplace";
            this.Text = "查找 / 替换";
            this.MouseEnter += new System.EventHandler(this.FrmSearchNReplace_MouseEnter);
            this.grpAdvanced.ResumeLayout(false);
            this.grpAdvanced.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Button btnSearchDown;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.RadioButton radbtnBM;
        private System.Windows.Forms.RadioButton radbtnKMP;
        private System.Windows.Forms.Label lblMode;
    }
}