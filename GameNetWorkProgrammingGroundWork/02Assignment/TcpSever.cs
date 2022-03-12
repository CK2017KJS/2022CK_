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

        byte[] rcvBuffer = new byte[BUFSIZE];
        int bytsRecvd;

        for(;;)
        {
            TcpClient Myclient = null;
            NetworkStream MynetStream = null;


            try{
                
                Myclient = m_Listener.AcceptTcpClient();
                MynetStream = Myclient.GetStream();
                Console.Write("Handling Client.. ");

                int totalByted = 0;
                while((bytsRecvd = MynetStream.Read(rcvBuffer,0,rcvBuffer.Length))>0)
                {
                    MynetStream.Write(rcvBuffer,0,bytsRecvd);
                    totalByted += bytsRecvd;
                }
                Console.Write("Echo {0} bytes",totalByted);


                MynetStream.Close();
                Myclient.Close();

            }
            catch(Exception Errors){
                Console.WriteLine(Errors.Message);
                MynetStream.Close();
            }
        }
    }

}