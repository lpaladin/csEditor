///////////////////////////////////////////////////////////
// searchBM.cpp��BM�㷨�ַ���ƥ�䣨��֧��ͨ�����
// ���ߣ�����
///////////////////////////////////////////////////////////
#include <iostream>
#include <list>
#include <string>
#include <vector>
#include "Editor.h"

using namespace std;

inline static int max(int a, int b)
{
	return a < b ? b : a;
}

/* ����ԭ�ͣ�pPos CEditorCore::searchBM(const CPosition &, const string &)
 * ����ˣ�����
 * �������ܣ����ı���Ѱ��ָ���ַ�������ʹ��ƥ���㷨ΪBM�㷨�����ַ�+�ú�׺��
 * ��������ʼ������λ�ã�Ҫ�������ַ���
 * ����ֵ�����ɹ������ҵ���һ���ַ����Ŀ�ʼ��β��ַ����ʧ�ܽ����صĵ�һ����ַ��������Ϊ-1
 */
pPos CEditorCore::searchBM(const CPosition & cpos, const string & p)
{
    int bc[128];						// ���ַ��������飬�ַ���ΪASCII
    int * gs = new int[p.length()];		// �ú�׺��������
    int * next = new int[p.length()];	// ����ú�׺ʱ���踨������
    int lp, lt, i, tmp, j, line;		// lpģʽ�����ȣ�ltĿ�괮���ȣ�line��λ��
    string t;

    memset(bc, 0, sizeof(bc));
    memset(gs, 0, sizeof(gs));
    memset(next, 0, sizeof(next));

    lp = p.length();
    // ���ַ�����Ԥ����
    for (i = 0; i < 128; ++i)
    {
        bc[i] = -1;
    }
    for (i = 0; i < lp; ++i)
    {
        bc[p[i]] = i;
    }

    // �ú�׺����Ԥ����
    // ����KMP����next���飺���ַ���p[i,lp - 1]�������ͬǰ��׺����׺Ϊnext[next[i], lp - 1]
    for (i = 0; i <= lp; ++i)
    {
        gs[i] = lp;
    }
    i = lp;
    j = lp + 1;
    next[i] = j;
    while (i > 0)
    {
        if (j == lp + 1 || p[i - 1] == p[j - 1])
        {
            next[--i] = --j;
        }
        else
        {
            j = next[j];
        }
    }
    for (j = 0; j < lp - 1; ++j)
    {
        gs[next[j]] = next[j];
    }

    // ƥ��
    list<string>::iterator itr = data.begin();
    line = cpos.y;
    for (i = 0; i < line; ++i, ++itr);
    i = cpos.x;

    while (itr != data.end())
    {
        t = *itr;
        lt = t.length();
        while (i <= lt - lp)
        {
            j = lp - 1;
            while (j > -1 && t[i + j] == p[j])		// �ҵ���ƥ���λ��
            {
                --j;
            }
            if (j == -1)							// ƥ��ɹ�
            {
                break;
            }
            else
            {
                tmp = max(j + 1 - gs[j + 1], j - bc[t[i + j]]);
                i += max(tmp, 1);
            }
        }
        if (i <= lt - lp)
        {
            return pPos(CPosition(i, line), CPosition(i + lp,line));
        }
        i = 0;
        ++itr;
        ++line;
    }
    return pPos(CPosition(-1, -1), cpos);
}