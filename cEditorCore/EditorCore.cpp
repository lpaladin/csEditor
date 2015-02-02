///////////////////////////////////////////////////////////
// EditorCore.cpp���ı��༭������DLL�Ĺ���ģ�� - �ı��༭
// ���ߣ�����
///////////////////////////////////////////////////////////

#define COPY_DRAG 0
#define DEL_DRAG 1
#include "Editor.h"

/* ����ԭ�ͣ�CEditorCore()
 * ����ˣ�����
 * �������ܣ�CEditorCore��Ĭ�Ϲ��캯������ɳ�ʼ�������Ĺ���
 * ��������
 * ����ֵ����
 */
CEditorCore::CEditorCore()
{
	data.push_back(string());	// ��������
    lineCnt = 1;				// ��������
    autoIndent = 0;				// �Զ������ر�
    autoWordWrap = 0;			// �Զ����йر�
    tabSpace = 4;				// tab�ո���Ĭ��4
    lineLength = -1;			// �������Զ�����
    lineWrapCnt = 0;			// ���к�����
}

/* ����ԭ�ͣ�bool insert(const CPosition &, char c)
 * ����ˣ�����
 * �������ܣ���ָ��λ�ò���ָ���ַ��������뻻�з������һ���µ�����������ˮƽ�Ʊ����ת��Ϊ��Ӧ��Ŀ�س�����
 * ����������λ�ã������ַ�
 * ����ֵ�����ɹ����뷵��true�����򷵻�false
 */
