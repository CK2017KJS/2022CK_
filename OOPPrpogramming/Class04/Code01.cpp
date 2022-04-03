/*
GameObjectPool

- STL 사용 없이 직접 동적 메모리 구현하여 사용



	\O/
	 |
	 ))

*/



#include<iostream>		//기본 Input/OutPut Stream

using namespace std;




class GameObject
{

public:
	static int CountGenGameObject;	//전체 생성된 Object의 갯수
	int mIndex;						//몇 번째로 생성된 Object인지의 정보 (반환,삭제 확인용)
	GameObject() { mIndex = CountGenGameObject++; }
};


class Tracker
{
public:
	bool count;					//GameObjectPool 의 남은 데이터 "수" 추적
	bool data;					//GameObjectPool 내부에 남은 데이터 추적
	bool origindata;			//GameObjectPool 할당된 전체 데이터 수 추적.
	bool addcell;				//Cell이 추가될 때 출력
								//OUTSIDE는 가장 최근 OUTSIDE 출력부터 계산합니다.
	bool push;					//PUSH된 DATA 를 추적합니다.
	bool pop;					//POP 된 DATA를 추적합니다.

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
	DATA*** mObjectPool;		//반환/할당을 반복하게 될 메모리
	DATA*** mObjectPoolOrigin;	//실제 생성된 Object 객체들

	int Count;					//현재 가진 Cell 개수(DATA**)
	int Size;					//Pool에서 하나의 셀당 가지는 오브젝트 수 .
	int Index;					//마지막으로 Load된 Index 번호.

	bool IsEmpty() { return Index == Size * Count; }
	// 전체 mObjectPool에서 검사

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

		Count++;							//CellCount 증가


	}


	//Config 값에 따라 정보를 출력하는 함수.

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
			cout << "알림 ! - cell이 추가되었습니다. 현재 cell개수: " << Count << endl;
	}



public:


	Tracker Config;
	//생성 후 설정으로 어떤 정보를 출력할 지 결정할 수 있습니다.



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
			cout << "\n\n 경고. 반환되지 않은 Object가 있습니다.\n\n";
		else
			cout << "\n\n  모든 Object가 반환되었습니다.\n\n";
		for (int i = 0; i < Count; i++)
		{
			for (int j = 0; j < Size; j++) {
				delete mObjectPoolOrigin[i][j];
			}
			//실제 데이터 삭제는 소멸자에서 처리한다.
			//Pop로 받는것은 Data의 포인터일 뿐.
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
	* DATA는 Satck 형으로 운영되지만, 삭제는 mObjectPoolOrigin 에서 일괄적으로 실행하므로,
	* mObjectPool 에서는 할당과 반납만 관리합니다. 또한 DATA**(이중 배열 포인터) 형에 포인터
	* 3중 포인터를 통하여 객체에 직접 접근하지 않고 주소값만 바꿀수 있도록 합니다.
	
	
	*/

	DATA* pop()						//단일 Object만 할당.
	{
		if (IsEmpty())				//셀이 비어있다면 추가 생성
			AddCell();
		DATA* RetVal =
			mObjectPool[Index / Size][Index % Size];

		RenderPop(RetVal);

		Index++;
		return RetVal;
	}
	DATA** pop_cell()				//Object 셀 전체를 할당.
	{
		AddCell();					//셀을 통째로 할당하므로 조건 없이 셀 하나 추가 생성
		DATA** RetVal = mObjectPool[Index / Size];
		RenderPop(RetVal);
		Index += Size;
		return RetVal;
	}
	void push(DATA* Data)			//Object만 반환
	{
		Index--;
		RenderPush(Data);
		mObjectPool[Index / Size][Index % Size] = Data;

	};
	void push(DATA** Data)			//Object만 반환
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
			printf("\nPool에 남은 Object 갯수. : %d 개. \n", RenderCount - Index);
		}

		if (Config.data) {
			if (Config.origindata)
				printf("할당된 모든 Object 갯수: %d 개, ", GameObject::CountGenGameObject);
			if (Config.count)
				printf("정보 : {");
			else
				printf("\nPool에 남은 Object 정보: {");
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


	Pool.Config.pop = true;			//POP 시에 뭘 POP했는지 출력
	Pool.Config.data = true;		//남은 DATA 정보 출력
	Pool.Config.push = true;		//PUSH시에 뭘 PUSH 햇는지 출력
	Pool.Config.addcell = true;		//Cell이 추가될 때 출력
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