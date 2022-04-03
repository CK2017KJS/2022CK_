/*
GameObjectPool

- STL ��� ���� ���� ���� �޸� �����Ͽ� ���



	\O/
	 |
	 ))

*/



#include<iostream>		//�⺻ Input/OutPut Stream

using namespace std;




class GameObject
{

public:
	static int CountGenGameObject;	//��ü ������ Object�� ����
	int mIndex;						//�� ��°�� ������ Object������ ���� (��ȯ,���� Ȯ�ο�)
	GameObject() { mIndex = CountGenGameObject++; }
};


class Tracker
{
public:
	bool count;					//GameObjectPool �� ���� ������ "��" ����
	bool data;					//GameObjectPool ���ο� ���� ������ ����
	bool origindata;			//GameObjectPool �Ҵ�� ��ü ������ �� ����.
	bool addcell;				//Cell�� �߰��� �� ���
								//OUTSIDE�� ���� �ֱ� OUTSIDE ��º��� ����մϴ�.
	bool push;					//PUSH�� DATA �� �����մϴ�.
	bool pop;					//POP �� DATA�� �����մϴ�.

	Tracker()
	{
		count = false;
		data = false;
		origindata = false;
		push = false;
		pop = false;
		addcell = false;
	}
};


template<typename DATA>
class ObjectPool
{
	DATA*** mObjectPool;		//��ȯ/�Ҵ��� �ݺ��ϰ� �� �޸�
	DATA*** mObjectPoolOrigin;	//���� ������ Object ��ü��

	int Count;					//���� ���� Cell ����(DATA**)
	int Size;					//Pool���� �ϳ��� ���� ������ ������Ʈ �� .
	int Index;					//���������� Load�� Index ��ȣ.

	bool IsEmpty() { return Index == Size * Count; }
	// ��ü mObjectPool���� �˻�

	void AddCell()
	{
		RenderAddCell();
		DATA*** TempData = mObjectPool;
		DATA*** TempDataOrigin = mObjectPoolOrigin;

		mObjectPool = new DATA **[Count + 1];
		mObjectPoolOrigin = new DATA **[Count + 1];

		for (int i = 0; i < Count; i++)
		{
			mObjectPool[i] = TempData[i];
			mObjectPoolOrigin[i] = TempDataOrigin[i];

		}



		mObjectPool[Count] = new DATA *[Size];
		mObjectPoolOrigin[Count] = new DATA *[Size];
		for (int j = 0; j < Size; j++)
		{
			mObjectPool[Count][j] = new DATA;
			mObjectPoolOrigin[Count][j] = mObjectPool[Count][j];
		}

		Count++;							//CellCount ����


	}


	//Config ���� ���� ������ ����ϴ� �Լ�.

	void RenderPop(DATA** data)
	{
		if (Config.pop)
		{
			printf("	POP DATA : {");
			for (int i = 0; i < Size; i++)
			{
				printf("%d", data[i % Size]->mIndex);
				if (i < Size - 1)
					printf(",");
			}
			printf("}\n");
		}
	}
	void RenderPop(DATA* data)
	{
		if (Config.pop)
		{
			printf("	POP DATA: %d\n", data->mIndex);
		}
	}
	void RenderPush(DATA** data)
	{
		if (Config.push)
		{
			printf("	PUSH DATA : {");
			for (int i = 0; i < Size; i++)
			{
				printf("%d", data[i % Size]->mIndex);
				if (i < Size - 1)
					printf(",");
			}
			printf("}\n");
		}
	}
	void RenderPush(DATA* data)
	{
		if (Config.push)
		{
			printf("	PUSH DATA: %d\n", data->mIndex);
		}
	}
	void RenderAddCell() {
		if (Config.addcell)
			cout << "�˸� ! - cell�� �߰��Ǿ����ϴ�. ���� cell����: " << Count << endl;
	}



public:


	Tracker Config;
	//���� �� �������� � ������ ����� �� ������ �� �ֽ��ϴ�.



	ObjectPool(int CellSize)
	{

		Size = CellSize;
		Count = 0;
		Index = 0;

		mObjectPool = new DATA **[Count];
		mObjectPoolOrigin = new DATA **[Count];


	}
	~ObjectPool()
	{
		if (GameObject::CountGenGameObject != Count * Size)
			cout << "\n\n ���. ��ȯ���� ���� Object�� �ֽ��ϴ�.\n\n";
		else
			cout << "\n\n  ��� Object�� ��ȯ�Ǿ����ϴ�.\n\n";
		for (int i = 0; i < Count; i++)
		{
			for (int j = 0; j < Size; j++) {
				delete mObjectPoolOrigin[i][j];
			}
			//���� ������ ������ �Ҹ��ڿ��� ó���Ѵ�.
			//Pop�� �޴°��� Data�� �������� ��.
		}
		for (int i = 0; i < Count; i++)
		{
			delete[] mObjectPoolOrigin[i];
			delete[] mObjectPool[i];
		}

		delete[] mObjectPoolOrigin;
		delete[] mObjectPool;
	}

