using System;
using System.Text; 
using System.IO; 
using System.Net.Sockets; 

//A data value that both the 
//server and the client have.
//��Ʈ,IP,���� ������� ������
//Pulbic�� �����صξ����ϴ�.
namespace NetWorkSocket
{
    public class NetWorkBaseSocket
    {
        public String m_strSeverIp;
        public int m_iPort;

        
        public int recv;

        public int m_iBufferSize;  
        public byte[] data;
        //This Data OutPut
        //���� �����͸� ����մϴ�
        public void RenderData()
        {
            Console.WriteLine("Connect Sever. IP Adress: {0}",m_strSeverIp);
            Console.WriteLine("Port Number: {0}",m_iPort);
            Console.WriteLine("BufferSize{0}",m_iPort);
        }  

        
        
    }

    public class MyTCPSever:NetWorkBaseSocket
    {
        public TcpListener m_Listener;
        public virtual bool Init()
        {
            m_Listener = new TcpListener(m_iPort);
            m_Listener.Start();
            return true;
        }



    }



    //TCP�� UDPŬ���̾�Ʈ�� ���� �ٸ� Clint�ڷ����̹Ƿ� �и��˴ϴ�.
    public class MyTCPClient:NetWorkBaseSocket
    {
        public TcpClient  m_TcpClient;
        public NetworkStream m_MyStream;
        


        public virtual bool Init()
        {
            this.data = new byte[1024];
            this.m_TcpClient = new TcpClient(m_strSeverIp,m_iPort);
            this.m_MyStream = m_TcpClient.GetStream();
            return true;
        }
        public string ReadMessage()
        {
            data = new byte[1024];
            recv = m_MyStream.Read(data,0,data.Length);
            return Encoding.ASCII.GetString(data,0,recv);
        }
        public void WriteMessage(string Data)
        {
            m_MyStream.Write(Encoding.ASCII.GetBytes(Data),0,Data.Length);
        }

        public void WriteMessage(byte[] InputData)
        {
            m_MyStream.Write(InputData,0,recv);

        }
        public void ClosedData()
        {
            m_MyStream.Close();
            m_TcpClient.Close();
        }

    }
    public class MyUDPClient:NetWorkBaseSocket
    {
        public UdpClient m_UDPClient;
    }




}