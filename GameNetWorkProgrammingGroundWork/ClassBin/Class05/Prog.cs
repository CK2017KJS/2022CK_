using System;
using System.Threading;
using System.Collections.Generic;


/*


�����ϰ� ����Ű�� �̵��� ���, 
O ����� �÷��̾�� X����� ���Ͱ� �����Դϴ�.
���Ϳ� ��´ٸ� ��� �� ������ ����Ǹ�, ���� �ð����� ���Ͱ�
�߰��˴ϴ�.


Thread ��� �κ�: Render/Input
Lock ��� �κ� : Collider(Collider�� Lock�� ���ٸ� 
����߿� ���� �ٲ� ���� ����)

 
 */




class CCore
{
    public static Random rand = new Random();
    //Random ��ü�Դϴ�.

    public static int GameFPS = 1000 / 24;
    public static int MonsterDelay = 1000 / 1;
    public static int PlayerDelay = 1000 / 2;
    //�ʴ� �ൿ Ƚ���� ������ Delay �Դϴ�.

    public static int GameSecond = 0;
    //���ӽ��� �� ��ƾ �ð��Դϴ�. ���� �߰� ������ ���˴ϴ�.
    int AddSecond=0;
    public static bool GameOn = true;
    //Game On/Off (��������)�� Ȯ���ϴ� �Լ��Դϴ�.

    public static ConsoleKeyInfo InputKey;
    //���� Key�� ������ �޾ƿɴϴ�.


    //Game Object �Դϴ�. 
    List<GameObjectBased> Monsters;
    GameObjectBased Player;
    GameObjectBased Map;


    Thread RenderThread;
    Thread MonsterUpdate;

    private object LockObj = new object();
    //Lock �� ���� Object�Դϴ�. 
    //�浹 ó���� ����ϴ� �Լ����� ���˴ϴ�.


    public void Init()
        //CCore �ʱ�ȭ.
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

        Thread.Sleep(1000);//�ΰ� ������ ���� ��ٸ���.

        EndMessage();
    }

    void RenderWithThread()
    {
        while (GameOn)
        {
            //������� ������Ʈ���� ����մϴ�.
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
            //��� ������ �� �� Ŀ���� ���ڸ���.
        }
    }
    void GetKey()
    {
        //����ؼ� Ű���� ���� �Է¹޽��ϴ�.
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
            if (AddSecond > 72)//3�ʸ��� ���� 1������ ����
            {
                Monsters.Add(new CMonster());
                AddSecond = 0;
            }
            CheakCollider();
        }
    }
    void CheakCollider()
    {
        lock (LockObj)//�浹 ó���� �ѹ��� �ϳ��� �����忡���� �����ϵ��� ó��.
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
    //�޹���� �������ϱ� ���� ������Ʈ
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
        Console.Write("{0} ������ ����.(��{1}��)", CCore.GameSecond, CCore.GameSecond/24);
    }
}


class CMonster : GameObjectBased
{
    //Update �ÿ� �Ź� ������ �������� �̵��մϴ�.


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