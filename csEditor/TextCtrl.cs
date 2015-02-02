//////////////////////////////////////////////
// TextCtrl.cs：文本编辑器的文本框
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
using System.Media;
using System.IO;
using System.Runtime.InteropServices;

namespace csEditor
{
    /// <summary>
    /// 文本框的完整底层实现
    /// </summary>
    public partial class TextCtrl : Control
    {
        public struct CharStyle
        {
            public Color foreColor, backColor;

            public CharStyle(Color fore, Color back)
            {
                foreColor = fore;
                backColor = back;
            }
        }

        public event OnLineChange LineChange;
        public delegate void OnLineChange(object sender);

        public event OnRequestFind RequestFind;
        public delegate void OnRequestFind(object sender);

        public event OnDebugMsgSend DebugMsgSend;
        public delegate void OnDebugMsgSend(string msg);

        private Size txtFieldSize;
        private bool wordWrapEnabled, charWidthUpdated, isSelecting, mouseDown, showLineNum, ctrlState, lastKeyHandled, isDragging;
        private int lineMaxLen, charHeight, lineNumWidth, selectionLength;
        private float charWidth;

        //[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]
        private char[][] texts;
        private int[] sizes, lineNums;
        private IntPtr[] pointers;
        private List<List<CharStyle>> styles;
        private List<Rectangle> selections;
        private CPosition cursorRelativePos, viewAbsolutePos, caretTargetCoord,
            selectionStartPos, selectionEndPos; // 绝对坐标
        private static Rectangle NULL = new Rectangle();
        private const int SAFEZONE = 50;
        private StringFormat currFormat;
        private Color selectionColor;

        /// <summary>
        /// 从字符位置转为实际坐标
        /// </summary>
        /// <param name="relativePos">相对字符坐标</param>
        /// <returns>实际坐标</returns>
        internal CPosition GetExactCoordOfPos(CPosition relativePos)
        {
            return new CPosition((int)(relativePos.x * charWidth + 1), relativePos.y * charHeight);
        }

        /// <summary>
        /// 从实际坐标转为字符坐标
        /// </summary>
        /// <param name="relativeCoord">实际坐标</param>
        /// <returns>字符坐标</returns>
        internal CPosition GetCharPosOfCoord(CPosition relativeCoord)
        {
            return new CPosition((int)((relativeCoord.x - 1.0) / charWidth + 0.5), relativeCoord.y / charHeight);
        }

        internal int[] GetLineLengths()
        {
            return sizes;
        }

        public TextCtrl()
        {
            selectionColor = Color.LightCoral;
            isDragging = false;
            lastKeyHandled = false;
            ctrlState = false;
            mouseDown = false;
            isSelecting = false;
            charWidthUpdated = false;
            showLineNum = false;
            charWidth = 8;
            charHeight = 15;
            lineNumWidth = 50;
            texts = new char[][] { new char[]{} };
            sizes = new int[] { 0 };
            lineNums = new int[] { 0 };
            pointers = new IntPtr[] { Marshal.UnsafeAddrOfPinnedArrayElement(texts[0], 0) };
            styles = new List<List<CharStyle>>();
            selections = new List<Rectangle>();
            //int i;
            //if (File.Exists("test.txt"))
            //    using (StreamReader sr = new StreamReader(new FileStream("test.txt", FileMode.Open)))
            //        while (!sr.EndOfStream)
            //        {
            //            string tmp = sr.ReadLine();
            //            texts.Add(tmp);
            //            styles.Add(new List<CharStyle>());
            //            List<CharStyle> lst = styles[styles.Count - 1];
            //            for (i = 0; i < tmp.Length; i++)
            //                lst.Add(new CharStyle(Color.Black, Color.White));
            //            if (tmp.Length > lineMaxLen)
            //                lineMaxLen = tmp.Length;
            //        }
            cursorRelativePos = new CPosition();
            viewAbsolutePos = new CPosition();
            currFormat = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox);

            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            myCaret.Visible = false;
            UpdateCaretPos();
        }

        /// <summary>
        /// 返回光标对整个文章的绝对位置
        /// </summary>
        public CPosition CursorAbsolutePosition
        {
            get
            {
                return cursorRelativePos + viewAbsolutePos;
            }
        }

