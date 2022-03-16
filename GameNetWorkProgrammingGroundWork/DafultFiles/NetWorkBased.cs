using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;


namespace MyBasedSocket//Base Program Socket
{
    
    
    public class BasedSocket
    {
        public static const int BUFFER_SIZE = 1024;
        public string ip;
        public int port,recv;
        public byte[] data;

    }
    namespace TCP 
    {
        public class Client:BasedSocket
        {
            protected TcpClient Tcp;
            protected NetworkStream ns;
            public void Init(){data = new byte[BUFFER_SIZE];}

            public bool Connect()
            {

                try
                    {
                        Tcp = new TcpClient(ip, port);
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("ConnectErrorToSever");
                        return false;
                    }

                ns = Tcp.GetStream();
                return true;
            }

            public void Read()
            {
                data = new byte[BUFFER_SIZE];
                recv = ns.Read(data, 0, data.Length);
            }
            

            public void Close()
            {
                Console.WriteLine("Disconnect");
                ns.Close();
                Tcp.Close();
            }
            public void Write(string input)
            {
                ns.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                ns.Flush();
            }

        }

        public class ThreadConnect:TCP.Client
        {
            private static int Count=0;
            public void Init(TcpListener listner)
            {
                base.Init();
                
                data = new byte[BUFFER_SIZE];

                Tcp = listner.AcceptTcpClient();
                ns  = Tcp.GetStream();

                Count ++;

                Console.WriteLine("New Client Online. :{0} Actives.", Count);

                this.Write("Welcome");
            }
            public void Run()
                {
                    while(true)
                    {
                        Read();
                        if(recv ==0)
                            break;
                        ns.Write(data,0,recv);

                    }
                    Close();
                    Count --;
                    Console.WriteLine("Disconnect. :{0} Actives.", Count);
                }
        }

        public class Sever: BasedSocket
        {
            public TcpListener client;

                public void Init()
                {
                    client = new TcpListener(port);
                }
                public void Connect()
                {
                    client.Start();
                }

        }
    }


    namespace UDP
    {
        public class Client : BasedSocket
        {
            public Socket CS;
            public EndPoint EP;

            public void Init()
            {

                data = new byte[BUFFER_SIZE];
            }

            public bool Connect()   //Connect -return True
                                            //Cannot Connect. Return False
            {
                
                CS = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram,ProtocolType.Udp);
                EP = new IPEndPoint(IPAddress.Loopback,port);

            }

            public string Read()
            {
                data = new byte[BUFFER_SIZE];
                recv = CS.ReceiveFrom(data,ref EP);
            }

        }

        public class Sever: Client
        {
            public bool Connect()
            {
                base.Connect();
                CS.Bind(EP);
                Console.WriteLine("UDP Sever On ");
                return true;

            }
        }
    }
}