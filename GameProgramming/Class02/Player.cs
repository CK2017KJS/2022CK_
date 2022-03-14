using System;
using System.Text;


class CPlayer
{
    string m_sName;
    int m_iHP;
    int m_iLevel;
    int m_iEXP;

    public void Render()
    {
        Console.WriteLine("-Name");
        Console.WriteLine("-HP");
        Console.WriteLine("-Level");
        Console.WriteLine("-EXP");
    }
    public void Whoareyou()
    {
        Console.WriteLine("It is Player.");
    }

}

class CWarrior:CPlayer
{
    public override void Whoareyou()
    {
        Console.WriteLine("I Am Warrior!");
    }
}

class ConsoleProg{
    static void Main(string[] args)
    {
        CWarrior  MyWarrior = new CWarrior();
        CPlayer MyPlayer = new CPlayer();

        Console.WriteLine("=Player.Render()=============");
        MyPlayer.Render();
        Console.WriteLine("-Warrior.Render()-------------");
        MyWarrior.Render();
        Console.WriteLine("==============");

        Console.WriteLine("=Player.WhoAreYou?=============");
        MyPlayer.Whoareyou();
        Console.WriteLine("-Warrior.WhoAreYou?-------------");
        MyWarrior.Whoareyou();
        Console.WriteLine("==============");
    }

}