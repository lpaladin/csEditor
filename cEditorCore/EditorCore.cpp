///////////////////////////////////////////////////////////
// EditorCore.cpp：文本编辑器核心DLL的功能模块 - 文本编辑
// 作者：蒋捷
///////////////////////////////////////////////////////////

#define COPY_DRAG 0
#define DEL_DRAG 1
#include "Editor.h"

/* 函数原型：CEditorCore()
 * 完成人：蒋捷
 * 函数功能：CEditorCore的默认构造函数，完成初始化参数的功能
 * 参数：无
 * 返回值：无
 */
CEditorCore::CEditorCore()
{
	data.push_back(string());	// 加入新行
    lineCnt = 1;				// 更新行数
    autoIndent = 0;				// 自动缩进关闭
    autoWordWrap = 0;			// 自动换行关闭
    tabSpace = 4;				// tab空格数默认4
    lineLength = -1;			// 不限制自动换行
    lineWrapCnt = 0;			// 换行后行数
}

/* 函数原型：bool insert(const CPosition &, char c)
 * 完成人：蒋捷
 * 函数功能：在指定位置插入指定字符，若插入换行符则插入一个新的链表，若插入水平制表符则转换为相应数目回车插入
 * 参数：插入位置，插入字符
 * 返回值：若成功插入返回true，否则返回false
 */
bool CEditorCore::insert(const CPosition & cpos, char c)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	if (line >= lineCnt) // 判断是否超过行数范围
	{
		return false;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // 判断是否超过宽度范围
	{
		return false;
	}

	if (c == '\n')
	{
		string tmp = itr->substr(pos, itr->length() - pos);
		itr->erase(pos, itr->length() - pos);
		++itr;
		++lineCnt;
		data.insert(itr, tmp);
	}
	else if (c == '\t')
	{
		for (int i = 0; i < tabSpace; ++i)
		{
			itr->insert(pos, " ");
		}
	}
	else
	{
		itr->insert(pos, string(1, c));
	}
	return true;
}

/* 函数原型：bool paste(const CPosition &)
 * 完成人：蒋捷
 * 函数功能：在指定位置插入剪切板中内容
 * 参数：插入位置
 * 返回值：若成功插入返回true，否则返回false
 */
CPosition CEditorCore::paste(const CPosition & cpos)
{
	return insert(cpos, clipboard);
}

/* 函数原型：CPosition insert(const CPosition &, const string &)
 * 完成人：蒋捷
 * 函数功能：在指定位置插入给定字符串的内容
 * 参数：插入位置，待插入字符串
 * 返回值：插入完成后光标的位置，若插入失败返回原位置
 */
CPosition CEditorCore::insert(const CPosition & cpos, const string & s)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	CPosition cursorPos(pos, line);
	if (line >= lineCnt) // 判断是否超过行数范围
	{
		return cursorPos;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // 判断是否超过宽度范围
	{
		return cursorPos;
	}

	int l = s.length();
	string tmp;
	for (int j = 0; j < l; ++j)
	{
		
		switch (s[j])
		{
        case '\r':
            break;
		case '\n':
			tmp = itr->substr(pos);
			itr->erase(pos);
			++itr;
			++line;
			++lineCnt;	// 维护行数
			itr = data.insert(itr, tmp);
			pos = 0;
			break;
		case '\t':
			for (int i = 0; i < tabSpace; ++i)
			{
				itr->insert(pos, " ");
				++pos;
			}
			break;
		default:
			itr->insert(pos, string(1, s[j]));
			++pos;
		}
	}
	cursorPos.x = pos;
	cursorPos.y = line;
	return cursorPos;
}


/* 函数原型：bool backspace(const CPosition &)
 * 完成人：蒋捷
 * 函数功能：在指定位置删除一个字符，若删除位置位于行首则将两个链表合并
 * 参数：删除位置
 * 返回值：若成功删除返回true，否则返回false
 */
bool CEditorCore::backspace(const CPosition & cpos)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	if (line >= lineCnt) // 判断是否超过行数范围
	{
		return false;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // 判断是否超过宽度范围
	{
		return false;
	}

	if (pos == 0)
	{
		if (itr != data.begin())
		{
			string tmp = *itr;
			itr = data.erase(itr);
			--itr;
			*itr = *itr + tmp;
			--lineCnt;
		}
	}
	else
	{
		itr->erase(pos - 1, 1);
	}
	return true;
}

/* 函数原型：bool removeLine(const int line, const int lineNum = 1)
 * 完成人：蒋捷
 * 函数功能：从指定行开始删除若干行（默认为一行），直到不能删除位置
 * 参数：删除位置
 * 返回值：若成功删除返回true，否则返回false
 */
