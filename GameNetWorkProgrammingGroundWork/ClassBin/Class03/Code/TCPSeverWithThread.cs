using System;
using System.Threading;

namespace TCPSeverWithThread
{
    class ConnectionThread : NetWorkSocket.MyTCPSever
    {
        NetWorkSocket.MyTCPClient MyClient;
        static int connections = 0;
        public void HandleConnection()
        {
            string GetDatas;
            MyClient.data = new byte[1024];
            MyClient.m_TcpClient = m_Listener.AcceptTcpClient();
            MyClient.m_MyStream = MyClient.m_TcpClient.GetStream();

            connections++;

            MyClient.WriteMessage("Welcome");

            while (true)
            {
                GetDatas = MyClient.ReadMessage();

                if (recv == 0)
                    break;

                MyClient.WriteMessage(data);

            }

            MyClient.ClosedData();
            connections--;
            Console.WriteLine("Client Disconnected: {0} Actives", connections);
        }
    }
    class TcpSverWithThread : NetWorkSocket.MyTCPSever
    {
        public override bool Init()
        {

            base.Init();


            return true;
        }

        public void Run()
        {
            while (true)
            {
                while (!this.m_Listener.Pending())
                {
                    Thread.Sleep(1000);
                }
                ConnectionThread newConnect = new ConnectionThread();

                newConnect.m_Listener = this.m_Listener;
                Thread newThread = new Thread(new ThreadStart(newConnect.HandleConnection));
                newThread.Start();
            }

        }


    }

}