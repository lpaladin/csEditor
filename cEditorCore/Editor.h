#pragma once
//////////////////////////////////////////////
// Editor.h：文本编辑器核心DLL的头文件
// 作者：蒋捷、邢曜鹏、赵万荣、周昊宇
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

#define DLLEXPORTHEADER extern "C" __declspec(dllexport)

#include <string>
#include <list>
#include <vector>
using namespace std;

struct CPosition  // 表示文本中坐标的结构体，y表示行，x表示字符中字符串的位置
{
    int x;
    int y;

	CPosition(int x, int y)
	{
		CPosition::x = x;
		CPosition::y = y;
	}

	CPosition(){}

	bool operator<(const CPosition & b) const
	{
		if (y != b.y)
		{
			return y < b.y;
		}
		else
		{
			return x < b.x;
		} 
	}

	bool operator>(const CPosition & b) const
	{
		if (y != b.y)
		{
			return y > b.y;
		}
		else
		{
			return x > b.x;
		}
	}  
};

typedef pair<CPosition, CPosition> pPos; // 用于字符串匹配行数的返回值

class CEditorCore
{
private:
    list<string> data;  // 存储数据
    string clipboard;   // 内置剪切板
    string fileName;    // 打开文件时的文件名
    int lineCnt;        // 总行数
	int tabSpace;		// 控制tab的空格数
	int lineLength;		// 自动换行时每一行的最大长度
	int lineWrapCnt;	// 自动换行时总行数
    bool autoIndent;    // 是否自动缩进标记
    bool autoWordWrap;  // 是否自动换行标记

public:
	int* lineMap;							// 自动换行每行的映射
    wchar_t** outputBuffer;					// 输出缓冲区
    //vector<int> outputFlag[MAXLINE];     // 输出标记，控制颜色

    CEditorCore();							// 默认构造函数
    bool loadFromFile(string);				// 从指定文件读取文本
    bool saveToFile(string);				// 写入到指定文件
    bool insert(const CPosition &, char);   // 在指定位置插入一个字符
    CPosition paste(const CPosition & cpos);				// 在指定位置插入剪切板中的内容
    CPosition insert(const CPosition &, const string &);	// 插入一个字符串
    bool backspace(const CPosition &);						// 删除一个字符
    bool removeLine(const int line, const int lineNum = 1); // 删除指定行，默认一行
    string remove(const CPosition &, const CPosition &);    // 删除段落，指定开始结束为止
    bool cut(const CPosition &, const CPosition &);         // 剪切段落，指定开始结束位置
	string getSelection(const CPosition &, const CPosition &) const;				// 获得选中的字符串
    CPosition replace(const string &, const CPosition &, const CPosition &);		// 替换指定位置的字符串
    CPosition match(CPosition);								// 匹配括号，注释，etc.
	bool copy(const CPosition &, const CPosition &);		// 将指定区域的字符串复制到剪切板
	bool drag(const CPosition &, const CPosition &, const CPosition &, bool);		// 将选中区域的文本复制到目标位置，由开关控制拖放/复制
	void clear();											// 清空文本（即重置），不清空buffer和clipboard
	void getLineLength(int *);								// 获得每行长度
	void setWordWrap(int length);							// 设置自动换行，参数为每一行的长度，0代表关闭自动换行
	int getLineCnt();										// 获得总行数（包括折行）
    void updateBuffer(int *);								// 更新输出缓冲区
	int replaceAll(const string &, const string &);			// 将全部文本中的匹配字符串替换
	pPos searchBM(const CPosition &, const string &);		// BM算法进行字符串匹配

	friend class SNodeString;
};

// 为了完成带*的模式匹配，要将原来的串分成多部分，写了一个类以方便存储和分开计算
class SNodeString
{
	string s;
	int len;
	int* next;
public:
	static CEditorCore* core;
	SNodeString (string _s);
	pPos KMP(CPosition beg);
	friend class Ckmp;
};



/*********************************************************
本类由赵万荣完成
完成了依托KMP的字符串匹配
包括有*（匹配任意数量的字符）和？（匹配任意一个字符）
其中*会优先进行最靠前的匹配，
若起始位置一样，优先进行最短匹配，
*匹配的字符串长度不超过100
*********************************************************/
class Ckmp
{
	string match;
	vector <SNodeString> MatchStr; // 匹配字符串依托*分开的目标数组
	pPos ans;
public:
	
	 //为了完成含有*的字符串匹配做出来的一个递归函数
	pPos rotate(vector<SNodeString>::iterator i, CPosition beg);
	pPos find(const string &_match, CPosition beg = CPosition(0,0));
};