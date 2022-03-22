
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



int main() 
{
	int DivNum = 15;
	MyStack<int> StackDiv;
	while (DivNum >= 1)
	{
		StackDiv.push(DivNum % 2);
		DivNum = DivNum / 2;
	}

	//StackDiv.Render();
	while (StackDiv.isNotEmpty()) 
	{
		cout << StackDiv.pop();
	}

}