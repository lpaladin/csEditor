//////////////////////////////////////////////
// MyCaret.cs：文本编辑器的光标
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csEditor
{
    /// <summary>
    /// 闪动光标的完整底层实现
    /// </summary>
    public partial class MyCaret : UserControl
    {
        public MyCaret()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            Width = SystemInformation.CaretWidth;
            Height = 15;
            timBlink.Interval = SystemInformation.CaretBlinkTime;
        }

        /// <summary>
        /// 强制光标立刻高亮
        /// </summary>
        public void ForceShow()
        {
            timBlink.Stop();
            BackColor = Color.Black;
            timBlink.Start();
        }

        /// <summary>
        /// 改变强制光标高亮状态
        /// </summary>
        /// <param name="continueFlash">是否还继续闪动</param>
        public void ForceShow(bool continueFlash)
        {
            if (continueFlash)
                timBlink.Start();
            else
                timBlink.Stop();
            BackColor = Color.Black;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }

        private void timBlink_Tick(object sender, EventArgs e)
        {
            if (BackColor == Color.Black)
                BackColor = Color.Transparent;
            else
                BackColor = Color.Black;
        }
    }
}
