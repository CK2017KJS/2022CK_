using System;
using System.Text; 
using System.IO; 
using System.Net.Sockets; 

//A data value that both the 
//server and the client have.
//��Ʈ,IP,���� ������� ������
//Pulbic�� �����صξ����ϴ�.
namespace MyNetWork
{
    public class NetWorkBaseSocket
    {
        public String m_strSeverIp;
        public int m_iPort;
        public int m_iBufferSize;  

        //This Data OutPut
        //���� �����͸� ����մϴ�
        public void RenderData()
        {
            Console.WriteLine("Connect Sever. IP Adress: {0}",m_strSeverIp);
            Console.WriteLine("Port Number: {0}",m_iPort);
            Console.WriteLine("BufferSize{0}",m_iPort);
        }  
    }

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