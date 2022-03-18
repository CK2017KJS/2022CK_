using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MyUdp;
using MyTcp;
using ThreadControl;


/*

Thread Programming GroundWork 연습하기
    - Join 사용 연습
    - Thread 에서의 인자 전달 방법
    - Thread 에서의 Moniter 또는 Lock 연습
    - C#기반의 Thread 서버/ C#기반의 Tcp Client(Thread아님)
    - C#기반의 UDP Thread 서버/C# 기반의 UDP Client
    


*/


class Program
{

    static void Main(string[] args)
    {
        string IP = "127.0.0.1";
        int TCPPort = 9050;
        int UDPPort = 10200;

        Console.WriteLine("1. Udp Client");
        Console.WriteLine("2. Udp Sever");
        Console.WriteLine("3. Tcp Client");
        Console.WriteLine("4. Tcp Sever");
        Console.WriteLine("5. UsedThreadJoin");
        Console.WriteLine("6. ThreadArgsControl");
        Console.WriteLine("7. ThreadLockControl-Mointer");
        Console.WriteLine("8. ThreadLockControl-Lock");











        MyUdp.Client UdpClient;
        MyUdp.SeverwithThread UdpSever ;
        MyTcp.Client.WithClass TcpClient ;
        MyTcp.Sever.WithThread TcpSevers;


        string Sel = Console.ReadLine();

        if (Sel == "1")
        {
            Console.WriteLine("\n\n 1. Udp Client");
            
            UdpClient = new MyUdp.Client();
            UdpClient.port = UDPPort;
            UdpClient.ip = IP;
            UdpClient.Init();
            UdpClient.Connect();
            UdpClient.Run();
            
        }
        else if (Sel == "2")
        {
            Console.WriteLine("\n\n 2. Udp Sever");
            
            UdpSever = new MyUdp.SeverwithThread();
            UdpSever.port = UDPPort;
            UdpSever.ip = IP;
            UdpSever.Init();
            UdpSever.Connect();
            UdpSever.Run();
        }
        else if (Sel == "3")
        {
            Console.WriteLine("\n\n 3. Tcp Client");
            
            TcpClient = new MyTcp.Client.WithClass();
            TcpClient.port = TCPPort;
            TcpClient.ip = IP;
            TcpClient.Init();
            TcpClient.Connect();
            TcpClient.Run();
        }
        else if (Sel == "4")
        {
            Console.WriteLine("\n\n 4. Tcp Sever");
            TcpSevers = new MyTcp.Sever.WithThread();
            TcpSevers.port = TCPPort;
            TcpSevers.ip = IP;
            TcpSevers.Init();
            TcpSevers.Connect();
            TcpSevers.Run();
        }
        else if (Sel == "5")
        {
            Console.WriteLine("\n\n 5. UsedThreadJoin");
            ThreadControl.UsedJoin J = new ThreadControl.UsedJoin();
            J.Run();
        }
        else if (Sel == "6")
        {
            Console.WriteLine("\n\n 6. ThreadArgsControl");
            ThreadControl.ThreadArgsControl J = new ThreadControl.ThreadArgsControl();
            J.Run();
        }
        else if (Sel == "7")
        {
            Console.WriteLine("\n\n 7. ThreadLockControl-Mointer");
            ThreadControl.ThreadMoniterControl J = new ThreadControl.ThreadMoniterControl();
            J.Run();
        }
        else if (Sel == "8")
        {
            Console.WriteLine("\n\n 8. ThreadLockControl-Lock");
            ThreadControl.ThreadLockControl J = new ThreadControl.ThreadLockControl();
            J.Run();
        }




    }




}