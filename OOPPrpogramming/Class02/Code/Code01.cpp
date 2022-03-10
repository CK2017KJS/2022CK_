
//한글 인코딩 깨짐 방지


#include <iostream>
using namespace std;

class CArrayProgram 
{
	int m_iX, m_iY;
	char** m_Array;
private:
	void RenderLine(int Line) 
	{
		for (int i = 0; i < m_iX; i++)
			cout << m_Array[Line - 1][i];
	}
public:
	void Init() 
	{
		cout << "Enter Buffer Width : ";
		cin >> m_iX;
		cout << "Enter Buffer Height: ";
		cin >> m_iY;

		m_Array = new char*[m_iX];

		for (int i = 0; i < m_iY; i++) 
		{
			m_Array[i] = new char[m_iX];
			for (int j = 0; m_iX; j++)
				cout << m_Array[i][j];
		}

	}

	void Run() 
	{
		int Input;
		while (true) 
		{
			cout << "\n Enter PrintLine :";
			cin >> Input;

			if (!Input) 
			{

			}


			else 
			{
				if (Input < m_iX)
				{
					this->RenderLine(Input);
				}

				else 
				{
					cout << "Index Over Error" << endl;
				}


			}
		}
	}

	~CArrayProgram()//2차원 배열 초기화
	{
		for (int i = 0; i < m_iY; i++)
			delete m_Array[i];
		delete m_Array;
	}
};