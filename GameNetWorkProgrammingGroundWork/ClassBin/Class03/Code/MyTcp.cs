using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MyBasedSocket;



namespace MyTcp
{
    namespace Client
    {
        public class WithClass : MyBasedSocket.TCP.Client
        {
            public void Run()
            {
                string input;

                while (true)
                {
                    input = Console.ReadLine();
                    if (input == "exit")
                        break;

                    Write(input);
                    Read();
                    Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

                }
                Close();

            }
        }
    }


    namespace Sever
    {
        public class WithThread : MyBasedSocket.TCP.Sever
        {

            public void Run()
            {
                while (true)
                {
                    while (!client.Pending())
                    {
                        Thread.Sleep(1000);
                    }

                    MyBasedSocket.TCP.ThreadConnect newConnect = new MyBasedSocket.TCP.ThreadConnect();
                    newConnect.Init(this.client);

                    Thread newThrad = new Thread(new ThreadStart(newConnect.Run));
                    newThrad.Start();
                }


            }
        }
    }

}

