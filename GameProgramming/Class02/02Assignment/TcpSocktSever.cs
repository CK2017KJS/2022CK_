using System; 
using System.Net; 
using System.Net.Sockets;



class CTCPServer
{
    int BUFSIZE;
    int m_iSeverPort;
    TcpListener m_Listener;

    public CTCPServer()
    {
        BUFSIZE = 32;
        m_iSeverPort  =9000;
        m_Listener = new TcpListener(IPAddress.Any,m_iSeverPort);

    }

    public void Run()
    {
        try
        {
            m_Listener.Start();
            Console.WriteLine("Tcp Sever On");
        }
        catch(SocketException se)
        {
           Console.WriteLine(se.ErrorCode + ": " + se.Message);
        }
    }

}