<h1>GameProgramming</h1>


- var Data Type

    명시적으로 지정된 데이터 타입 대신 암시적으로 지역변수의 데이터 타입으로 사용한다.

    
    ```cs

    var i = 5;              //int
    var s = "Hello";        //String
    var a = new[]{0,1,2};   //int[]

    for(var x=1;x<10;x++)           //for문에서도 사용 가능
    foreach(var Char in string){}   //foreach문에서도 사용 가능하다.

    ```

- Var Data 타입을 많이 쓰는게 유리할까?..?

    * 일단 명시적인 사용법을 권장함
    * 그러나 이전보다는 var에 대한 사용에 대해 긍정적으로 변함
    * var 사용의 빈도는 협업 프로그래머와 사전 협의함
    * 임시 사용 변수는 var 사용 권장
    * 컴파일러가 우리보다 똑똑해지고,(이미 똑똑함) 가독성을 중시하는 가독성 메타가 도래함에 따라서 var 사용을 하는 추세지만, 제1원칙은 사전에 규정된 규칙을 지키는 것이다. 이는 언어의 표준보다도 우위에 있을 수 있다(예시: IDE에서 자료형을 표현하지만 여전히 헝가리안 표기법을 고수하는 등 ..)


- C# Property

    * 데이터 한정자

        기존의 C++ 등을 생각하면 된다. 흔하디 흔한 데이터 한정자이다.
        >Public,Private.. 

    * 접근자(Accessor),Property

        C++에서는 존재하지 않는 개념으로, Get,Set등의 Public 함수를
        매번 만들어야 하는 귀찮음을 방지하기 위해 만들어졌다.

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

        생각보다 많은 경우의 속성자가 많다. 집가서 한번 복습해볼 것 ,,,




- Generic

    C++의 템플릿과 유사하며, 클래스나 메소드에서 <T\> 연산자를 사용하여 나타낸다.
    런타임(시작 후)에 특정한 현식으로 대체될 때 까지 Generic을 유지한다.
    동일한 형태로 인수만 다른 형태일 경우 오버로딩을 통하여 구현..
    -이 경우, 제너릭을 통한 데이터 형식을 일반화해서 코드를 간소화한다 .
        
    ```cs
    static T sum<T>(T Val1,T Val2){
        return Val1+Val2;
        }
        //컴파일 에러로 개같이 멸망
    ```

    개같이 멸망하는 원인-

    키워드 <T\>가 정확히 무슨 자료형을 나타내는지 알 수 없으므로 ..

    ```cs
    static T sum<T>(T Val1,T Val2){
        dynamic data =Val1;
        dynamic data2= Val2;
        //Dynamic 은 컴파일시 처리하지 않다가, 헌타임 시에서 처리.
        return Val1+Val2;
        }
        
    ```


    -C# Generic Class처리

    ```cs

    class CommonData<T>
    {
        private T[] dataArray;
        private T _subValue;

        public T subValue{get;set;}

        //Generic 을 사용하더라도 Property속성은 사용 가능하다.
    }

    ```








- C# Collection 

    C++ 에서의 STL에 대응되는 자료구조 정의?

    * Array,List
    * ArrayList
    * Stack
    * Queue
    * Dictionary
    
    >단순 Array를 제외하고는 모두 Generic을 이용하여 만들어진다.
    

