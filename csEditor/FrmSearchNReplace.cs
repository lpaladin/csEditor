//////////////////////////////////////////////
// FrmSearchNReplace.cs：文本编辑器查找框界面
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csEditor
{
    public partial class FrmSearchNReplace : Form
    {
        private TextCtrl txt; // 文本框引用
        public FrmSearchNReplace(TextCtrl txt)
        {
            this.txt = txt;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CPosition result = new CPosition(), result2 = result, tmp;
            if (txtPattern.Text.Length > 0)
            {
                tmp = txt.CursorAbsolutePosition;
				//int[] sizes = txt.GetLineLengths();
				//while (sizes[tmp.y] == tmp.x)
				//{
				//	if (tmp.y == sizes.Length - 1)
				//	{
				//		tmp.x = tmp.y = 0;
				//		break;
				//	}
				//	tmp.y++;
				//}
                MatchPattern(txtPattern.Text, ref tmp, ref result, ref result2);
                if (result.x < 0)
                    MessageBox.Show("找不到匹配的字符串！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    txt.ScrollToAndSelect(result, result2);
            }
        }

        /// <summary>
        /// 调用核心模块开始匹配算法
        /// </summary>
        /// <param name="pattern">匹配模版</param>
        /// <param name="pos">起始位置</param>
        /// <param name="resultB">找到字串的起始点</param>
        /// <param name="resultE">找到字串的终止点</param>
        private void MatchPattern(string pattern, ref CPosition pos, ref CPosition resultB, ref CPosition resultE)
        {
            if (radbtnBM.Checked)
                CoreMethods.Search(pattern, ref pos, ref resultB, ref resultE);
            else
                CoreMethods.KMPSearch(pattern, ref pos, ref resultB, ref resultE);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            CPosition result = new CPosition(), result2 = result, tmp;
            if (txtPattern.Text.Length > 0)
            {
                tmp = txt.CursorAbsolutePosition;
                int[] sizes = txt.GetLineLengths();
                while (sizes[tmp.y] == tmp.x)
                {
                    if (tmp.y == sizes.Length - 1)
                    {
                        tmp.x = tmp.y = 0;
                        break;
                    }
                    tmp.y++;
                }
                MatchPattern(txtPattern.Text, ref tmp, ref result, ref result2);
                if (result.x < 0)
                    MessageBox.Show("找不到匹配的字符串！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    CoreMethods.Replace(txtReplace.Text, ref result, ref result2, ref tmp);
                    txt.UpdateText();
                    txt.ScrollTo(tmp);
                }
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            if (txtPattern.Text.Length > 0)
            {
                MessageBox.Show("替换了" + CoreMethods.ReplaceAll(txtPattern.Text, txtReplace.Text) + "处。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt.ScrollTo(new CPosition());
                txt.UpdateText();
            }
        }

        private void FrmSearchNReplace_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
