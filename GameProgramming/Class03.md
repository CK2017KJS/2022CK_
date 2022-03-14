<h1>GameProgramming</h1>


- var Data Type

    ��������� ������ ������ Ÿ�� ��� �Ͻ������� ���������� ������ Ÿ������ ����Ѵ�.

    
    ```cs

    var i = 5;              //int
    var s = "Hello";        //String
    var a = new[]{0,1,2};   //int[]

    for(var x=1;x<10;x++)           //for�������� ��� ����
    foreach(var Char in string){}   //foreach�������� ��� �����ϴ�.

    ```

- Var Data Ÿ���� ���� ���°� �����ұ�?..?

    * �ϴ� ������� ������ ������
    * �׷��� �������ٴ� var�� ���� ��뿡 ���� ���������� ����
    * var ����� �󵵴� ���� ���α׷��ӿ� ���� ������
    * �ӽ� ��� ������ var ��� ����
    * �����Ϸ��� �츮���� �ȶ�������,(�̹� �ȶ���) �������� �߽��ϴ� ������ ��Ÿ�� �����Կ� ���� var ����� �ϴ� �߼�����, ��1��Ģ�� ������ ������ ��Ģ�� ��Ű�� ���̴�. �̴� ����� ǥ�غ��ٵ� ������ ���� �� �ִ�(����: IDE���� �ڷ����� ǥ�������� ������ �밡���� ǥ����� ����ϴ� �� ..)


- C# Property

    * ������ ������

        ������ C++ ���� �����ϸ� �ȴ�. ���ϵ� ���� ������ �������̴�.
        >Public,Private.. 

    * ������(Accessor),Property

        C++������ �������� �ʴ� ��������, Get,Set���� Public �Լ���
        �Ź� ������ �ϴ� �������� �����ϱ� ���� ���������.

        ```cs

            pulbic class Player                     
            {
                private int Value;                      //Legarcy(C++ Sytle)

                public int getValue(){return Value;}
                public void setValue(int i){Value = i}

                private int value2;                     //C# Property
                public int Value{get{return value2;}
                set{if(Value3 < 10) value2 = Value;}}


                private int value3;
                public int Value3{get;private set;}     

            }

        ```

        �������� ���� ����� �Ӽ��ڰ� ����. ������ �ѹ� �����غ� �� ,,,




- Generic

    C++�� ���ø��� �����ϸ�, Ŭ������ �޼ҵ忡�� <T\> �����ڸ� ����Ͽ� ��Ÿ����.
    ��Ÿ��(���� ��)�� Ư���� �������� ��ü�� �� ���� Generic�� �����Ѵ�.
    ������ ���·� �μ��� �ٸ� ������ ��� �����ε��� ���Ͽ� ����..
    -�� ���, ���ʸ��� ���� ������ ������ �Ϲ�ȭ�ؼ� �ڵ带 ����ȭ�Ѵ� .
        
    ```cs
    static T sum<T>(T Val1,T Val2){
        return Val1+Val2;
        }
        //������ ������ ������ ���
    ```

    ������ ����ϴ� ����-

    Ű���� <T\>�� ��Ȯ�� ���� �ڷ����� ��Ÿ������ �� �� �����Ƿ� ..

    ```cs
    static T sum<T>(T Val1,T Val2){
        dynamic data =Val1;
        dynamic data2= Val2;
        //Dynamic �� �����Ͻ� ó������ �ʴٰ�, ��Ÿ�� �ÿ��� ó��.
        return Val1+Val2;
        }
        
    ```


    -C# Generic Classó��

    ```cs

    class CommonData<T>
    {
        private T[] dataArray;
        private T _subValue;

        public T subValue{get;set;}

        //Generic �� ����ϴ��� Property�Ӽ��� ��� �����ϴ�.
    }

    ```








- C# Collection 

    C++ ������ STL�� �����Ǵ� �ڷᱸ�� ����?

    * Array,List
    * ArrayList
    * Stack
    * Queue
    * Dictionary
    
    >�ܼ� Array�� �����ϰ�� ��� Generic�� �̿��Ͽ� ���������.
    

