using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
namespace MySocket
{
    public class NetWorkInfo
    {
        public string ip;
        public int port;
        public int BUFFER_SIZE;
    }
    public abstract class NetWorkBasedSocket
    {
        public NetWorkInfo info =new NetWorkInfo();

        protected int recv;
        protected byte[] data;

        protected Encoding encoder;

        public abstract void Close();

        public string ByteTostring(byte[] mydata = null)
        {
            return encoder.GetString(mydata, 0, recv);
        }

        public byte[] StringToByte(string input)
        {
            return encoder.GetBytes(input);
        }
        public abstract void Write(string input);
        public abstract string Read();
    }
    public abstract class TCPBased : NetWorkBasedSocket
    {
        protected TcpClient tcp;
        protected NetworkStream ns;

        public TCPBased() : base()
        {
            encoder = Encoding.ASCII;
            try
            {
                tcp = new TcpClient(info.ip, info.port);
            }
            catch (SocketException)
            {
            }
            ns = tcp.GetStream();

        }
        public override void Close()
        {
            ns.Close();
            tcp.Close();
        }


    }
    public abstract class UDPBased : NetWorkBasedSocket
    {
        public Socket CS;
        public EndPoint EP;
        public UDPBased() : base()
        {
            encoder = Encoding.UTF8;

            data = new byte[info.BUFFER_SIZE];

            CS = new Socket(AddressFamily.InterNetwork
                , SocketType.Dgram, ProtocolType.Udp);
            EP = new IPEndPoint(IPAddress.Loopback, info.port);

        }
        
        public override void Close()
        {
            CS.Close();
        }
        public override string Read()
        {
            data = new byte[info.BUFFER_SIZE];
            recv = CS.ReceiveFrom(data, ref EP);
            return ByteTostring();
        }
        public override void Write(string input)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(input);
            CS.SendTo(StringToByte(input), EP);
        }
    }

    namespace TCP
    {
        public class Client : TCPBased
        {
            public override string Read()
            {
                data = new byte[info.BUFFER_SIZE];
                recv = ns.Read(data, 0, data.Length);
                return ByteTostring(data);
            }
            public override void Write(string input)
            {
                ns.Write(StringToByte(input), 0, input.Length);
                ns.Flush();
            }
        }
        public class Sever : Client
        {
            public TcpListener client;
            public Sever() : base()
            {
                client = new TcpListener(info.port);
                client.Start();
            }
        }
    }

    namespace UDP
    {
        public class Sever : UDPBased
        {
            public Sever():base()
            {

            }
            
        }
    }
}
