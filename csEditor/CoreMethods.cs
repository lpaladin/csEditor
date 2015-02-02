//////////////////////////////////////////////
// CoreMethods.cs：文本编辑器调用核心函数接口
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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace csEditor
{

    /// <summary>
    /// 文本编辑器核心函数静态类
    /// </summary>
    internal static class CoreMethods
    {
        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Load(string path);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Save(string path = "");

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InsertS(ref CPosition pos, string str, ref CPosition endPos);
        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool InsertC(ref CPosition pos, char chr);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Drag(ref CPosition start, ref CPosition end, ref CPosition target, bool isMove);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Remove(ref CPosition start, ref CPosition end);
        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool RemoveLine(int line);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Backspace(ref CPosition pos);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MatchBracket(ref CPosition inpos, ref CPosition outpos);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Search(string pattern, ref CPosition pos, ref CPosition resultS, ref CPosition resultE);
        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl,EntryPoint = "Search")]
        internal static extern void KMPSearch(string pattern, ref CPosition pos, ref CPosition resultS, ref CPosition resultE);
        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ReplaceAll(string pattern, string target);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Replace(string target, ref CPosition start, ref CPosition end, ref CPosition endPos);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetLineCount();

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UpdateBuffer(IntPtr[] texts, int[] map);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Clear();

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GetSelection(ref CPosition start, ref CPosition end, 
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder result); // 转换托管类型为非托管

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetWordWrap(int viewWidth);

        [DllImport("cEditorCore.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GetLineSizes(int[] sizes);


    }
}
