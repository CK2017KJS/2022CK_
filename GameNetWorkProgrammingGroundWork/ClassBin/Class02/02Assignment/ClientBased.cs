using System;
using System.Text; 
using System.IO; 
using System.Net.Sockets; 

//A data value that both the 
//server and the client have.
//포트,IP,버퍼 사이즈같은 값들을
//Pulbic로 저장해두었습니다.
namespace MyNetWork
{
    public class NetWorkBaseSocket
    {
        public String m_strSeverIp;
        public int m_iPort;
        public int m_iBufferSize;  

        //This Data OutPut
        //현재 데이터를 출력합니다
        public void RenderData()
        {
            Console.WriteLine("Connect Sever. IP Adress: {0}",m_strSeverIp);
            Console.WriteLine("Port Number: {0}",m_iPort);
            Console.WriteLine("BufferSize{0}",m_iPort);
        }  
    }

    //TCP와 UDP클라이언트는 각각 다른 Clint자료형이므로 분리됩니다.
    public class MyTCPClient:NetWorkBaseSocket
    {
        public TcpClient  m_TcpClient;
        public NetworkStream m_MyStream;

    }
    public class MyUDPClient:NetWorkBaseSocket
    {
        public UdpClient m_UDPClient;
    }




}