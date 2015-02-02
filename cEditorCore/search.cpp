/////////////////////////////////////////////////////////
// search.cpp���ı��༭������DLL�Ĺ���ģ�� - �ַ���ƥ��
// ���ߣ�������
/////////////////////////////////////////////////////////

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

#include <iostream>
#include <list>
#include <string>
#include <vector>
#include "Editor.h"

using namespace std;


/*********************************************************
�����������������
���������KMP���ַ���ƥ��
������*��ƥ�������������ַ����ͣ���ƥ������һ���ַ���
����*�����Ƚ����ǰ��ƥ�䣬
����ʼλ��һ�������Ƚ������ƥ�䣬
*ƥ����ַ������Ȳ�����100
*********************************************************/


SNodeString::SNodeString (string _s)
{
	s = _s;
	len = s.size();
			
	// Ԥ����ÿ��string, ��¼next����
	next = new int[len];
	next[0] = 0;
	/*int LastPos, i =1;
	for (; i < len; i++)
	{
		LastPos = next[i -1];
		while(LastPos != -1 && s[LastPos] != s[i - 1])
			LastPos = next[LastPos];
		next[i] = next[LastPos] + 1;
		if (s[i] == s[next[i]])
			next[i] = next[next[i]]; 
	}*/
	int i = 0, j = -1;
	next[i] = j;
	while (i < len)
	{
		if (j == -1 || s[i] == s[j])
		{
			next[++i] = ++j;
			if (s[i] == s[j])
				next[i] = next[j];
		}
		else
		{
			j = next[j];
		}
	}
}
pPos SNodeString::KMP(CPosition beg)
{
	// ��ȡ����ʼλ�ÿ�ʼ��ȫ���ĵ���Ϣ
	list<string>::iterator cur = core->data.begin();
	for(int i = 0; i < beg.y; i++)
		cur ++; 
		
	//KMP����
	int i =0;
	while (i < len)
	{
		if (i==-1 ||s[i] == (*cur)[beg.x] || s[i] == '?') 
		{
			i++; 
			beg.x++; 
			if (beg.x >= cur->size())
			{
				cur++;
				beg.y++;
				if (cur == core->data.end())
				{
					break;
				}
				beg.x = 0;
			}
		}
		else  
			i = next[i];
	}
	if( i >= len) 	
	{
		beg.x -= len;
		if (beg.x < 0)
		{
			beg.y--;
			beg.x += (*(cur--)).length();
		}
		CPosition end(beg.y, beg.x);

		// Ѱ�ҽ���λ��
		int i = len;
		while(i != 0)
		{
			if ((*cur)[end.x] == -1)
			{
				cur++;
				end.y++;
					
				end.x = 0;
			}
			else
				end.x++;
			i--;
		}
		return pPos(beg, end);
	}
	return pPos(CPosition(-1, -1), CPosition(-1, -1));
}

pPos Ckmp::rotate(vector<SNodeString>::iterator i, CPosition beg)
{
	pPos temp = (*i).KMP(beg), res;
	if (temp.first.y == -1)
		return pPos(CPosition(-1, -1), CPosition(-1, -1));
	beg = temp.second;
	res.first = temp.first; 
	i++;
	if (i != MatchStr.end())
		temp = rotate(i, beg);
	while (temp.first.y == -1)
	{
		i--;
		temp = (*i).KMP(beg);
		if (temp.first.y == -1)
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
		MatchStr.push_back(match.substr(0, i) );
		if (i == len)
			break;
	}
	
	// ���ݷֶ�KMP
	vector <SNodeString>::iterator i = MatchStr.begin();
	return rotate(i, beg);
}