        public bool WordWrap
        {
            get
            {
                return wordWrapEnabled;
            }

            set
            {
                if (value != wordWrapEnabled)
                {
                    hScrollBar.Visible = !value;
                    wordWrapEnabled = value;
                    if (value)
                        CoreMethods.SetWordWrap(txtFieldSize.Width);
                    else
                        CoreMethods.SetWordWrap(0);
                    UpdateText();
                }
            }
        }

        /// <summary>
        /// 返回 / 设置每行高度
        /// </summary>
        public int LineHeight
        {
            get
            {
                return charHeight;
            }

            set
            {
                if (value != charHeight)
                {
                    charHeight = value;
                    Invalidate();
                    TextCtrl_Layout(null, null);
                    myCaret.SetHeight(value);
                }
            }
        }

        /// <summary>
        /// 返回 / 设置是否显示行号
        /// </summary>
        public bool ShowLineNum
        {
            get
            {
                return showLineNum;
            }

            set
            {
                if (value != showLineNum)
                {
                    showLineNum = value;
                    UpdateCaretPos();
                    TextCtrl_Layout(null, null);
                }
            }
        }

        /// <summary>
        /// 返回当前行号
        /// </summary>
        public int CurrLine
        {
            get
            {
                return cursorRelativePos.y + viewAbsolutePos.y;
            }
        }

        /// <summary>
        /// 返回总行数
        /// </summary>
        public int LineCount
        {
            get
            {
                return texts.Length;
            }
        }

