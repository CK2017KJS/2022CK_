using System;
using System.Threading;
using MySocket;

namespace TcpSever
{
    class WithThread:MySocket.TCP.Sever
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