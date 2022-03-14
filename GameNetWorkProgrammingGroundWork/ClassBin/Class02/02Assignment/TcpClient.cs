using System; 
using System.Text; 
using System.IO; 
using System.Net.Sockets; 
using MyNetWork;

class CTcpEchoClient:MyNetWork.MyTCPClient
{
    byte[] byteBuffer;
    public CTcpEchoClient()
    {
        this.m_TcpClient =null;
        this.m_MyStream = null;
        this.m_strSeverIp = "127.0.0.1";
        this.m_iBufferSize =32; 
        this.m_iPort =9000;
        this.byteBuffer = Encoding.ASCII.GetBytes("TCP Client");
    }

    public bool Init()
    {
        try
        {
            m_TcpClient = new TcpClient(m_strSeverIp,m_iPort);
            Console.WriteLine("Connect..");
            m_MyStream = m_TcpClient.GetStream();
            return true;
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
    public void Run()
    {
        try
        {
            RenderData();
            m_MyStream = m_TcpClient.GetStream();
            m_MyStream.Write(byteBuffer,0,byteBuffer.Length);

            int TotalRcvd=0;
            int BytesRcvd=0;
        
            while(TotalRcvd < byteBuffer.Length)
            {
                if((BytesRcvd = m_MyStream.Read(byteBuffer,TotalRcvd,
                byteBuffer.Length - TotalRcvd))== 0)
                {

                Console.WriteLine("Connection closed prematurely.");
                break;

                }

                TotalRcvd += BytesRcvd;
                Console.WriteLine("Received {0} bytes from server: {1}", TotalRcvd, 
                Encoding.ASCII.GetString(byteBuffer, 0, TotalRcvd));
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }finally{
            m_MyStream.Close();
            m_TcpClient.Close();
        }

    }
}

