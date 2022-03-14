using System; 
using System.Text; 
using System.Net; 
using System.Net.Sockets; 
using MyNetWork;

class CUdpEchoSever:MyNetWork.MyUDPClient
{
    byte[] SendPacket;
    public CUdpEchoSever()
    {
        this.m_iPort =9100;
        this.m_UDPClient=null;
    }

    public bool Init()//Init() 초기화 실행후에 Error이 발생히면 False를 반환합니다.
    {
        try
        {
            this.m_UDPClient = new UdpClient(m_iPort);  
            return true;
        }
        catch(SocketException se){
           Console.WriteLine(se.ErrorCode + ": " + se.Message);
            return false;
        }
    }
    public void Run()
    {
        IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any, 0);

        for(;;)
        {
            try
            {
                byte[] byteBuffer = m_UDPClient.Receive(ref EndPoint);
                Console.Write("Handling client at " + EndPoint + " - ");
                m_UDPClient.Send(byteBuffer, byteBuffer.Length, EndPoint); 
                Console.WriteLine("echoed {0} bytes.", byteBuffer.Length);
            
            }
            catch(SocketException se)
            {

            Console.WriteLine(se.ErrorCode + ": " + se.Message);
            
            }
        }
    }
}
