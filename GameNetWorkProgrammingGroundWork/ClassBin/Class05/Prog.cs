using System;
using System.Threading;
using System.Collections.Generic;


/*


실행하고 방향키로 이동할 경우, 
O 모양의 플레이어와 X모양의 몬스터가 움직입니다.
몬스터에 닿는다면 잠시 후 게임은 종료되며, 일정 시간마다 몬스터가
추가됩니다.


Thread 사용 부분: Render/Input
Lock 사용 부분 : Collider(Collider에 Lock이 없다면 
계산중에 값이 바뀔 수도 있음)

 
 */




class CCore
{
    public static Random rand = new Random();
    //Random 객체입니다.

    public static int GameFPS = 1000 / 24;
    public static int MonsterDelay = 1000 / 1;
    public static int PlayerDelay = 1000 / 2;
    //초당 행동 횟수를 결정할 Delay 입니다.

    public static int GameSecond = 0;
    //게임시작 후 버틴 시간입니다. 몬스터 추가 생성에 사용됩니다.
    int AddSecond=0;
    public static bool GameOn = true;
    //Game On/Off (종료조건)을 확인하는 함수입니다.

    public static ConsoleKeyInfo InputKey;
    //누른 Key의 정보를 받아옵니다.


    //Game Object 입니다. 
    List<GameObjectBased> Monsters;
    GameObjectBased Player;
    GameObjectBased Map;


    Thread RenderThread;
    Thread MonsterUpdate;

    private object LockObj = new object();
    //Lock 에 사용될 Object입니다. 
    //충돌 처리를 사용하는 함수에만 사용됩니다.


    public void Init()
        //CCore 초기화.
    {
        Console.CursorVisible = false;

        Monsters = new List<GameObjectBased>();
        Player = new CPlayer();
        Map = new CGameBoard();

        Monsters.Add(new CMonster());
        Monsters.Add(new CMonster());
        Monsters.Add(new CMonster());
        Monsters.Add(new CMonster());
        Monsters.Add(new CMonster());
        Monsters.Add(new CMonster());

        RenderThread = new Thread(RenderWithThread);
        MonsterUpdate = new Thread(MonsterUpdateWithThread);

    }




    public CCore(){}

    public void Run()
    {

        
        RenderThread.Start();
        MonsterUpdate.Start();

        while (GameOn)
        {

            CheakCollider();
            GetKey();
        }

        Thread.Sleep(1000);//두개 스레드 종료 기다리기.

        EndMessage();
    }

    void RenderWithThread()
    {
        while (GameOn)
        {
            //스레드로 오브젝트들을 출력합니다.
            Map.Render();

            foreach (GameObjectBased A in Monsters)
            {
                A.Render();
            }

            Player.Render();
            Thread.Sleep(GameFPS);
            GameSecond++;
            AddSecond++;

            Console.SetCursorPosition(0,0);
            //모두 렌더링 한 후 커서를 제자리로.
        }
    }
    void GetKey()
    {
        //계속해서 키보드 값을 입력받습니다.
        InputKey = Console.ReadKey(true);
        Player.Input();
        Thread.Sleep(PlayerDelay);
    }
    void MonsterUpdateWithThread()
    {
        while (GameOn)
        {
            Thread.Sleep(MonsterDelay);
            foreach (GameObjectBased A in Monsters)
            {
                A.Update();
            }
            if (AddSecond > 72)//3초마다 몬스터 1마리씩 증가
            {
                Monsters.Add(new CMonster());
                AddSecond = 0;
            }
            CheakCollider();
        }
    }
    void CheakCollider()
    {
        lock (LockObj)//충돌 처리는 한번에 하나에 스레드에서만 가능하도록 처리.
        {
            foreach (GameObjectBased A in Monsters)
            {
                if (A.X == Player.X && A.Y == Player.Y)
                {
                    Player.Collider(A);
                    return;
                }

            }
        }
    }

    void EndMessage()
    {
        Map.Render();
        Console.SetCursorPosition(5, 5);
        Console.WriteLine("Game End.");
    }
}




class GameObjectBased
{
    public string Name;
    public int X;
    public int Y;
    public virtual void Logic(){}
    public virtual void Input(){ }
    public virtual void Collider(GameObjectBased A) { }
    public virtual void Update() { }
    public virtual void Render() 
    {
        Console.SetCursorPosition(X, Y);
    }

}





class CGameBoard: GameObjectBased
{
    //뒷배경을 렌더링하기 위한 오브젝트
    public static int MapX = 20;
    public static int MapY = 20;
    public CGameBoard()
    {
        X = 0;
        Y = 0;
    }
    public override void Render()
    {
        base.Render();
        for (int i = 0; i < MapY; i++)
        {
            Console.WriteLine("                              ");
        }
        Console.Write("{0} 프레임 생존.(약{1}초)", CCore.GameSecond, CCore.GameSecond/24);
    }
}


class CMonster : GameObjectBased
{
    //Update 시에 매번 랜덤한 방향으로 이동합니다.


    public CMonster()
    {
        Name = "Monster";
        X = CCore.rand.Next() % CGameBoard.MapX;
        Y = CCore.rand.Next() % CGameBoard.MapY;
    }
    public override void Logic()
    {
        Update();
    }
    public override void Update()
    {

        int MoveRotate = CCore.rand.Next() % 4;
        switch (MoveRotate)
        {
            case 1:
                X++;
                break;
            case 2:
                X--;
                break;
            case 3:
                Y--;
                break;
            case 4:
                Y++;
                break;
        }
        X = Math.Clamp(X, 0, CGameBoard.MapX);
        Y = Math.Clamp(Y, 0, CGameBoard.MapY);

    }

    public override void Render()
    {
        base.Render();
        Console.Write("X");
    }
}
class CPlayer: GameObjectBased
{
    public override void Render()
    {
        base.Render();
        Console.Write("O");
    }
    public override void Collider(GameObjectBased A)
    {
        if (A.Name == "Monster")
        {
            CCore.GameOn = false;
        }
    }
    public override void Input() {
        switch (CCore.InputKey.Key)
        {
            case ConsoleKey.LeftArrow:
                X--;
                break;
            case ConsoleKey.RightArrow:
                X++;
                break;
            case ConsoleKey.UpArrow:
                Y--;
                break;
            case ConsoleKey.DownArrow:
                Y++;
                break;
            case ConsoleKey.Q:
                CCore.GameOn = false;
                return;
        }
    }
}










namespace CsTempProj
{
    class Program
    {
        static void Main(string[] args)
        {
            CCore Core = new CCore();
            Core.Init();
            Core.Run();

        }
        

        
    }


}