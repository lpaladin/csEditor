////////////////////////////////////////////////////////////////////////
// loadFromFile.cpp���ı��༭������DLL�Ĺ���ģ�� - �ļ���д������ƥ��
// ���ߣ���ҫ��
////////////////////////////////////////////////////////////////////////

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
	int Lx=a.x;int Ly=a.y;// ��¼λ��
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
		return CPosition(Dx,Dy);//ע�⣬Ҫ�ж��Ƿ�Ϊ�����Ƿ�Ϊ-1��-1
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
		return CPosition(Dx,Dy);//ע�⣬Ҫ�ж��Ƿ�Ϊ�����Ƿ�Ϊ-1��-1
	}
	else
	{
		return CPosition(-2,-2);//��������
		//cout<<"����Դ����"<<endl;
	}
}
