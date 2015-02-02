using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;

namespace csEditor
{

    public partial class TextCtrl : UserControl
    {
        public struct CPosition
        {
            public int x, y;

            public CPosition(int x = 0, int y = 0)
            {
                this.x = x;
                this.y = y;
            }
        }

        private Size txtFieldSize;
        private bool wordWrapEnabled;
        private int lineMaxLen;
        private List<string> texts;
        private CPosition cursorRelativePos, viewAbsolutePos;

        private Font currFont;

        internal CPosition GetExactCoordOfPos(CPosition relativePos)
        {
            return new CPosition(relativePos.x * 9, relativePos.y * 15);
        }

        internal void Alarm(string msg)
        {
            MessageBox.Show(msg, "错误 QAQ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public TextCtrl()
        {
            texts = new List<string>(new string[] {
            "Hi! I'm a pig", "I goes ghrr..", "MWAHAHA~", "haha", "hah", "ha", "h", "...", "hey! Can't you say something?",
            "Oh well...", "well..", "well.", "HELL",
            "Hi! I'm a pig", "I goes ghrr..", "MWAHAHA~", "haha", "hah", "ha", "h", "...", "hey! Can't you say something?",});
            lineMaxLen = 30;
            cursorRelativePos = new CPosition();
            viewAbsolutePos = new CPosition();
            currFont = SystemFonts.DefaultFont;

            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            myCaret.Visible = false;
            UpdateCaretPos();
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
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            txtFieldSize.Width = (this.Width - vScrollBar.Width) / 9;
            if (hScrollBar.Visible)
                txtFieldSize.Height = (this.Height - hScrollBar.Height) / 15;
            else
                txtFieldSize.Height = this.Height / 15;
            if (txtFieldSize.Height >= texts.Count)
                vScrollBar.Enabled = false;
            else
            {
                vScrollBar.Enabled = true;
                vScrollBar.Maximum = texts.Count - txtFieldSize.Height;
                int val = viewAbsolutePos.y;
                if (val > vScrollBar.Maximum)
                {
                    val = vScrollBar.Maximum;
                    viewAbsolutePos.y = val;
                }
                vScrollBar.Value = val;
                val = txtFieldSize.Height * vScrollBar.Maximum / texts.Count;
                vScrollBar.LargeChange = val;
            }
            if (!WordWrap)
                if (txtFieldSize.Width >= lineMaxLen)
                    hScrollBar.Enabled = false;
                else
                {
                    hScrollBar.Enabled = true;
                    hScrollBar.Maximum = lineMaxLen - txtFieldSize.Width;
                    int val = viewAbsolutePos.x;
                    if (val > hScrollBar.Maximum)
                    {
                        val = hScrollBar.Maximum;
                        viewAbsolutePos.x = val;
                    }
                    hScrollBar.Value = val;
                    val = txtFieldSize.Width * hScrollBar.Maximum / lineMaxLen;
                    hScrollBar.LargeChange = val;
                }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CPosition pos;
            int i, h = texts.Count - viewAbsolutePos.y;
            if (h > txtFieldSize.Height)
                h = txtFieldSize.Height;
            for (i = 0; i < h; i++)
            {
                pos = GetExactCoordOfPos(new CPosition(0, i));
                string currLine = texts[i + viewAbsolutePos.y];
                if (currLine.Length > viewAbsolutePos.x)
                    e.Graphics.DrawString(currLine.Substring(viewAbsolutePos.x), currFont, new SolidBrush(Color.Black), new PointF(pos.x, pos.y));
            }
        }

        private void TextCtrl_Load(object sender, EventArgs e)
        {

        }

        private void UpdateCaretPos()
        {
            CPosition tmp = GetExactCoordOfPos(cursorRelativePos);
            myCaret.Top = tmp.y;
            myCaret.Left = tmp.x;
        }
        
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll && vScrollBar.Enabled)
                vScrollBar.AutoScrollOffset = new Point(0, se.NewValue - se.OldValue);
            if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll && hScrollBar.Enabled)
                hScrollBar.AutoScrollOffset = new Point(se.NewValue - se.OldValue, 0);
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            viewAbsolutePos.y = vScrollBar.Value;
            Invalidate();
        }

        private void TextCtrl_DragDrop(object sender, DragEventArgs e)
        {

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
            Invalidate();
        }

        private void MoveCaretToExtremePos(bool isHead)
        {
            if (isHead)
                cursorRelativePos.x = 0;
            else
                cursorRelativePos.x = texts[cursorRelativePos.y + viewAbsolutePos.y].Length;
        }

        private void TextCtrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (cursorRelativePos.y > 0)
                    cursorRelativePos.y--;
                else if (viewAbsolutePos.y > 0)
                {
                    viewAbsolutePos.y--;
                    Invalidate();
                }
                else
                    SystemSounds.Beep.Play();
                UpdateCaretPos();
            }
            if (e.KeyCode == Keys.Down)
            {
                if (cursorRelativePos.y < txtFieldSize.Height - 1)
                    cursorRelativePos.y++;
                else if (viewAbsolutePos.y < texts.Count - 1)
                {
                    viewAbsolutePos.y++;
                    Invalidate();
                }
                else
                    SystemSounds.Beep.Play();
                UpdateCaretPos();
            }
            if (e.KeyCode == Keys.Left)
            {
                if (cursorRelativePos.x > 0)
                {
                    cursorRelativePos.x--;
                    Invalidate();
                }
                else if (viewAbsolutePos.x > 0)
                    viewAbsolutePos.x--;
                else
                {
                    if (cursorRelativePos.y > 0)
                    {
                        cursorRelativePos.y--;
                        MoveCaretToExtremePos(false);
                    }
                    else if (viewAbsolutePos.y > 0)
                    {
                        viewAbsolutePos.y--;
                        MoveCaretToExtremePos(false);
                        Invalidate();
                    }
                    else
                        SystemSounds.Beep.Play();
                }
                UpdateCaretPos();
            }
            if (e.KeyCode == Keys.Right)
            {
                if (cursorRelativePos.x < txtFieldSize.Width - 1)
                    cursorRelativePos.x++;
                else if (viewAbsolutePos.x < texts.Count - 1)
                {
                    viewAbsolutePos.x++;
                    Invalidate();
                }
                else
                {
                    if (cursorRelativePos.y < txtFieldSize.Height - 1)
                    {
                        cursorRelativePos.y++;
                        MoveCaretToExtremePos(true);
                    }
                    else if (viewAbsolutePos.y < texts.Count - 1)
                    {
                        viewAbsolutePos.y++;
                        MoveCaretToExtremePos(true);
                        Invalidate();
                    }
                    else
                        SystemSounds.Beep.Play();
                }
                UpdateCaretPos();
            }

        }
    }
}