	/*
	* DATA�� Satck ������ �������, ������ mObjectPoolOrigin ���� �ϰ������� �����ϹǷ�,
	* mObjectPool ������ �Ҵ�� �ݳ��� �����մϴ�. ���� DATA**(���� �迭 ������) ���� ������
	* 3�� �����͸� ���Ͽ� ��ü�� ���� �������� �ʰ� �ּҰ��� �ٲܼ� �ֵ��� �մϴ�.
	
	
	*/

	DATA* pop()						//���� Object�� �Ҵ�.
	{
		if (IsEmpty())				//���� ����ִٸ� �߰� ����
			AddCell();
		DATA* RetVal =
			mObjectPool[Index / Size][Index % Size];

		RenderPop(RetVal);

		Index++;
		return RetVal;
	}
	DATA** pop_cell()				//Object �� ��ü�� �Ҵ�.
	{
		AddCell();					//���� ��°�� �Ҵ��ϹǷ� ���� ���� �� �ϳ� �߰� ����
		DATA** RetVal = mObjectPool[Index / Size];
		RenderPop(RetVal);
		Index += Size;
		return RetVal;
	}
	void push(DATA* Data)			//Object�� ��ȯ
	{
		Index--;
		RenderPush(Data);
		mObjectPool[Index / Size][Index % Size] = Data;

	};
	void push(DATA** Data)			//Object�� ��ȯ
	{
		RenderPush(Data);
		for (int i = 0; i < Size; i++) {
			Index--;
			mObjectPool[Index / Size][Index % Size] = Data[i];
		}

	};
	void RenderIsPoolStat()
	{
		int RenderCount = Size * Count;

		if (Config.count)
		{
			printf("\nPool�� ���� Object ����. : %d ��. \n", RenderCount - Index);
		}

		if (Config.data) {
			if (Config.origindata)
				printf("�Ҵ�� ��� Object ����: %d ��, ", GameObject::CountGenGameObject);
			if (Config.count)
				printf("���� : {");
			else
				printf("\nPool�� ���� Object ����: {");
			for (int i = Index; i < RenderCount; i++)
			{
				printf("%d", mObjectPool[i / Size][i % Size]->mIndex);
				if (i < RenderCount - 1)
					printf(",");
			}
			printf("}\n");
		}
	}

};
int GameObject::CountGenGameObject = 0;
int main()
{
	ObjectPool<GameObject> Pool(5);


	Pool.Config.pop = true;			//POP �ÿ� �� POP�ߴ��� ���
	Pool.Config.data = true;		//���� DATA ���� ���
	Pool.Config.push = true;		//PUSH�ÿ� �� PUSH �޴��� ���
	Pool.Config.addcell = true;		//Cell�� �߰��� �� ���
	Pool.Config.count = true;
	Pool.Config.origindata = true;


	GameObject* ObjPtr1[3];
	GameObject* ObjPtr2[5];
	GameObject* ObjPtr3[20];
	GameObject** CellPtr1, **CellPtr2;


	cout << "\n\n-POP DATA1 - \n\n";
	for (int i = 0; i < 3; i++) {
		ObjPtr1[i] = Pool.pop();
	}
	Pool.RenderIsPoolStat();


	cout << "\n\n-POP DATA2 - \n\n";
	for (int i = 0; i < 5; i++) {
		ObjPtr2[i] = Pool.pop();
	}
	Pool.RenderIsPoolStat();


	cout << "\n\n-POP DATA3 - \n\n";
	CellPtr1 = Pool.pop_cell();
	CellPtr2 = Pool.pop_cell();
	Pool.RenderIsPoolStat();


	cout << "\n\n-PUSH DATA1 - \n\n";
	for (int i = 0; i < 5; i++) {
		Pool.push(ObjPtr2[i]);
	}
	Pool.RenderIsPoolStat();


	cout << "\n\n-PUSH DATA2 - \n\n";
	for (int i = 0; i < 3; i++) {
		Pool.push(ObjPtr1[i]);
	}
	Pool.RenderIsPoolStat();


	cout << "\n\n-PUSH DATA3 - \n\n";
	for (int i = 0; i < 5; i++) {
		Pool.push(CellPtr1[i]);
	}
	Pool.push(CellPtr2);
	Pool.RenderIsPoolStat();


	cout << "\n\n-POP DATA1 - \n\n";
	for (int i = 0; i < 3; i++) {
		ObjPtr1[i] = Pool.pop();
	}
	Pool.RenderIsPoolStat();

	cout << "\n\n-POP DATA2 - \n\n";
	for (int i = 0; i < 17; i++) {
		ObjPtr3[i] = Pool.pop();
	}

	cout << "\n\n-PUSH DATA1 - \n\n";
	for (int i = 0; i < 3; i++) {
		Pool.push(ObjPtr1[i]);
	}
	Pool.RenderIsPoolStat();

	cout << "\n\n-PUSH DATA2 - \n\n";
	for (int i = 0; i < 17; i++) {
		Pool.push(ObjPtr3[i]);

	}
	Pool.RenderIsPoolStat();
}