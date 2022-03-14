using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetWorkSocket;

namespace TCPClients
{
    class TCPClient:NetWorkSocket.MyTCPClient
    {

        public override bool Init()
        {
            base.Init();

            try
            {
                m_TcpClient = new TcpClient(m_strSeverIp,m_iPort);
            }
            catch(SocketException)
            {
                Console.WriteLine("Error To Connect");
                return false;
            }
            return true;
        }

        public void Run()
        {
            string input;
            m_MyStream = m_TcpClient.GetStream();
            Console.WriteLine(ReadMessage());

            while(true)
            {
                input = Console.ReadLine();
                if(input == "exit")
                    break;

                WriteMessage(input);
                m_MyStream.Flush();

                Console.WriteLine(ReadMessage());


            }
            Console.WriteLine("Disconect");
            ClosedData();
        }
    }

}