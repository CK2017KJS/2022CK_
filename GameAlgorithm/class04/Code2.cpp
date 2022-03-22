
#include <iostream>
/*
* 2022_03_22 
* GameAlgorithm Class 04 Assignment
* https://github.com/CK2017KJS
*/


using namespace std;

template<typename DATA>
struct _CStackNodes {
	DATA Data;
	_CStackNodes* Prev;
	_CStackNodes() {}
	_CStackNodes(DATA data): Data(data), Prev(nullptr) {}
};

template<typename DATA>
class MyStack 
{
private:

	typedef _CStackNodes<DATA> NODE;
	typedef _CStackNodes<DATA>* PNODE;
	//_CstackNodes<DATA> = Node, PNODE

	PNODE HeadNode;
	//HeadNode�� ������ �����մϴ�. Stack�� NULL �̴���.
	PNODE TopNode;
	//TopNode �� Stack�� �����ͼ��� 0�̶�� �������� �ʽ��ϴ�.

	void del_top()
	{
		PNODE delnode;
		if (isEmpty())
		{
			throw new out_of_range("Stack  is NONE.");
		}

		delnode = TopNode;

		//TopNode->Prev = Head���� Stack�� ��������� �ǹ��մϴ�.
		if (TopNode->Prev == HeadNode)
			TopNode = nullptr;
		else
			this->TopNode = TopNode->Prev;

		delete delnode;
	}
	//Pop ���� �ڷ� ��ȯ+ Top ������ �̷�����µ�,
	//�ڷ� ��ȯ�� Top ������ �и����ѵ� ���� �޼����Դϴ�.

public:
	MyStack() 
	{
		this->HeadNode = new NODE;
		this->TopNode = nullptr;
	}
	~MyStack() 
	{
		clear();
	}



	void push(DATA data)
	{
		PNODE newNode = new NODE(data);

		if (!TopNode) // TOPNode is NULL = Stack is NULL
			newNode->Prev = HeadNode;
		else 
			newNode->Prev = TopNode;
		TopNode = newNode;
	}
	DATA pop()
	{
		DATA retval;
		if (isEmpty()) 
		{
			throw out_of_range("Stack is Empty");
		}

		retval = get_top();
		this->del_top();
		return retval;
	}
	DATA get_top() 
	{
		DATA retval;
		if (isEmpty()) 
		{
			throw out_of_range("Stack is Empty");
		}

		retval = TopNode->Data;
		return retval;
	}

	void clear() 
	{
		while (isNotEmpty())
			this->del_top();
	}


	bool isEmpty() {return !TopNode;}
	bool isNotEmpty() { return TopNode; }
	//void Render() 
	//	//��� Data�� �����ִ� �޼����Դϴ�. �ڷᱸ���� ���
	//	//�����ϴ�.
	//{
	//	PNODE Node;
	//	if (isNotEmpty()) 
	//	{
	//		Node = TopNode;
	//		while (Node != HeadNode)
	//		{
	//			cout << Node->Data;
	//			Node = Node->Prev;
	//		}
	//	}
	//}
};
























//�������� ���� Enum�Դϴ�.
enum {
	_STACK_NONE,
	_STACK_PUSH_SOUND,
	_STACK_IDLE,
	_STACK_POP_THIS
};


class BaseMenu {
	//��� Menu�鿡 ��ӵǴ� BaseMenu�Դϴ�.
	//BaseMenu* ������ ����Ŭ������ ���� �����ϵ���
	//�ֿ� �޼��尡 Virtual ó�� �Ǿ��ֽ��ϴ�.
public:
	int iSelect;
	BaseMenu() {}
	virtual void Render() 
	{
		system("cls");
		cout << endl << endl << endl;
		cout << "====================" << endl;
	};
	virtual void InputSelect() 
	{
		cin >> iSelect;
	};
	virtual int Input() = 0;
	//Input�� ���������Լ��� , Input �� ���� InputSelect���� �޾ƿ���,
	//Input ���ο����� ������ iSelect���� ���� ó���� �մϴ�.
};

class MainMenu :public BaseMenu {
public:
	MainMenu() {}
	void Render()
	{
		BaseMenu::Render();
		cout << "1.Start Game " << endl;
		cout << "2.Save Game " << endl;
		cout << "3.Sound Setting" << endl;
		cout << "4.End Game" << endl;
		cout << "============" << endl;
	}
	int Input() {
		BaseMenu::InputSelect();
		switch (iSelect)
		{
		case 1:
			cout << "..Start Game!" << endl;
			system("pause");
			return _STACK_IDLE;
		case 2:
			cout << "..Save Game!" << endl;
			system("pause");
			return _STACK_IDLE;
		case 3:
			cout << "..Sound Setting!" << endl;
			system("pause");
			return _STACK_PUSH_SOUND;
			break;
		case 4:
			cout << "..End" << endl;
			system("pause");
			return _STACK_POP_THIS;
			break;
		}
		return _STACK_NONE;
	}
};
class SoundMenu :public BaseMenu {
public:
	SoundMenu() {}
	void Render()
	{
		BaseMenu::Render();
		cout << "1.Sound On" << endl;
		cout << "2.Sound Off" << endl;
		cout << "3.Backto Main" << endl;
		cout << "============" << endl;
	}
	int Input() 
	{
		BaseMenu::InputSelect();
		switch (iSelect)
		{
		case 1:
			cout << "Sound On" << endl;
			system("pause");
			return _STACK_IDLE;
			break;
		case 2:
			cout << "Sound Off" << endl;
			system("pause");
			return _STACK_IDLE;
			break;
		case 3:
			cout << "Back To Main." << endl;
			system("pause");
			return _STACK_POP_THIS;
			break;
		}
		return iSelect;
	}
};



int main() 
{
	MyStack<BaseMenu*> Menus;
	BaseMenu* SelectData = NULL;
	Menus.push(new MainMenu);			//SoundMenu�� ���� ȣ���ؾ� ������, MainMenu�� 
	int CtrlValue = 0;					//���۽ÿ� �̸� ȣ��

	while (Menus.isNotEmpty())
	{
		Menus.get_top()->Render();
		CtrlValue = Menus.get_top()->Input(); //���� Value�� Return �ߴ��� Ȯ���Ͽ�,
											  //Stack �ٱ����� Stack�� ����(���ο��� �ܺ���� �ȵǴϱ�..)
		switch (CtrlValue)				//��ü ���α׷� ��� ���ؼ��� ControlValue�� ���
		{
		case _STACK_PUSH_SOUND: 
		{
			Menus.push(new SoundMenu);	//SoundMenu�� ���Խ� SoundMenw�� ���� �Ҵ�
			break;
		}
		case _STACK_NONE: 
		{
			break;
		}
		case _STACK_POP_THIS: 
		{
			SelectData = Menus.pop();
			delete SelectData;			//PoP This�� Sound�̴�, Main �̴� ������ ������
			SelectData = nullptr;		//���������ν�, Sound�����ÿ��� Main����, 
			break;						//Main���� �����ÿ��� ���� ����
			}
		}

	}

}