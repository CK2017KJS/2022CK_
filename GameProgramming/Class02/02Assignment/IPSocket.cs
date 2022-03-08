

using System;
using System.Net;

using System.Net.Sockets; // Socket


class CSocket// IP Adress Class
{
    IPAddress    m_Adr;
    IPEndPoint    m_End;

    Socket  m_Socket;

    public CSocket()
    {
        m_Adr = IPAddress.Parse("127.0.0.1");
        m_End = new IPEndPoint(m_Adr, 8000);

        m_Socket = new Socket(AddressFamily.InterNetwork, 
            SocketType.Stream,
            ProtocolType.Tcp);

    }

    public void Run()
    {
        Console.WriteLine(" Adr Family   : {0} " , m_Socket.AddressFamily);
        Console.WriteLine(" Sock Types   : {0}"  , m_Socket.SocketType);
        Console.WriteLine(" ProtoColType : {0}"  , m_Socket.ProtocolType);
        Console.WriteLine(" BlockIng:    : {0}\n", m_Socket.Blocking);

        m_Socket.Blocking = false;
        Console.WriteLine(" New BlockIng  : {0}", m_Socket.Blocking);
        Console.WriteLine(" Connecrted    : {0}\n", m_Socket.Connected);

        m_Socket.Bind(m_End);

        IPEndPoint ipEndPoint = (IPEndPoint)m_Socket.LocalEndPoint;
        Console.WriteLine(" Local endPoint :{0}",ipEndPoint.ToString());
        m_Socket.Close();

    }
}