bool CEditorCore::insert(const CPosition & cpos, char c)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	if (line >= lineCnt) // �ж��Ƿ񳬹�������Χ
	{
		return false;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // �ж��Ƿ񳬹���ȷ�Χ
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

/* ����ԭ�ͣ�bool paste(const CPosition &)
 * ����ˣ�����
 * �������ܣ���ָ��λ�ò�����а�������
 * ����������λ��
 * ����ֵ�����ɹ����뷵��true�����򷵻�false
 */
CPosition CEditorCore::paste(const CPosition & cpos)
{
	return insert(cpos, clipboard);
}

/* ����ԭ�ͣ�CPosition insert(const CPosition &, const string &)
 * ����ˣ�����
 * �������ܣ���ָ��λ�ò�������ַ���������
 * ����������λ�ã��������ַ���
 * ����ֵ��������ɺ����λ�ã�������ʧ�ܷ���ԭλ��
 */
CPosition CEditorCore::insert(const CPosition & cpos, const string & s)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	CPosition cursorPos(pos, line);
	if (line >= lineCnt) // �ж��Ƿ񳬹�������Χ
	{
		return cursorPos;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // �ж��Ƿ񳬹���ȷ�Χ
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
			++lineCnt;	// ά������
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


/* ����ԭ�ͣ�bool backspace(const CPosition &)
 * ����ˣ�����
 * �������ܣ���ָ��λ��ɾ��һ���ַ�����ɾ��λ��λ����������������ϲ�
 * ������ɾ��λ��
 * ����ֵ�����ɹ�ɾ������true�����򷵻�false
 */
bool CEditorCore::backspace(const CPosition & cpos)
{
	list<string>::iterator  itr = data.begin();
	int line = cpos.y, pos = cpos.x;
	if (line >= lineCnt) // �ж��Ƿ񳬹�������Χ
	{
		return false;
	}
	for (int i = 0; i < line; ++i, ++itr);
	if (pos > itr->length()) // �ж��Ƿ񳬹���ȷ�Χ
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

/* ����ԭ�ͣ�bool removeLine(const int line, const int lineNum = 1)
 * ����ˣ�����
 * �������ܣ���ָ���п�ʼɾ�������У�Ĭ��Ϊһ�У���ֱ������ɾ��λ��
 * ������ɾ��λ��
 * ����ֵ�����ɹ�ɾ������true�����򷵻�false
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

/* ����ԭ�ͣ�string remove(const CPosition &, const CPosition &)
 * ����ˣ�����
 * �������ܣ�ɾ����ѡ����������������ݣ���������ɾ������
 * ������ɾ����ʼλ�ã�����λ��
 * ����ֵ�����ɹ�ɾ��������ɾ���ַ��������򷵻ؿմ�
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
	if (lineB >= lineCnt) // �ж��Ƿ񳬹�������Χ
	{
		return tmp;
	}
	for (i = 0, itrA = data.begin(); i < lineA; ++i, ++itrA);
	for (i = 0, itrB = data.begin(); i < lineB; ++i, ++itrB);
	if (posA > itrA->length() || posB > itrB->length()) // �ж��Ƿ񳬹���ȷ�Χ
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

/* ����ԭ�ͣ�string getSelection(const CPosition &, const CPosition &) const
 * ����ˣ�����
 * �������ܣ���ȡ��ѡ�����������������
 * ��������ʼλ�ã�����λ��
 * ����ֵ�������������ַ�������ʧ�ܷ��ؿմ�
 */
string CEditorCore::getSelection(const CPosition & cposA, const CPosition & cposB) const      // ���ѡ�е��ַ���
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
	if (lineB >= lineCnt) // �ж��Ƿ񳬹�������Χ
	{
		return tmp;
	}
	for (i = 0, itrA = data.begin(); i < lineA; ++i, ++itrA);
	for (i = 0, itrB = data.begin(); i < lineB; ++i, ++itrB);
	if (posA > itrA->length() || posB > itrB->length()) // �ж��Ƿ񳬹���ȷ�Χ
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

/* ����ԭ�ͣ�bool cut(const CPosition &, const CPosition &)
 * ����ˣ�����
 * �������ܣ�ɾ����ѡ����������������ݣ����������ƶ������а�
 * ��������ʼλ�ã�����λ��
 * ����ֵ�����ظò����Ƿ�ɹ�
 */
bool CEditorCore::cut(const CPosition & cposA, const CPosition & cposB)              // ���ж��䣬ָ����ʼ����λ��
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

/* ����ԭ�ͣ�bool replace(const string &, const CPosition &, const CPosition &)
 * ����ˣ�����
 * �������ܣ�����ѡ������ַ����滻�ɸ����ַ���
 * ������Ҫ�滻���ַ�������ʼλ�ã�����λ��
 * ����ֵ�������滻�Ƿ�ɹ�
 */
CPosition CEditorCore::replace(const string & s, const CPosition & cposA, const CPosition & cposB)
{
	remove(cposA, cposB);
	return insert(cposA, s);
}

/* ����ԭ�ͣ�void clear()
 * ����ˣ�����
 * �������ܣ����ı�ȫ����գ��������ز���
 * ��������
 * ����ֵ����
 */
void CEditorCore::clear()
{
    data.clear();
	data.push_back("");
    lineCnt = 1;
    lineWrapCnt = 0;
}

/* ����ԭ�ͣ�int getLineCnt()
 * ����ˣ�����
 * �������ܣ���ȡ���������Զ����п���ʱ���ؼ������е�������
 * ��������
 * ����ֵ�����ͣ�������
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

/* ����ԭ�ͣ�void getLineLength(int * lineSize)
 * ����ˣ�����
 * �������ܣ����ÿһ�еĳ���
 * ��������Ҫ�ṩ����д�������
 * ����ֵ����
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

/* ����ԭ�ͣ�void setWordWrap(int)
 * ����ˣ�����
 * �������ܣ������Զ����й��ܵĿ�����ر�
 * ������ÿһ�еĳ��ȣ�0����ر��Զ�����
 * ����ֵ����
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


/* ����ԭ�ͣ�int updateBuffer()
 * ����ˣ�����
 * �������ܣ�����������������Զ����п���ʱ�����Զ����й���
 * ��������
 * ����ֵ���Զ�����δ����ʱ���ص��е������
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

/* ����ԭ�ͣ�bool CEditorCore::copy(const CPosition &, const CPosition &)
 * ����ˣ�����
 * �������ܣ���ָ��������ַ������Ƶ����а�
 * ���������뿪ʼ������λ��
 * ����ֵ���ɹ�ʱ����true
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

/* ����ԭ�ͣ�bool CEditorCore::drag(const CPosition &, const CPosition &, const CPosition &, bool)
 * ����ˣ�����
 * �������ܣ���ѡ��������ı����Ƶ�Ŀ��λ�ã��ɿ��ؿ����Ϸ�/����
 * ������ѡ������Ŀ�ʼ������λ�ã�Ŀ��λ�ã�����
 * ����ֵ���ɹ�ʱ����true
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
            if (cposC < cposA) // ɾ����קʱӦ����Ŀ����Դ�����λ��
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

/* ����ԭ�ͣ�int replaceAll(const string &, const string &)
 * ����ˣ�����
 * �������ܣ���ȫ���ı��г��ֵ�ģʽ��ȫ���滻��Ҫ�滻��Ŀ�괮
 * ������ģʽ�����滻Ŀ�괮
 * ����ֵ�����سɹ��滻�ĸ���
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