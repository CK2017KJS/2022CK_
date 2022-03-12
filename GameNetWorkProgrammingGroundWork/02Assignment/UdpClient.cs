using System; 
using System.Text; 
using System.Net; 
using System.Net.Sockets; 
using MyNetWork;

class CUdpEchoClient:MyNetWork.MyUDPClient
{
    byte[] SendPacket;
    public CUdpEchoClient()
    {
        this.m_strSeverIp = "127.0.0.1";
        this.m_iBufferSize =32; 
        this.m_iPort =9100;
        this.SendPacket = Encoding.ASCII.GetBytes("UDP Client");
    }

    public bool Init()
    {
        this.m_UDPClient = new UdpClient();  
        return true;
    }
    public void Run()
    {
        try
        {
            RenderData();
            m_UDPClient.Send(SendPacket,SendPacket.Length,m_strSeverIp,m_iPort);
            Console.WriteLine("Sent {0} bytes to the server...", SendPacket.Length);

            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Any,0);
            byte[] rcvPacket = m_UDPClient.Receive(ref EndPoint);
            
            Console.WriteLine("Received {0} bytes from {1}: {2}", 
            rcvPacket.Length, IPEndPoint,Encoding.ASCII.GetString(rcvPacket, 0, rcvPacket.Length));
            
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }finally{
            m_UDPClient.Close();
        }

    }
}
