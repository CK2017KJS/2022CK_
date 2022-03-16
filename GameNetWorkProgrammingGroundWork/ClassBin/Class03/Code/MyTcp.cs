using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MySocket;




namespace MyTcp
{
    namespace Client
    {
        public class WithClass: MySocket.TCP.Client
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
                    Console.WriteLine(Read_to_string());

                }
                Close();

            }
        }
    }


    namespace Sever{
    public class WithThread:MySocket.TCP.Sever
    {

            void Run()
            {
                while(true)
                {
                    while(!client.Pending())
                    {
                        Thread.Sleep(1000);
                    }    
                }

                MySocket.TCP.ThreadConnect newConnect = new MySocket.TCP.ThreadConnect();
                newConnect.Init(this.client);

                Thread newThrad = new Thread(new ThreadStart(newConnect.Run));
                newThrad.Start();
            }
        }
    }

}