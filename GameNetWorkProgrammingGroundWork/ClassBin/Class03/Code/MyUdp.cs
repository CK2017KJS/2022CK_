

namespace MyUdp
{

    public class Client : MyBasedSocket.UDP.Client
    {
        void Run()
        {
            Read();
            EndPoint SeverEP = new IPEndPoint(IPAddress.Loopback, 10200);
            base.CS.SendTo(data, SeverEP);

            Read();
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));


        }
    }

    public class SeverwithThread : MyBasedSocket.UDP.Sever
    {

        void SeverFunc(object obj)
        {
            MyBasedSocket.UDP.Client srv = new MyBasedSocket.UDP.Client();
            srv.port = 10200;
            srv.Init();
            srv.Connect();

            EndPoint ClientEp = new IPEndPoint(IPAddress.None, 0);

            while (true)
            {
                srv.Read();
                string Text = Encoding.UTF8.GetString(srv.data, 0, srv.recv);
                Console.WriteLine(Text);

                srv.data = Encoding.UTF8.GetBytes("Hellow: " + Text);
                srv.CS.SendTo(srv.data, srv.EP);

            }


        }

    }
}