bool CEditorCore::removeLine(const int line, const int lineNum)
{
	list<string>::iterator itr = data.begin();
	if (line >= lineCnt)
	{
		return false;
	}
	for (int i = 0; i < line; ++i, ++itr);

	for (int i = 0; i < lineNum; ++i)
	{
		itr = data.erase(itr);
		if (itr == data.end())
		{
			break;
		}
	}
	if (--lineCnt == 0)
	{
		data.push_back("");
		lineCnt++;
	}
	return true;
}

/* 函数原型：string remove(const CPosition &, const CPosition &)
 * 完成人：蒋捷
 * 函数功能：删除所选择区域里的所有内容，并返回所删除内容
 * 参数：删除开始位置，截至位置
 * 返回值：若成功删除返回所删除字符串，否则返回空串
 */
string CEditorCore::remove(const CPosition & cposA, const CPosition & cposB)
{
	string tmp;
	int i;
	if (cposA > cposB)
	{
		return tmp;
	}
	list<string>::iterator  itrA, itrB;
	int lineA = cposA.y, posA = cposA.x;
	int lineB = cposB.y, posB = cposB.x;
	if (lineB >= lineCnt) // 判断是否超过行数范围
	{
		return tmp;
	}
	for (i = 0, itrA = data.begin(); i < lineA; ++i, ++itrA);
	for (i = 0, itrB = data.begin(); i < lineB; ++i, ++itrB);
	if (posA > itrA->length() || posB > itrB->length()) // 判断是否超过宽度范围
	{
		return tmp;
	}

	if (itrA == itrB)
	{
		tmp = itrA->substr(posA, posB - posA);
		itrA->erase(posA, posB - posA);
	}
	else
	{
		list<string>::iterator tmpItr = itrA;
		tmpItr++;
		while (tmpItr != itrB)
		{
			tmp += *tmpItr + '\n';
			tmpItr = data.erase(tmpItr);
			--lineCnt;
		}
		tmp = itrA->substr(posA, itrA->length() - posA) + '\n' + tmp;
		itrA->erase(posA, itrA->length() - posA);
		tmp += itrB->substr(0, posB);
		itrB->erase(0, posB);
		*itrA += *itrB;
		data.erase(itrB);
		--lineCnt;
	}
	return tmp;
}

/* 函数原型：string getSelection(const CPosition &, const CPosition &) const
 * 完成人：蒋捷
 * 函数功能：获取所选择区域里的所有内容
 * 参数：开始位置，截至位置
 * 返回值：返回区域内字符串，若失败返回空串
 */
string CEditorCore::getSelection(const CPosition & cposA, const CPosition & cposB) const      // 获得选中的字符串
{
	string tmp;
	int i;
	if (cposA > cposB)
	{
		return tmp;
	}
	list<string>::const_iterator  itrA, itrB;
	int lineA = cposA.y, posA = cposA.x;
	int lineB = cposB.y, posB = cposB.x;
	if (lineB >= lineCnt) // 判断是否超过行数范围
	{
		return tmp;
	}
	for (i = 0, itrA = data.begin(); i < lineA; ++i, ++itrA);
	for (i = 0, itrB = data.begin(); i < lineB; ++i, ++itrB);
	if (posA > itrA->length() || posB > itrB->length()) // 判断是否超过宽度范围
	{
		return tmp;
	}

	if (itrA == itrB)
	{
		tmp = itrA->substr(posA, posB - posA);
	}
	else
	{
		list<string>::const_iterator tmpItr = itrA;
		for (tmpItr++; tmpItr != itrB; tmpItr++)
		{
			tmp += *tmpItr + '\n';
		}
		tmp = itrA->substr(posA, itrA->length() - posA) + '\n' + tmp;
		tmp += itrB->substr(0, posB);
	}
	return tmp;
}

/* 函数原型：bool cut(const CPosition &, const CPosition &)
 * 完成人：蒋捷
 * 函数功能：删除所选择区域里的所有内容，并将内容移动至剪切板
 * 参数：开始位置，截至位置
 * 返回值：返回该操作是否成功
 */
bool CEditorCore::cut(const CPosition & cposA, const CPosition & cposB)              // 剪切段落，指定开始结束位置
{
	string tmp;
	tmp = remove(cposA, cposB);
	if (tmp == "")
	{
		return false;
	}
	else
	{
		clipboard = tmp;
		return true;
	}
}

/* 函数原型：bool replace(const string &, const CPosition &, const CPosition &)
 * 完成人：蒋捷
 * 函数功能：将所选区域的字符串替换成给定字符串
 * 参数：要替换的字符串，开始位置，截至位置
 * 返回值：返回替换是否成功
 */
CPosition CEditorCore::replace(const string & s, const CPosition & cposA, const CPosition & cposB)
{
	remove(cposA, cposB);
	return insert(cposA, s);
}

/* 函数原型：void clear()
 * 完成人：蒋捷
 * 函数功能：将文本全部清空，并清空相关参数
 * 参数：无
 * 返回值：无
 */
void CEditorCore::clear()
{
    data.clear();
	data.push_back("");
    lineCnt = 1;
    lineWrapCnt = 0;
}

