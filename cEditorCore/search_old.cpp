/////////////////////////////////////////////////////////
// search.cpp：文本编辑器核心DLL的功能模块 - 字符串匹配
// 作者：赵万荣
/////////////////////////////////////////////////////////

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

#include <iostream>
#include <list>
#include <string>
#include <vector>
#include "Editor.h"

using namespace std;


/*********************************************************
本函数由赵万荣完成
完成了依托KMP的字符串匹配
包括有*（匹配任意数量的字符）和？（匹配任意一个字符）
其中*会优先进行最靠前的匹配，
若起始位置一样，优先进行最短匹配，
*匹配的字符串长度不超过100
*********************************************************/


SNodeString::SNodeString (string _s)
{
	s = _s;
	len = s.size();
			
	// 预处理每个string, 记录next数组
	next = new int[len];
	next[0] = 0;
	int LastPos, i =1;
	for (; i < len; i++)
	{
		LastPos = next[i -1];
		while(LastPos != -1 && s[LastPos] != s[i - 1])
			LastPos = next[LastPos];
		next[i] = next[LastPos] + 1;
		if (s[i] == s[next[i]])
			next[i] = next[next[i]]; 
	}
}
pPos SNodeString::KMP(CPosition beg)
{
	// 读取从起始位置开始的全部文档信息
	list<string>::iterator cur = core->data.begin();
	for(int i = 0; i < beg.x; i++)
		cur ++; 
		
	//KMP主体
	int i =0;
	while (i < len)
	{
		if (i==-1 ||s[i] == (*cur)[beg.y] || s[i] == '?') 
		{
			i++; 
			beg.y++; 
			if ((*cur)[beg.y] == -1)
			{
				cur++;
				beg.x++;
				if (cur == core->data.end())
				{
					break;
				}
				beg.y = 0;
			}
		}
		else  
			i = next[i];
	}
	if( i >= len) 	
	{
		beg.y -= len;
		if (beg.y < 0)
		{
			beg.x--;
			beg.y += (*(cur--)).length();
		}
		CPosition end(beg.x, beg.y);

		// 寻找结束位置
		int i = len;
		while(i != 0)
		{
			if ((*cur)[end.y] == -1)
			{
				cur++;
				end.x++;
					
				end.y = 0;
			}
			else
				end.y++;
			i--;
		}
		return pPos(beg, end);
	}
	return pPos(CPosition(-1, -1), CPosition(-1, -1));
}

pPos Ckmp::rotate(vector<SNodeString>::iterator i, CPosition beg)
{
	pPos temp = (*i).KMP(beg), res;
	if (temp.first.x == -1)
		return pPos(CPosition(-1, -1), CPosition(-1, -1));
	beg = temp.second;
	res.first = temp.first; 
	i++;
	if (i != MatchStr.end())
		temp = rotate(i, beg);
	while (temp.first.x == -1)
	{
		i--;
		temp = (*i).KMP(beg);
		if (temp.first.x == -1)
			return pPos(CPosition(-1, -1), CPosition(-1, -1));
		temp = rotate(i, beg);
		beg = temp.second;
		i++;
	}
	res.second = temp.second;
	return res;
}
pPos Ckmp::find(const string &_match, CPosition beg)
{
	string match = _match;
	while(!match.empty())
	{
		int len = match.length();
		int i = 0;
		while (match[i] != '*' && i < len)
			i++;
		MatchStr.push_back(match.substr(0, i));
		match = match.substr(i);
	}
	
	// 回溯分段KMP
	vector <SNodeString>::iterator i = MatchStr.begin();
	return rotate(i, beg);
}