        /// <summary>
        /// 返回 / 设置选区的颜色
        /// </summary>
        public Color SelectionColor
        {
            get
            {
                return selectionColor;
            }

            set
            {
                if (value != selectionColor)
                {
                    selectionColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 滚动到指定位置并选择文段
        /// </summary>
        /// <param name="start">选区起点</param>
        /// <param name="end">选区终点</param>
        public void ScrollToAndSelect(CPosition start, CPosition end)
        {
            ScrollTo(end);
            selectionStartPos = start;
            selectionEndPos = end;
            UpdateSelection();
        }

        /// <summary>
        /// 滚动到指定位置
        /// </summary>
        /// <param name="pos">滚动到的位置</param>
        public void ScrollTo(CPosition pos)
        {
            CPosition rel = pos - viewAbsolutePos;
            if (rel.x >= 0 && rel.y >= 0 && rel.x <= txtFieldSize.Width && rel.y < txtFieldSize.Height)
            {
                cursorRelativePos = rel;
                UpdateCaretPos();
                return;
            }
            cursorRelativePos.x = pos.x;
            cursorRelativePos.y = pos.y;
            viewAbsolutePos.x = 0;
            viewAbsolutePos.y = 0;
            ValidateCaretPos();
            if (viewAbsolutePos.x + txtFieldSize.Width > lineMaxLen)
            {
                viewAbsolutePos.x = lineMaxLen - txtFieldSize.Width;
                if (viewAbsolutePos.x < 0)
                    viewAbsolutePos.x = 0;
                Invalidate();
            }
            UpdateCaretPos();
            if (LineChange != null)
                LineChange(this);
        }

        /// <summary>
        /// 载入文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>载入是否成功</returns>
        public bool LoadFile(string path)
        {
            if (!CoreMethods.Load(path))
                return false;
            else
            {
                UpdateText();
                TextCtrl_Layout(null, null);
                return true;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件路径，为空则为默认路径</param>
        /// <returns>保存是否成功</returns>
        public bool SaveFile(string path = "")
        {
            return CoreMethods.Save(path);
        }

        /// <summary>
        /// 重写控件的绘制函数以实现文本显示
        /// </summary>
        /// <param name="e">绘制事件的参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!charWidthUpdated)
            {
                charWidth = e.Graphics.MeasureString("AB", Font).Width - e.Graphics.MeasureString("A", Font).Width;
                TextCtrl_Layout(null, null);
                charWidthUpdated = true;
            }
            base.OnPaint(e);
            if (selections.Count > 0)
                foreach (Rectangle rect in selections)
                    e.Graphics.FillRectangle(new SolidBrush(SelectionColor), rect);
            CPosition pos;
            int i, h = texts.Length - viewAbsolutePos.y, w;
            if (h > txtFieldSize.Height)
                h = txtFieldSize.Height;
            if (showLineNum)
            {
                for (i = 0; i < h; i++)
                {
                    pos = GetExactCoordOfPos(new CPosition(0, i));
                    e.Graphics.DrawString((i + viewAbsolutePos.y + 1).ToString(), Font,
                        new SolidBrush(Color.DarkSlateBlue), pos, currFormat);
                    pos.x += lineNumWidth;
                    char[] currLine = texts[i + viewAbsolutePos.y];
                    w = currLine.Length - SAFEZONE - viewAbsolutePos.x;
                    if (w > txtFieldSize.Width)
                        w = txtFieldSize.Width;
                    if (w > 0)
                        e.Graphics.DrawString(
                            new string(currLine, viewAbsolutePos.x, currLine.Length - SAFEZONE - viewAbsolutePos.x), Font,
                            new SolidBrush(ForeColor), pos, currFormat);
                }
            }
            else
            {
                for (i = 0; i < h; i++)
                {
                    pos = GetExactCoordOfPos(new CPosition(0, i));
                    char[] currLine = texts[i + viewAbsolutePos.y];
                    w = currLine.Length - SAFEZONE - viewAbsolutePos.x;
                    if (w > txtFieldSize.Width)
                        w = txtFieldSize.Width;
                    if (w > 0)
                        e.Graphics.DrawString(
                            new string(currLine, viewAbsolutePos.x, currLine.Length - SAFEZONE - viewAbsolutePos.x), Font,
                            new SolidBrush(ForeColor), pos, currFormat);
                }
            }
        }

        /// <summary>
        /// 以动画方式更新光标位置
        /// 若上次动画未结束则立即结束
        /// </summary>
        private void UpdateCaretPos()
        {
            if (timCaretAnimation.Enabled)
            {
                timCaretAnimation.Stop();
                myCaret.Location = caretTargetCoord;
            }
            caretTargetCoord = GetExactCoordOfPos(cursorRelativePos);
            if (showLineNum)
                caretTargetCoord.x += lineNumWidth;
            myCaret.ForceShow(false);
            timCaretAnimation.Start();
        }
        
        /// <summary>
        /// 响应鼠标滚轮事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            int val;
            if (vScrollBar.Enabled)
            {
                val = vScrollBar.Value - e.Delta / 60;
                if (val < 0)
                    vScrollBar.Value = 0;
                else if (val > texts.Length - txtFieldSize.Height)
                    vScrollBar.Value = texts.Length - txtFieldSize.Height;
                else
                    vScrollBar.Value = val;
            }
            else if (hScrollBar.Enabled)
            {
                val = hScrollBar.Value + e.Delta / 60;
                if (val < 0)
                    hScrollBar.Value = 0;
                else if (val > lineMaxLen - txtFieldSize.Width)
                    hScrollBar.Value = lineMaxLen - txtFieldSize.Width;
                else
                    hScrollBar.Value = val;
            }
        }

        /// <summary>
        /// 响应纵向滚动条滚动事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            viewAbsolutePos.y = vScrollBar.Value;
            ValidateCaretPos();
            UpdateCaretPos();
            UpdateSelection();
            Invalidate();
            if (LineChange != null)
                LineChange(this);
        }

        private void TextCtrl_Enter(object sender, EventArgs e)
        {
            myCaret.Visible = true;
        }

        private void TextCtrl_Leave(object sender, EventArgs e)
        {
            myCaret.Visible = false;
        }

        private void hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            viewAbsolutePos.x = hScrollBar.Value;
            ValidateCaretPos();
            UpdateCaretPos();
            UpdateSelection();
            Invalidate();
            if (LineChange != null)
                LineChange(this);
        }

        /// <summary>
        /// 检查光标位置并更正。
        /// </summary>
        /// <returns>若没有出现越界，则返回真，反之为假。</returns>
        private bool ValidateCaretPos()
        {
            bool result = true;
            if (cursorRelativePos.y < 0)
                if (viewAbsolutePos.y + cursorRelativePos.y > 0)
                {
                    viewAbsolutePos.y += cursorRelativePos.y;
                    cursorRelativePos.y = 0;
                }
                else
                {
                    cursorRelativePos.y = 0;
                    viewAbsolutePos.y = 0;
                    result = false;
                }
            int remain = texts.Length - viewAbsolutePos.y;
            if (txtFieldSize.Height < remain)
                remain = txtFieldSize.Height;
            if (cursorRelativePos.y >= remain)
                if (texts.Length < txtFieldSize.Height)
                {
                    cursorRelativePos.y = texts.Length - 1;
                    viewAbsolutePos.y = 0;
                    result = false;
                }
                else if (viewAbsolutePos.y + cursorRelativePos.y < texts.Length)
                {
                    viewAbsolutePos.y += cursorRelativePos.y - txtFieldSize.Height + 1;
                    cursorRelativePos.y = txtFieldSize.Height - 1;
                }
                else
                {
                    cursorRelativePos.y = txtFieldSize.Height - 1;
                    viewAbsolutePos.y = texts.Length - txtFieldSize.Height;
                    result = false;
                }
            if (cursorRelativePos.x < 0)
                if (viewAbsolutePos.x + cursorRelativePos.x > 0)
                {
                    viewAbsolutePos.x += cursorRelativePos.x;
                    cursorRelativePos.x = 0;
                }
                else
                {
                    cursorRelativePos.x = 0;
                    viewAbsolutePos.x = 0;
                    result = false;
                }
            remain = texts[cursorRelativePos.y + viewAbsolutePos.y].Length - SAFEZONE - viewAbsolutePos.x;
            if (remain > txtFieldSize.Width)
                remain = txtFieldSize.Width;
            if (cursorRelativePos.x > remain)
                if (texts[cursorRelativePos.y + viewAbsolutePos.y].Length - SAFEZONE < txtFieldSize.Width)
                {
                    cursorRelativePos.x = texts[cursorRelativePos.y + viewAbsolutePos.y].Length - SAFEZONE;
                    viewAbsolutePos.x = 0;
                    result = false;
                }
                else if (viewAbsolutePos.x + cursorRelativePos.x < texts[cursorRelativePos.y + viewAbsolutePos.y].Length - SAFEZONE)
                {
                    viewAbsolutePos.x += cursorRelativePos.x - txtFieldSize.Width;
                    cursorRelativePos.x = txtFieldSize.Width;
                }
                else
                {
                    cursorRelativePos.x = txtFieldSize.Width;
                    viewAbsolutePos.x = texts[cursorRelativePos.y + viewAbsolutePos.y].Length - SAFEZONE - txtFieldSize.Width;
                    result = false;
                }
            return result;
        }

        /// <summary>
        /// 重写判断按键是否算作输入的函数，以使控件能响应所有的按键
        /// </summary>
        /// <param name="keyData">按键</param>
        /// <returns>真</returns>
        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        /// <summary>
        /// 复制选区
        /// </summary>
        public void CopySelection()
        {
            if (!isSelecting)
            {
                SystemSounds.Beep.Play();
                return;
            }
            StringBuilder selection = new StringBuilder(selectionLength + 1);
            CoreMethods.GetSelection(ref selectionStartPos, ref selectionEndPos, selection);
            Clipboard.SetText(selection.ToString(), TextDataFormat.Text);
        }

        /// <summary>
        /// 剪切选区
        /// </summary>
        public void CutSelection()
        {
            if (!isSelecting)
            {
                SystemSounds.Beep.Play();
                return;
            }
            StringBuilder selection = new StringBuilder(selectionLength + 1);
            CoreMethods.GetSelection(ref selectionStartPos, ref selectionEndPos, selection);
            Clipboard.SetText(selection.ToString(), TextDataFormat.Text);
            if (!CoreMethods.Remove(ref selectionStartPos, ref selectionEndPos))
                if (DebugMsgSend != null)
                    DebugMsgSend("::Remove failed!");
            ScrollTo(selectionStartPos);
            ClearSelection();
            UpdateText();
        }

        /// <summary>
        /// 粘贴选区
        /// </summary>
        public void Paste()
        {
            if (isSelecting)
            {
                if (!CoreMethods.Remove(ref selectionStartPos, ref selectionEndPos))
                    if (DebugMsgSend != null)
                        DebugMsgSend("::Remove failed!");
            }
            if (!Clipboard.ContainsText())
            {
                SystemSounds.Beep.Play();
                return;
            }
            CPosition tmp = cursorRelativePos + viewAbsolutePos, endPos = new CPosition();
            CoreMethods.InsertS(ref tmp, Clipboard.GetText(), ref endPos);
            UpdateText();
            ScrollTo(endPos);
        }

        /// <summary>
        /// 处理控制键按下的事件，包括功能键、删除键、编辑键的功能实现
        /// </summary>
        /// <param name="sender">事件触发者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            lastKeyHandled = true;
            if (e.KeyCode == Keys.Up)
            {
                cursorRelativePos.y--;
                if (!ValidateCaretPos())
                    SystemSounds.Beep.Play();
                UpdateCaretPos();
                ArrowKeyEndProc();
            }
            else if (e.KeyCode == Keys.Down)
            {
                cursorRelativePos.y++;
                if (!ValidateCaretPos())
                    SystemSounds.Beep.Play();
                UpdateCaretPos();
                ArrowKeyEndProc();
            }
            else if (e.KeyCode == Keys.Left)
                CaretStep(false);
            else if (e.KeyCode == Keys.Right)
                CaretStep(true);
            else if (e.KeyCode == Keys.Home)
            {
                cursorRelativePos.x = viewAbsolutePos.x = 0;
                ValidateCaretPos();
                UpdateCaretPos();
                Invalidate();
            }
            else if (e.KeyCode == Keys.End)
            {
                viewAbsolutePos.x = 0;
                cursorRelativePos.x = lineMaxLen;
                ValidateCaretPos();
                UpdateCaretPos();
                Invalidate();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                cursorRelativePos.y -= txtFieldSize.Height;
                ValidateCaretPos();
                UpdateCaretPos();
                Invalidate();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                cursorRelativePos.y += txtFieldSize.Height;
                ValidateCaretPos();
                UpdateCaretPos();
                Invalidate();
            }
            else if (e.KeyCode == Keys.C && e.Control)
                CopySelection();
            else if (e.KeyCode == Keys.V && e.Control)
                Paste();
            else if (e.KeyCode == Keys.F && e.Control)
                RequestFind(this);
            else if (e.KeyCode == Keys.S && e.Control)
                SaveFile();
            else if (e.KeyCode == Keys.A && e.Control)
            {
                selectionStartPos.x = selectionStartPos.y = 0;
                selectionEndPos.y = texts.Length - 1;
                selectionEndPos.x = sizes[texts.Length - 1];
                isSelecting = true;
                UpdateSelection();
            }
            else if (e.KeyCode == Keys.D && e.Control)
            {
                CoreMethods.RemoveLine(cursorRelativePos.y + viewAbsolutePos.y);
                UpdateText();
                ValidateCaretPos();
                UpdateCaretPos();
            }
            else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (isSelecting)
                {
                    if (!CoreMethods.Remove(ref selectionStartPos, ref selectionEndPos))
                        if (DebugMsgSend != null)
                            DebugMsgSend("::Remove failed!");
                    ScrollTo(selectionStartPos);
                    ClearSelection();
                    UpdateText();
                }
                else
                {
                    if (e.KeyCode == Keys.Delete && !CaretStep(true, true))
                        return;
                    CPosition tmp = cursorRelativePos + viewAbsolutePos;
                    if (!CoreMethods.Backspace(ref tmp))
                        if (DebugMsgSend != null)
                            DebugMsgSend("::Backspace failed!");
                    CaretStep(false, true);
                    tmp = cursorRelativePos + viewAbsolutePos;
                    UpdateText();
                    UpdateScrollBars();
                    cursorRelativePos = tmp - viewAbsolutePos;
                    ValidateCaretPos();
                    UpdateCaretPos();
                }
            }
            else
                lastKeyHandled = false;
            ctrlState = e.Control;
        }

        /// <summary>
        /// 控制光标前进/后退一格，会自动换行
        /// </summary>
        /// <param name="isForward">前进？</param>
        /// <param name="noUpdate">是否更新光标位置</param>
        /// <returns>移动成功与否</returns>
        private bool CaretStep(bool isForward, bool noUpdate = false)
        {
            bool result = true;
            if (isForward)
            {
                cursorRelativePos.x++;
                if (!ValidateCaretPos())
                {
                    cursorRelativePos.y++;
                    viewAbsolutePos.x = cursorRelativePos.x = 0;
                    if (!ValidateCaretPos())
                    {
                        viewAbsolutePos.x = 0;
                        cursorRelativePos.x = lineMaxLen;
                        SystemSounds.Beep.Play();
                        result = false;
                    }
                    else
                        viewAbsolutePos.x = cursorRelativePos.x = 0;
                    ValidateCaretPos();
                }
                if (!noUpdate)
                    UpdateCaretPos();
                ArrowKeyEndProc();
            }
            else
            {
                cursorRelativePos.x--;
                if (!ValidateCaretPos())
                {
                    cursorRelativePos.y--;
                    if (!ValidateCaretPos())
                    {
                        SystemSounds.Beep.Play();
                        result = false;
                    }
                    else
                    {
                        viewAbsolutePos.x = 0;
                        cursorRelativePos.x = lineMaxLen;
                        ValidateCaretPos();
                    }
                }
                if (!noUpdate)
                    UpdateCaretPos();
                ArrowKeyEndProc();
            }
            return result;
        }

        /// <summary>
        /// 清空选区
        /// </summary>
        private void ClearSelection()
        {
            selections.Clear();
            selectionStartPos = selectionEndPos = cursorRelativePos + viewAbsolutePos;
            isSelecting = false;
        }

        /// <summary>
        /// 方向键按后处理
        /// </summary>
        private void ArrowKeyEndProc()
        {
            vScrollBar.Value = viewAbsolutePos.y;
            if (hScrollBar.Visible)
                hScrollBar.Value = viewAbsolutePos.x;
            ClearSelection();
            if (LineChange != null)
                LineChange(this);
        }

        /// <summary>
        /// 更新滚动条参数
        /// </summary>
        private void UpdateScrollBars()
        {
            if (txtFieldSize.Height >= texts.Length)
                vScrollBar.Enabled = false;
            else
            {
                vScrollBar.Enabled = true;
                vScrollBar.Maximum = texts.Length - 1;
                int val = viewAbsolutePos.y;
                if (val > texts.Length - txtFieldSize.Height)
                {
                    val = texts.Length - txtFieldSize.Height;
                    viewAbsolutePos.y = val;
                }
                vScrollBar.Value = val;
                val = txtFieldSize.Height;
                vScrollBar.LargeChange = val;
            }
            if (!WordWrap)
                if (txtFieldSize.Width >= lineMaxLen)
                    hScrollBar.Enabled = false;
                else
                {
                    hScrollBar.Enabled = true;
                    hScrollBar.Maximum = lineMaxLen - 1;
                    int val = viewAbsolutePos.x;
                    if (val > lineMaxLen - txtFieldSize.Width)
                    {
                        val = lineMaxLen - txtFieldSize.Width;
                        viewAbsolutePos.x = val;
                    }
                    hScrollBar.Value = val;
                    val = txtFieldSize.Width;
                    hScrollBar.LargeChange = val;
                }
        }


        /// <summary>
        /// 当文本框大小发生变化时的适应操作
        /// </summary>
        /// <param name="sender">事件触发者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_Layout(object sender, LayoutEventArgs e)
        {
            if (Width < 100 && Height < 100)
                return;
            if (showLineNum)
                txtFieldSize.Width = (int)((this.Width - vScrollBar.Width - lineNumWidth) / charWidth);
            else
                txtFieldSize.Width = (int)((this.Width - vScrollBar.Width) / charWidth);

            if (hScrollBar.Visible)
                txtFieldSize.Height = (this.Height - hScrollBar.Height) / charHeight;
            else
                txtFieldSize.Height = this.Height / charHeight;
            UpdateScrollBars();
            UpdateSelection();
            Invalidate();
            if (LineChange != null)
                LineChange(this);
        }

        /// <summary>
        /// 处理鼠标按下事件，包括拖动与选择的处理
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (isSelecting)
            {
                CPosition mousePos;
                if (showLineNum)
                {
                    if (e.X > lineNumWidth)
                        mousePos = new CPosition(e.X - lineNumWidth, e.Y);
                    else
                        mousePos = new CPosition(0, e.Y);
                }
                else
                    mousePos = new CPosition(e.X, e.Y);
                if (selections.Find((Rectangle rect) => rect.Contains(mousePos)) != NULL)
                {
                    isDragging = true;
                    return;
                }
                else
                    isDragging = false;
            }
            mouseDown = true;
            isSelecting = false;
            if (showLineNum)
            {
                if (e.X > lineNumWidth)
                    cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X - lineNumWidth, e.Y));
                else
                    cursorRelativePos = GetCharPosOfCoord(new CPosition(0, e.Y));
            }
            else
                cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X, e.Y));
            ValidateCaretPos();
            selectionStartPos = cursorRelativePos + viewAbsolutePos;
            UpdateCaretPos();
        }

        /// <summary>
        /// 在选择区域组中添加一个矩形选区。
        /// </summary>
        /// <param name="start">选区开始处的相对坐标</param>
        /// <param name="end">选区结束处的相对坐标</param>
        /// <returns>矩形中的字串长度</returns>
        private int AddRectangle(CPosition start, CPosition end)
        {
            if (start == end)
                return 0;
            int result = end.x - start.x;
            start = GetExactCoordOfPos(start);
            end.y++;
            end = GetExactCoordOfPos(end);
            if (showLineNum)
                selections.Add(new Rectangle(start + new CPosition(lineNumWidth, 0), end - start));
            else
                selections.Add(new Rectangle(start, end - start));
            return result;
        }

        /// <summary>
        /// 根据选区始终点坐标更新选区、绘制矩形
        /// </summary>
        private void UpdateSelection()
        {
            CPosition start, end;
            if (selectionEndPos == selectionStartPos)
            {
                selections.Clear();
                isSelecting = false;
                return;
            }
            else
                isSelecting = true;
            selections.Clear();
            if (selectionEndPos > selectionStartPos)
            {
                start = selectionStartPos;
                end = selectionEndPos;
            }
            else
            {
                start = selectionEndPos;
                end = selectionStartPos;
            }
            selectionLength = 0;
            for (; start.y < end.y; start.y++, start.x = 0)
                if (start.y >= viewAbsolutePos.y && start.y < viewAbsolutePos.y + txtFieldSize.Height)
                    selectionLength += AddRectangle(start - viewAbsolutePos,
                        new CPosition(texts[start.y].Length - SAFEZONE - viewAbsolutePos.x, start.y - viewAbsolutePos.y)) + 1;
            selectionLength += AddRectangle(start - viewAbsolutePos, end - viewAbsolutePos);
            UpdateCaretPos();
            Invalidate();
        }

        /// <summary>
        /// 处理鼠标移动事件，包括拖动与选择的处理
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mouseAbsPos = e.Location + viewAbsolutePos;
                if (selections.Find((Rectangle rect) => rect.Contains(mouseAbsPos)) == NULL)
                {
                    if (ctrlState)
                        Cursor = Cursors.Hand;
                    else
                        Cursor = Cursors.Arrow;
                    if (showLineNum)
                    {
                        if (e.X > lineNumWidth)
                            cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X - lineNumWidth, e.Y));
                        else
                            cursorRelativePos = GetCharPosOfCoord(new CPosition(0, e.Y));
                    }
                    else
                        cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X, e.Y));
                    ValidateCaretPos();
                    UpdateCaretPos();
                }
                else
                    Cursor = Cursors.No;
            }
            else if (mouseDown)
            {
                if (showLineNum)
                {
                    if (e.X > lineNumWidth)
                        cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X - lineNumWidth, e.Y));
                    else
                        cursorRelativePos = GetCharPosOfCoord(new CPosition(0, e.Y));
                }
                else
                    cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X, e.Y));
                ValidateCaretPos();
                selectionEndPos = cursorRelativePos + viewAbsolutePos;
                UpdateSelection();
            }
        }

        /// <summary>
        /// 处理鼠标释放事件，包括拖放结束处理和选中的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (selectionStartPos > selectionEndPos)
            {
                CPosition tmp = selectionEndPos;
                selectionEndPos = selectionStartPos;
                selectionStartPos = tmp;
            }
            if (isDragging)
            {
                Point mouseAbsPos = e.Location + viewAbsolutePos;
                if (selections.Find((Rectangle rect) => rect.Contains(mouseAbsPos)) == NULL)
                {
                    if (showLineNum)
                    {
                        if (e.X > lineNumWidth)
                            cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X - lineNumWidth, e.Y));
                        else
                            cursorRelativePos = GetCharPosOfCoord(new CPosition(0, e.Y));
                    }
                    else
                        cursorRelativePos = GetCharPosOfCoord(new CPosition(e.X, e.Y));
                    ValidateCaretPos();
                    CPosition pos = cursorRelativePos + viewAbsolutePos;
                    if (!CoreMethods.Drag(ref selectionStartPos, ref selectionEndPos, ref pos, !ctrlState))
                        if (DebugMsgSend != null)
                            DebugMsgSend("::Drag failed!");
                    UpdateText();
                    ScrollTo(pos);
                    isSelecting = false;
                }
                isDragging = false;
                Cursor = Cursors.IBeam;
            }
            mouseDown = false;
            if (!isSelecting)
            {
                ClearSelection();
                Invalidate();
            }
        }

        private void TextCtrl_ForeColorChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void TextCtrl_FontChanged(object sender, EventArgs e)
        {
            charWidthUpdated = false;
            Invalidate();
            TextCtrl_Layout(null, null);
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        public void Clear()
        {
            CoreMethods.Clear();
            cursorRelativePos.x = cursorRelativePos.y = viewAbsolutePos.x = viewAbsolutePos.y = 0;
            UpdateCaretPos();
            UpdateText();
        }

        /// <summary>
        /// 更新文本框中的文本
        /// </summary>
        internal void UpdateText()
        {
            lineMaxLen = 0;
            int n = CoreMethods.GetLineCount(), i;
            if (n != texts.Length)
            {
                texts = new char[n][];
                sizes = new int[n];
                pointers = new IntPtr[n];
            }
            if (WordWrap)
            {
                lineNums = new int[n];
            }
            CoreMethods.GetLineSizes(sizes);
            for (i = 0; i < n; i++)
            {
                if (texts[i] == null || texts[i].Length - SAFEZONE != sizes[i])
                {
                    texts[i] = new char[sizes[i] + SAFEZONE];
                }
                pointers[i] = Marshal.UnsafeAddrOfPinnedArrayElement(texts[i], 0);
                if (lineMaxLen < sizes[i])
                    lineMaxLen = sizes[i];
            }
            CoreMethods.UpdateBuffer(pointers, lineNums);
            Invalidate();
        }

        /// <summary>
        /// 处理输入键按下的事件，即输入字符的事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lastKeyHandled)
                return;
            if (isSelecting)
            {
                if (!CoreMethods.Remove(ref selectionStartPos, ref selectionEndPos))
                    if (DebugMsgSend != null)
                        DebugMsgSend("::Remove failed!");
                ScrollTo(selectionStartPos);
                ClearSelection();
            }
            CPosition tmp = cursorRelativePos + viewAbsolutePos;
            if (e.KeyChar == 13)
                e.KeyChar = (char)10;
            if (!CoreMethods.InsertC(ref tmp, e.KeyChar))
                if (DebugMsgSend != null)
                    DebugMsgSend("::InsertChar failed!");
            UpdateText();
            UpdateScrollBars();
            if (e.KeyChar == '\t')
            {
                cursorRelativePos.x += 4;
                UpdateCaretPos();
            }
            else
                CaretStep(true);
        }

        /// <summary>
        /// 处理键释放事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void TextCtrl_KeyUp(object sender, KeyEventArgs e)
        {
            ctrlState = e.Control;
            if (!ctrlState && isDragging && Cursor == Cursors.Arrow)
                Cursor = Cursors.Hand;
            if (ctrlState && isDragging && Cursor == Cursors.Hand)
                Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 动画计时器的每帧处理，实现光标的移动动画
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void timCaretAnimation_Tick(object sender, EventArgs e)
        {
            const int AnimationSpeed = 5;
            int dx = myCaret.Left - caretTargetCoord.x,
                dy = myCaret.Top - caretTargetCoord.y;
            if (dx / AnimationSpeed == 0)
                dx = Math.Sign(dx);
            else
                dx /= AnimationSpeed;
            if (dy / AnimationSpeed == 0)
                dy = Math.Sign(dy);
            else
                dy /= AnimationSpeed;
            if (dx == 0 && dy == 0)
            {
                myCaret.Location = caretTargetCoord;
                timCaretAnimation.Stop();
                myCaret.ForceShow(true);
            }
            else
            {
                myCaret.Top -= dy;
                myCaret.Left -= dx;
            }
        }
    }
    
    /// <summary>
    /// 结构体：指示一个整数坐标
    /// </summary>
    [StructLayout(LayoutKind.Sequential)] // 有序输出以便PInvoke封装
    public struct CPosition
    {
        public int x, y;

        public CPosition(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator>(CPosition a, CPosition b)
        {
            if (a.y == b.y)
                return a.x > b.x;
            return a.y > b.y;
        }

        public static bool operator<(CPosition a, CPosition b)
        {
            if (a.y == b.y)
                return a.x < b.x;
            return a.y < b.y;
        }

        public static bool operator==(CPosition a, CPosition b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator!=(CPosition a, CPosition b)
        {
            return a.x != b.x || a.y != b.y;
        }
            
        public static CPosition operator-(CPosition a, CPosition b)
        {
            return new CPosition(a.x - b.x, a.y - b.y);
        }

        public static CPosition operator+(CPosition a, CPosition b)
        {
            return new CPosition(a.x + b.x, a.y + b.y);
        }

        public static implicit operator PointF(CPosition pos)
        {
            return new PointF(pos.x, pos.y);
        }
            
        public static implicit operator Point(CPosition pos)
        {
            return new Point(pos.x, pos.y);
        }
            
        public static implicit operator Size(CPosition pos)
        {
            return new Size(pos.x, pos.y);
        }
    }
}