/* 函数原型：int getLineCnt()
 * 完成人：蒋捷
 * 函数功能：获取总行数，自动换行开启时返回计算折行的总行数
 * 参数：无
 * 返回值：整型，总行数
 */
int CEditorCore::getLineCnt()
{
    if (!autoWordWrap)
    {
        return lineCnt;
    }
    else
    {
        int i = 0;
        lineWrapCnt = 0;
        list<string>::iterator itr = data.begin();
        for (; i < lineCnt; ++i, ++itr)
        {
            lineWrapCnt += itr->length() / (lineLength + 1) + 1;
        }
        return lineWrapCnt;
    }
}

/* 函数原型：void getLineLength(int * lineSize)
 * 完成人：蒋捷
 * 函数功能：获得每一行的长度
 * 参数：需要提供长度写入的数组
 * 返回值：无
 */
void CEditorCore::getLineLength(int * lineSize)
{
    int i = 0;
    list<string>::iterator itr = data.begin();
    for (; i < lineCnt; ++i, ++itr)
    {
        lineSize[i] = itr->length();
    }
    return;
}

/* 函数原型：void setWordWrap(int)
 * 完成人：蒋捷
 * 函数功能：设置自动换行功能的开启与关闭
 * 参数：每一行的长度，0代表关闭自动换行
 * 返回值：无
 */
void CEditorCore::setWordWrap(int length)
{
    lineLength = length;
    if (length == 0)
    {
        autoWordWrap = 0;
    }
    else
    {
        autoWordWrap = 1;
    }
}


/* 函数原型：int updateBuffer()
 * 完成人：蒋捷
 * 函数功能：更新输出缓冲区，自动换行开启时处理自动换行功能
 * 参数：无
 * 返回值：自动换行未开启时返回单行的最长长度
 */
void CEditorCore::updateBuffer(int* p)
{
	lineMap = p;
    if (autoWordWrap)
    {
        int i = 0, j = 0, k;
        list<string>::iterator itr = data.begin();
        while (itr != data.end())
        {
            lineMap[j] = i;
            for (k = 0; k < lineLength && k < itr->size(); ++k)
            {
                if (k % lineLength == 0)
                    ++i;
                outputBuffer[i][k % lineLength] = (*itr)[k];
            }
            ++i;
            ++j;
			itr++;
        }
        lineWrapCnt = i;
    }
    else
    {
        int i = 0, t, j;
        list<string>::iterator itr = data.begin();
        while (itr != data.end())
        {
            t = itr->length();
			for (j = 0; j < t; j++)
				outputBuffer[i][j] = (*itr)[j];
			i++;
			itr++;
        }
    }
}

/* 函数原型：bool CEditorCore::copy(const CPosition &, const CPosition &)
 * 完成人：蒋捷
 * 函数功能：将指定区域的字符串复制到剪切板
 * 参数：传入开始截至的位置
 * 返回值：成功时返回true
 */
bool CEditorCore::copy(const CPosition & cposA, const CPosition & cposB)
{
    string tmp;
	tmp = getSelection(cposA, cposB);
	if (tmp == "")
	{
		return false;
	}
	else
	{
		clipboard = tmp;
		return true;
	}
}

/* 函数原型：bool CEditorCore::drag(const CPosition &, const CPosition &, const CPosition &, bool)
 * 完成人：蒋捷
 * 函数功能：将选中区域的文本复制到目标位置，由开关控制拖放/复制
 * 参数：选中区域的开始截至的位置，目标位置，开关
 * 返回值：成功时返回true
 */
bool CEditorCore::drag(const CPosition & cposA, const CPosition & cposB, const CPosition & cposC, bool swi)
{
    if (copy(cposA, cposB))
    {
        if (swi == COPY_DRAG)
        {
            paste(cposC);
        }
        else if (swi == DEL_DRAG)
        {
            if (cposC < cposA) // 删除拖拽时应考虑目标与源的相对位置
            {
                remove(cposA, cposB);
                paste(cposC);
            }
            else
            {
                paste(cposC);
                remove(cposA, cposB);
            }
        }
        return true;
    }
    else
    {
        return false;
    }
}

/* 函数原型：int replaceAll(const string &, const string &)
 * 完成人：蒋捷
 * 函数功能：将全部文本中出现的模式串全部替换成要替换的目标串
 * 参数：模式串，替换目标串
 * 返回值：返回成功替换的个数
 */
int CEditorCore::replaceAll(const string & pattern, const string & replacement)
{
	list<string>::iterator itr = data.begin();
	int cnt = 0;
	CPosition cposA = CPosition(0, 0);
	pPos spos;
	for (;;)
	{
		spos = searchBM(cposA, pattern);
		cposA = spos.first;
		if (cposA.x == -1)
		{
			break;
		}
		replace(replacement, cposA, spos.second);
		cposA.x += replacement.length();
		cnt++;
	}
	return cnt;
}