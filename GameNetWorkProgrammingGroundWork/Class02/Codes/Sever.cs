using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

class TcpEchoServer
{
    private const int BUFSIZE = 32;
    static void Main()
    {

        int SeverPort = 9000;           //Sever Port
        TcpListener listener = null;    //Craete Listner 


        try
        {
            listener = new TcpListener(IPAddress.Any, SeverPort);
            listener.Start();
            Console.WriteLine("Sever OnLine");
        }
        catch (SocketException SError)
        {
            Console.WriteLine(SError.Message);
            Environment.Exit(SError.ErrorCode);
        }


        byte[] RecvBuffer = new byte[BUFSIZE];
        int ByteRcvd;

        for (; ; )
        {
            TcpClient client = null;
            NetworkStream netStream = null;

            try
            {
                client = listener.AcceptTcpClient();
                netStream = client.GetStream();
                Console.Write("Handing Client-");

                int TotalByte = 0;
                while ((ByteRcvd = netStream.Read(RecvBuffer, 0, RecvBuffer.Length)) > 0)
                {
                    netStream.Write(RecvBuffer, 0, ByteRcvd);
                    TotalByte += ByteRcvd;
                }
                Console.WriteLine("Echo {0} bytes", TotalByte);

                netStream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                netStream.Close();
            }
        }


    }

}