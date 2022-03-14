using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
class TcpEchoClient
{
    static void Main(string[] args)
    {
        String server = "127.0.0.1"; 
        int servPort = 9000;
        byte[] byteBuffer = Encoding.ASCII.GetBytes("Connect");
        TcpClient client = null;
        NetworkStream netStream = null;
        try
        {
            client = new TcpClient(server, servPort);

            Console.WriteLine("Connected");
            netStream = client.GetStream();
            netStream.Write(byteBuffer, 0, byteBuffer.Length);
            Console.WriteLine("Sent{0}bytest", byteBuffer.Length);

            int totalBytesRcvd = 0;
            int bytesRcvd = 0;
            while (totalBytesRcvd < byteBuffer.Length)
            {
                if ((
                bytesRcvd = netStream.Read(byteBuffer, totalBytesRcvd,
                byteBuffer.Length - totalBytesRcvd)) == 0)
                {
                    Console.WriteLine("Connectionclosedprematurely.");
                    break;
                }
                totalBytesRcvd += bytesRcvd;
            }
            Console.WriteLine("Recv {0} server:{1}", totalBytesRcvd, Encoding.ASCII.GetString(byteBuffer, 0, totalBytesRcvd));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally { netStream.Close(); client.Close(); }
    }
}