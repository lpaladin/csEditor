///////////////////////////////////////////////////////////
// searchBM.cpp：BM算法字符串匹配（不支持通配符）
// 作者：蒋捷
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

/* 函数原型：pPos CEditorCore::searchBM(const CPosition &, const string &)
 * 完成人：蒋捷
 * 函数功能：在文本中寻找指定字符串，所使用匹配算法为BM算法（坏字符+好后缀）
 * 参数：开始搜索的位置，要搜索的字符串
 * 返回值：若成功返回找到第一个字符串的开始结尾地址，若失败将返回的第一个地址横坐标置为-1
 */
pPos CEditorCore::searchBM(const CPosition & cpos, const string & p)
{
    int bc[128];						// 坏字符辅助数组，字符集为ASCII
    int * gs = new int[p.length()];		// 好后缀辅助数组
    int * next = new int[p.length()];	// 计算好后缀时所需辅助数组
    int lp, lt, i, tmp, j, line;		// lp模式串长度，lt目标串长度，line行位置
    string t;

    memset(bc, 0, sizeof(bc));
    memset(gs, 0, sizeof(gs));
    memset(next, 0, sizeof(next));

    lp = p.length();
    // 坏字符数组预处理
    for (i = 0; i < 128; ++i)
    {
        bc[i] = -1;
    }
    for (i = 0; i < lp; ++i)
    {
        bc[p[i]] = i;
    }

    // 好后缀数组预处理
    // 类似KMP定义next数组：在字符串p[i,lp - 1]中最长的相同前后缀，后缀为next[next[i], lp - 1]
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

    // 匹配
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
            while (j > -1 && t[i + j] == p[j])		// 找到不匹配的位置
            {
                --j;
            }
            if (j == -1)							// 匹配成功
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