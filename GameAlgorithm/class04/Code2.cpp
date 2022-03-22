
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
	//HeadNode는 언제나 존재합니다. Stack이 NULL 이더라도.
	PNODE TopNode;
	//TopNode 는 Stack의 데이터수가 0이라면 존재하지 않습니다.

	void del_top()
	{
		PNODE delnode;
		if (isEmpty())
		{
			throw new out_of_range("Stack  is NONE.");
		}

		delnode = TopNode;

		//TopNode->Prev = Head임은 Stack이 비어잇음을 의미합니다.
		if (TopNode->Prev == HeadNode)
			TopNode = nullptr;
		else
			this->TopNode = TopNode->Prev;

		delete delnode;
	}
	//Pop 에서 자료 반환+ Top 삭제가 이루어지는데,
	//자료 반환과 Top 삭제를 분리시켜둔 내부 메서드입니다.

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
	//	//모든 Data를 보여주는 메서드입니다. 자료구조랑 상관
	//	//없습니다.
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
























//가독성을 위한 Enum입니다.
enum {
	_STACK_NONE,
	_STACK_PUSH_SOUND,
	_STACK_IDLE,
	_STACK_POP_THIS
};


class BaseMenu {
	//모든 Menu들에 상속되는 BaseMenu입니다.
	//BaseMenu* 만으로 하위클래스에 접근 가능하도록
	//주요 메서드가 Virtual 처리 되어있습니다.
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
	//Input은 순수가상함수로 , Input 된 값을 InputSelect에서 받아오고,
	//Input 내부에서는 가져온 iSelect값에 대한 처리를 합니다.
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
	Menus.push(new MainMenu);			//SoundMenu는 따로 호출해야 하지만, MainMenu는 
	int CtrlValue = 0;					//시작시에 미리 호출

	while (Menus.isNotEmpty())
	{
		Menus.get_top()->Render();
		CtrlValue = Menus.get_top()->Input(); //무슨 Value를 Return 했는지 확인하여,
											  //Stack 바깥에서 Stack을 제어(내부에선 외부제어가 안되니까..)
		switch (CtrlValue)				//전체 프로그램 제어를 위해서는 ControlValue를 사용
		{
		case _STACK_PUSH_SOUND: 
		{
			Menus.push(new SoundMenu);	//SoundMenu로 진입시 SoundMenw를 새로 할당
			break;
		}
		case _STACK_NONE: 
		{
			break;
		}
		case _STACK_POP_THIS: 
		{
			SelectData = Menus.pop();
			delete SelectData;			//PoP This는 Sound이던, Main 이던 마지막 스택을
			SelectData = nullptr;		//삭제함으로써, Sound삭제시에는 Main으로, 
			break;						//Main에서 삭제시에는 종료 실행
			}
		}

	}

}