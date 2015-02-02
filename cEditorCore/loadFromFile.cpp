////////////////////////////////////////////////////////////////////////
// loadFromFile.cpp：文本编辑器核心DLL的功能模块 - 文件读写、括号匹配
// 作者：邢耀鹏
////////////////////////////////////////////////////////////////////////

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
#include <vector>
#include <list>
#include <string>
#include <fstream>
#include "Editor.h"

using namespace std;

bool CEditorCore::loadFromFile(string a)
{
	fileName = a;
	data.clear();
	ifstream  infile;
	infile.open(a,ios::in);
	if(!infile)
	{
		//cout<<"can not open file"<<' '<<a<<','<<"please try again"<<endl;		
		return false;
	}
	/*string s;
	int lineCnt=0;
	while(getline(infile,s))
	{

		data.push_back(s);
		lineCnt++;
		if(lineCnt>=MAXLINE)
		{
			//cout<<"there are two mush lines in the file,you can not read all of the"<<endl;
			return false;
		}
	}*/
	lineCnt=0;
	while(1)
	{
		string s;
		char l;
	    while((l=infile.get())!=EOF&&l!='\n')
		{
           if(l=='	')
		   {
			   for(int i=0;i<4;i++)
			   {
				   s.push_back(' ');
			   }
		   }
	       else {s.push_back(l);}
		}
		data.push_back(s);
		lineCnt++;
		if (l == EOF)
			break;
	}
	infile.close();
	return true;

}
bool CEditorCore::saveToFile(string a)
{
	if (a != "")
		fileName = a;
	ofstream outfile;
	outfile.open(fileName,ios::out);
	if(!outfile)
	{
		//cout<<"can not write in the file<<' '<<a<<endl;
		return false;
	}
	list<string>::iterator p;
	for(p=data.begin();p!=data.end();p++)
	{
		outfile<<*p<<endl;
	}
	outfile.close();
	return true;
}

CPosition CEditorCore::match(CPosition a)
{
	int Lx=a.x;int Ly=a.y;// 记录位置
	int Dx=-1,Dy=-1;
	list<string>::iterator p=data.begin();
	int temp=Ly;
	while(temp--)
	{
		p++;
	}
	char sour=(*p)[Lx];
	char des;
	switch(sour)
	{
	case')':des='(';break;
	case'(':des=')';break;
	case'[':des=']';break;
	case']':des='[';break;
	case'{':des='}';break;
	case'}':des='{';break;
	}
	int n=1;
	if(sour=='('||sour=='['||sour=='{')
	{
		while(p!=data.end())
		{
			Lx++;
			while(Lx<(*p).length())
			{
				if((*p)[Lx]==des)
				{
					n--;
					if(n==0)
					{
					Dx=Lx;
					Dy=Ly;
					return CPosition(Dx,Dy);
					}
				}
				if((*p)[Lx]==sour)
				{
					n++;
				}
				Lx++;
			}
			p++;
			Ly++;
			if(p!=data.end())
			{
				Lx=-1;
			}
		}
		return CPosition(Dx,Dy);//注意，要判断是否为坐标是否为-1，-1
	}
	else if(sour==')'||sour==']'||sour=='}')
	{
		while(Ly>=0)
		{
			Lx--;
			while(Lx>=0)
			{
				if((*p)[Lx]==des)
				{
					n--;
					if(n==0)
					{
					Dx=Lx;
					Dy=Ly;
					return CPosition(Dx,Dy);
					}
				}
				if((*p)[Lx]==sour)
				{
					n++;
				}
				Lx--;
			}
			p--;
			Ly--;
			if(Ly>=0)
			{
				Lx=(*p).length();
			}
		}
		return CPosition(Dx,Dy);//注意，要判断是否为坐标是否为-1，-1
	}
	else
	{
		return CPosition(-2,-2);//传输有误；
		//cout<<"传输源有误"<<endl;
	}
}
