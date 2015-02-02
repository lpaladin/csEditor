#pragma once
//////////////////////////////////////////////
// Editor.h���ı��༭������DLL��ͷ�ļ�
// ���ߣ����ݡ��������������١������
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

#define DLLEXPORTHEADER extern "C" __declspec(dllexport)

#include <string>
#include <list>
#include <vector>
using namespace std;

struct CPosition  // ��ʾ�ı�������Ľṹ�壬y��ʾ�У�x��ʾ�ַ����ַ�����λ��
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

typedef pair<CPosition, CPosition> pPos; // �����ַ���ƥ�������ķ���ֵ

class CEditorCore
{
private:
    list<string> data;  // �洢����
    string clipboard;   // ���ü��а�
    string fileName;    // ���ļ�ʱ���ļ���
    int lineCnt;        // ������
	int tabSpace;		// ����tab�Ŀո���
	int lineLength;		// �Զ�����ʱÿһ�е���󳤶�
	int lineWrapCnt;	// �Զ�����ʱ������
    bool autoIndent;    // �Ƿ��Զ��������
    bool autoWordWrap;  // �Ƿ��Զ����б��

public:
	int* lineMap;							// �Զ�����ÿ�е�ӳ��
    wchar_t** outputBuffer;					// ���������
    //vector<int> outputFlag[MAXLINE];     // �����ǣ�������ɫ

    CEditorCore();							// Ĭ�Ϲ��캯��
    bool loadFromFile(string);				// ��ָ���ļ���ȡ�ı�
    bool saveToFile(string);				// д�뵽ָ���ļ�
    bool insert(const CPosition &, char);   // ��ָ��λ�ò���һ���ַ�
    CPosition paste(const CPosition & cpos);				// ��ָ��λ�ò�����а��е�����
    CPosition insert(const CPosition &, const string &);	// ����һ���ַ���
    bool backspace(const CPosition &);						// ɾ��һ���ַ�
    bool removeLine(const int line, const int lineNum = 1); // ɾ��ָ���У�Ĭ��һ��
    string remove(const CPosition &, const CPosition &);    // ɾ�����䣬ָ����ʼ����Ϊֹ
    bool cut(const CPosition &, const CPosition &);         // ���ж��䣬ָ����ʼ����λ��
	string getSelection(const CPosition &, const CPosition &) const;				// ���ѡ�е��ַ���
    CPosition replace(const string &, const CPosition &, const CPosition &);		// �滻ָ��λ�õ��ַ���
    CPosition match(CPosition);								// ƥ�����ţ�ע�ͣ�etc.
	bool copy(const CPosition &, const CPosition &);		// ��ָ��������ַ������Ƶ����а�
	bool drag(const CPosition &, const CPosition &, const CPosition &, bool);		// ��ѡ��������ı����Ƶ�Ŀ��λ�ã��ɿ��ؿ����Ϸ�/����
	void clear();											// ����ı��������ã��������buffer��clipboard
	void getLineLength(int *);								// ���ÿ�г���
	void setWordWrap(int length);							// �����Զ����У�����Ϊÿһ�еĳ��ȣ�0����ر��Զ�����
	int getLineCnt();										// ������������������У�
    void updateBuffer(int *);								// �������������
	int replaceAll(const string &, const string &);			// ��ȫ���ı��е�ƥ���ַ����滻
	pPos searchBM(const CPosition &, const string &);		// BM�㷨�����ַ���ƥ��

	friend class SNodeString;
};

// Ϊ����ɴ�*��ģʽƥ�䣬Ҫ��ԭ���Ĵ��ֳɶಿ�֣�д��һ�����Է���洢�ͷֿ�����
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
���������������
���������KMP���ַ���ƥ��
������*��ƥ�������������ַ����ͣ���ƥ������һ���ַ���
����*�����Ƚ����ǰ��ƥ�䣬
����ʼλ��һ�������Ƚ������ƥ�䣬
*ƥ����ַ������Ȳ�����100
*********************************************************/
class Ckmp
{
	string match;
	vector <SNodeString> MatchStr; // ƥ���ַ�������*�ֿ���Ŀ������
	pPos ans;
public:
	
	 //Ϊ����ɺ���*���ַ���ƥ����������һ���ݹ麯��
	pPos rotate(vector<SNodeString>::iterator i, CPosition beg);
	pPos find(const string &_match, CPosition beg = CPosition(0,0));
};