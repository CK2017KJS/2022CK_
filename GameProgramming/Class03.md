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
        