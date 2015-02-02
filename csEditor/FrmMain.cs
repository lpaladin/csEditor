//////////////////////////////////////////////
// FrmMain.cs：文本编辑器的界面
// 作者：周昊宇
//////////////////////////////////////////////

#region HONORCODE
// 我真诚地保证：
// 我自己独立地完成了整个程序从分析、设计到编码的所有工作。
// 如果在上述过程中，我遇到了什么困难而求教于人，那么，我将在程序实习报告中
// 详细地列举我所遇到的问题，以及别人给我的提示。
// 在此，我感谢MSDN对我的启发和帮助。下面的报告中，我还会具体地提到
// 它在各个方法对我的帮助。
// 我的程序里中凡是引用到其他程序或文档之处，
// 例如教材、课堂笔记、网上的源代码以及其他参考书上的代码段,
// 我都已经在程序的注释里很清楚地注明了引用的出处。
// 我从未抄袭过别人的程序，也没有盗用别人的程序，
// 不管是修改式的抄袭还是原封不动的抄袭。
// 我编写这个程序，从来没有想过要去破坏或妨碍其他计算机系统的正常运转。
// 蒋捷、邢曜鹏、赵万荣、周昊宇
#endregion

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace csEditor
{
    /// <summary>
    /// 主窗口的界面逻辑
    /// </summary>
    public partial class FrmMain : Form
    {
        ColorDialog cd;
        FontDialog fd;
        FrmSearchNReplace frmSnR;
        string fileName;

        public FrmMain()
        {
            if (!File.Exists("cEditorCore.dll"))
            {
                MessageBox.Show("你大爷！把我的cEditorCore.dll还回来！", "WTF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            InitializeComponent();
            cd = new ColorDialog();
            fd = new FontDialog();
            frmSnR = new FrmSearchNReplace(txtMain);
            string[] argu = Environment.GetCommandLineArgs();
            if (argu.Length == 2 && File.Exists(argu[1]))
                Open(argu[1]);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"文本编辑器
v0.2 Alpha
_________________

作者：（音序）
蒋捷、邢曜鹏、赵万荣、周昊宇");
        }

        private void mnuWordWrap_Click(object sender, EventArgs e)
        {
            txtMain.WordWrap = mnuWordWrap.Checked;
        }

        /// <summary>
        /// QuickParse的实现……
        /// 其实是使用了运行时编译- -
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void btnParse_Click(object sender, EventArgs e)
        {
            Microsoft.CSharp.CSharpCodeProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            param.OutputAssembly = "VolatileClass";
            param.GenerateInMemory = true;
            CompilerResults result = provider.CompileAssemblyFromSource(param,
                  @"using System;

                    class Main
                    {
                        public static object Calc()
                        {
                            return " + txtCmdToParse.Text + @";
                        }
                    }");
            if (result.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder("该句无法解析！错误如下：");
                sb.Append(Environment.NewLine);
                foreach (CompilerError error in result.Errors)
                    sb.Append(error.ErrorText + "\n");
                MessageBox.Show(sb.ToString());
            }
            else
            {
                Assembly ass = result.CompiledAssembly;
                ttCalcResult.Show(
                    ass.GetType("Main").GetMethod("Calc").Invoke(null, null).ToString(), this, 
                    new Point(btnParse.Bounds.Location.X + toolStrip2.Bounds.Location.X,
                        btnParse.Bounds.Location.Y + toolStrip2.Bounds.Location.Y), 5000);
            }
        }

        private void txtCmdToParse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnParse_Click(null, null);
        }

        private void txtCmdToParse_MouseMove(object sender, MouseEventArgs e)
        {
            if (!frmSnR.Visible)
                txtCmdToParse.Focus();
        }

        private void txtMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!frmSnR.Visible)
                txtMain.Focus();
        }

        private void txtMain_LineChange(object sender)
        {
            lblStatus.Text = "当前行：" + (txtMain.CurrLine + 1) + " 总行数：" + txtMain.LineCount;
        }

        private void mnuLineNum_Click(object sender, EventArgs e)
        {
            txtMain.ShowLineNum = mnuLineNum.Checked;
        }

        private void mnuDefaultColor_Click(object sender, EventArgs e)
        {
            cd.Color = txtMain.ForeColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMain.ForeColor = cd.Color;
                mnuDefaultColor.ForeColor = cd.Color;
            }
        }

        private void mnuFontDialog_Click(object sender, EventArgs e)
        {
            fd.Font = txtMain.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtMain.Font = fd.Font;
        }

        private void mnuSearchNReplace_Click(object sender, EventArgs e)
        {
			if (frmSnR.IsDisposed)
				frmSnR = new FrmSearchNReplace(txtMain);
			frmSnR.Show(this);
        }

        private void Open(string path)
        {
            if (!txtMain.LoadFile(path))
                MessageBox.Show("载入文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                fileName = path;
                Text = "文本编辑器 - " + Path.GetFileName(fileName);
            }
        }

        private void mnuLineHeightConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int val = int.Parse(txtLineHeight.Text);
                if (val < 8 || val > 72)
                    throw new Exception();
                txtMain.LineHeight = val;
            }
            catch
            {
                MessageBox.Show("请输入合法字号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLineHeight.Text = txtMain.LineHeight.ToString();
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK && File.Exists(ofd.FileName))
                Open(ofd.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                if (!txtMain.SaveFile())
                    MessageBox.Show("保存文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                mnuSaveAs_Click(null, null);
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (!txtMain.SaveFile(sfd.FileName))
                    MessageBox.Show("保存文件失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    fileName = sfd.FileName;
                    Text = "文本编辑器 - " + Path.GetFileName(fileName);
                }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            txtMain.Clear();
            fileName = null;
            Text = "文本编辑器 - 未命名";
        }

        private void txtMain_DebugMsgSend(string msg)
        {
            lblDebug.Text = msg;
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            txtMain.CutSelection();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            txtMain.CopySelection();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            txtMain.Paste();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("你确定要退出吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                e.Cancel = true;
            frmSnR.Close();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
                Open(files[0]);
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void mnuSelectionColor_Click(object sender, EventArgs e)
        {
            cd.Color = txtMain.SelectionColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMain.SelectionColor = cd.Color;
                mnuSelectionColor.BackColor = cd.Color;
            }
        }

        private void txtMain_RequestFind(object sender)
        {
            mnuSearchNReplace_Click(sender, null);
        }

        private void mnuBracketMatch_Click(object sender, EventArgs e)
        {
            CPosition inPos = txtMain.CursorAbsolutePosition, outPos = new CPosition();
            CoreMethods.MatchBracket(ref inPos, ref outPos);
            if (outPos.x > 0)
                txtMain.ScrollToAndSelect(outPos, outPos + new CPosition(1, 0));
        }
    }
}
