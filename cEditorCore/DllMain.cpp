//////////////////////////////////////////////
// DllMain.cpp���ı��༭���ĺ���DLL��������
// ���ߣ������
// ��ע�������޷���C#��ֱ��ʹ��C++�ж�����࣬
// ���������ֱ��ʵ������һ�����󡭡�
// Ϊ�����DLL�����淶�����е���������������
// ������DLLEXPORTǰ׺���������;��������û�
// ������ָ��
//////////////////////////////////////////////


#ifndef HONORCODE
#define HONORCODE
// ����ϵر�֤��
// ���Լ��������������������ӷ�������Ƶ���������й�����
// ��������������У���������ʲô���Ѷ�������ˣ���ô���ҽ��ڳ���ʵϰ������
// ��ϸ���о��������������⣬�Լ����˸��ҵ���ʾ��
// �ڴˣ��Ҹ�лMSDN���ҵ������Ͱ���������ı����У��һ��������ᵽ
// ���ڸ����������ҵİ�����
// �ҵĳ������з������õ�����������ĵ�֮����
// ����̲ġ����ñʼǡ����ϵ�Դ�����Լ������ο����ϵĴ����,
// �Ҷ��Ѿ��ڳ����ע����������ע�������õĳ�����
// �Ҵ�δ��Ϯ�����˵ĳ���Ҳû�е��ñ��˵ĳ���
// �������޸�ʽ�ĳ�Ϯ����ԭ�ⲻ���ĳ�Ϯ��
// �ұ�д������򣬴���û�����Ҫȥ�ƻ���������������ϵͳ��������ת��
// ���ݡ��������������١������
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