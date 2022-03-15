using System;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace MySocket
{
    public class Socket
    {

        public const int BUFFER_SIZE = 1024;

        public string ip;
        public int port;

        protected byte[] data;
        protected int recv;
        protected NetworkStream ns;


    }

    namespace TCP
    {
        public class Client : Socket
        {
            TcpClient client;

            public void Init()
            {

                data = new byte[BUFFER_SIZE];

            }

            public bool Connect()   //Connect -return True
                                            //Cannot Connect. Return False
            {
                try
                {
                    client = new TcpClient(ip, port);
                    return true;
                }
                catch (SocketException)
                {
                    Console.WriteLine("ConnectErrorToSever");
                    return false;
                }

                ns = client.GetStream();

            }

            public string Read_to_string()
            {
                data = new byte[BUFFER_SIZE];
                recv = ns.Read(data, 0, data.Length);
                return Encoding.ASCII.GetString(data, 0, recv);
            }
            public void Read_Data()
            {
                data = new byte[BUFFER_SIZE];
                recv = ns.Read(data, 0, data.Length);
                
            }
            public void Write(string input)
            {
                ns.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                ns.Flush();
            }

            public void Close()
            {
                Console.WriteLine("Disconnect");
                ns.Close();
                client.Close();
            }


        }
    

        public class ThreadConnect:Client
        {
            private static int Count=0;
            
            public void Init(TcpListener listner)
            {
                data = new byte[BUFFER_SIZE];

                client = listner.AcceptTcpClient();
                ns  = client.GetStream();

                Count ++;

                Console.WriteLine("New Client Online. :{0} Actives.", Count);

                this.Write("Welcome");
            }

            public void Run()
            {
                while(true)
                {
                    Read_Data();
                    if(recv ==0)
                        break;
                    ns.Write(data,0,recv);

                }
                Close();
                Count --;
                Console.WriteLine("Disconnect. :{0} Actives.", Count);
            }

        }

        public class Sever : Socket
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


}