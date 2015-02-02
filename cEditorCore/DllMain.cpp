//////////////////////////////////////////////
// DllMain.cpp：文本编辑器的核心DLL导出函数
// 作者：周昊宇
// 备注：由于无法在C#中直接使用C++中定义的类，
// 　　　因而直接实例化了一个对象……
// 为了配合DLL导出规范，所有导出函数都加上了
// 　　　DLLEXPORT前缀，特殊类型均采用引用或
// 　　　指针
//////////////////////////////////////////////


#ifndef HONORCODE
#define HONORCODE
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
#endif

#include <cstring>
#include "Editor.h"

CEditorCore core;

Ckmp kmp;

CEditorCore* SNodeString::core;

DLLEXPORTHEADER bool Load(char* path)
{
	return core.loadFromFile(path);
}

DLLEXPORTHEADER bool Backspace(CPosition& pos)
{
	return core.backspace(pos);
}

DLLEXPORTHEADER bool Save(char* path = "")
{
	return core.saveToFile(path);
}

DLLEXPORTHEADER bool Remove(CPosition& start, CPosition& end)
{
	return core.remove(start, end).size();
}

DLLEXPORTHEADER bool RemoveLine(int line)
{
	return core.removeLine(line);
}

DLLEXPORTHEADER bool Drag(CPosition& start, CPosition& end, CPosition& target, bool isMove)
{
	return core.drag(start, end, target, isMove);
}

DLLEXPORTHEADER void InsertS(CPosition& pos, char* str, CPosition& endPos)
{
	endPos = core.insert(pos, str);
}

DLLEXPORTHEADER bool InsertC(CPosition& pos, char chr)
{
	return core.insert(pos, chr);
}

DLLEXPORTHEADER void Search(char* pattern, CPosition& pos, CPosition& resultA, CPosition& resultB)
{
	pPos result = core.searchBM(pos, pattern);
	resultA = result.first;
	resultB = result.second;
}

DLLEXPORTHEADER void KMPSearch(char* pattern, CPosition& pos, CPosition& resultA, CPosition& resultB)
{
	pPos result = kmp.find(pattern, pos);
	resultA = result.first;
	resultB = result.second;
}

DLLEXPORTHEADER int ReplaceAll(char* pattern, char* target)
{
	return core.replaceAll(pattern, target);
}

DLLEXPORTHEADER void GetSelection(CPosition& start, CPosition& end, char* result)
{
	strcpy(result, core.getSelection(start, end).c_str());
}

DLLEXPORTHEADER void MatchBracket(CPosition& in, CPosition& out)
{
	out = core.match(in);
}

DLLEXPORTHEADER void Clear()
{
	core.clear();
}

DLLEXPORTHEADER void SetWordWrap(int viewWidth)
{
	core.setWordWrap(viewWidth);
}

DLLEXPORTHEADER int GetLineCount()
{
	return core.getLineCnt();
}

DLLEXPORTHEADER	void GetLineSizes(int* sizes)
{
	core.getLineLength(sizes);
}

DLLEXPORTHEADER void UpdateBuffer(wchar_t* texts[], int* p)
{
	if (!SNodeString::core)
		SNodeString::core = &core;
	core.outputBuffer = texts;
	core.updateBuffer(p);
}

DLLEXPORTHEADER void Replace(char* target, CPosition& start, CPosition& end, CPosition& endPos)
{
	endPos = core.replace(target, start, end);
}