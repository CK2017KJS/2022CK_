
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