using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MyUdp
{

    public class Client : MyBasedSocket.UDP.Client
    {
        public void Run()
        {
            data = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            CS.SendTo(data, EP);

            byte[] ndata = new byte[BUFFER_SIZE];
            int nrecv = CS.ReceiveFrom(ndata, ref EP);
            Console.WriteLine(Encoding.UTF8.GetString(ndata, 0, nrecv));


        }
    }

    public class SeverwithThread : MyBasedSocket.UDP.Sever
    {
        public void Run()
        {
            Thread SeverThread = new Thread(SeverFunc);
            SeverThread.IsBackground = true;
            SeverThread.Start();
            Thread.Sleep(500);
            Console.WriteLine("Press Any Key is End.");
            Console.ReadLine();
        }
        void SeverFunc(object obj)
        {
            MyBasedSocket.UDP.Client srv = new MyBasedSocket.UDP.Client();
            srv.port = 10200;
            srv.Init();
            srv.Connect();

            IPEndPoint ClientEp = new IPEndPoint(IPAddress.Any, port);
            srv.CS.Bind(ClientEp);


            EP = new IPEndPoint(IPAddress.None, 0);

            srv.data = new byte[1024];
            while (true)
            {
                srv.recv = srv.CS.ReceiveFrom(srv.data, ref this.EP);
                string Text = Encoding.UTF8.GetString(srv.data, 0, srv.recv);
                Console.WriteLine(Text);

                byte[] sendBytes = Encoding.UTF8.GetBytes("Hellow: " + Text);
                srv.CS.SendTo(sendBytes,EP);

            }


        }

    }
}