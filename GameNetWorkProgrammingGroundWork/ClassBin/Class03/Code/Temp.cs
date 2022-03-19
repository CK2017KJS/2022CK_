/*
 * UDP/TCP 의 서버/클라이언트를
 * 미리 캡슐화 시켜둔 파일입니다.
 */


using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;


namespace MySocket
{
    public class NetWorkInfo
    //앞으로 생성될 모든 클래스들의 NetWorkInfo 설정.
    //이를 기반으로 네트워크 소켓이 만들어집니다.
    {
        public string ip;
        public int port;
        public int BUFFER_SIZE;
        public string EncodeType;
    }

    public abstract class NetWorkBasedSocket
    {
        public NetWorkInfo info;
        protected int recv;
        protected byte[] data;

        public abstract bool Init();
        //기본적인 연결을 담당하는 초기화 함수
        public abstract void Close();
        //소켓의 종료를 담당하는 함수

        public string ByteTostring(byte[] mydata = null)
        {
            if (mydata == null)
            {
                mydata = this.data;
            }
            if (info.EncodeType == "UTF-8")
            {
                return Encoding.UTF8.GetString(mydata, 0, recv);
            }
            else if
                (info.EncodeType == "ASCII")
            {
                return Encoding.ASCII.GetString(mydata, 0, recv);
            }
            else
            {
                return "Encoder is NONE";
            }

        }

        public byte[] StringToByte(string input)
        {
            if (info.EncodeType == "UTF-8")
            {
                return Encoding.UTF8.GetBytes(input);
            }
            else if
                (info.EncodeType == "ASCII")
            {
                return Encoding.ASCII.GetBytes(input);
            }
            else
            {
                Console.WriteLine("Encoder is NONE");
                return null;
            }

        }
        public abstract void Write(string input);
        public abstract string Read();
    }
    public abstract class TCPBased : NetWorkBasedSocket
    {
        protected TcpClient tcp;
        protected NetworkStream ns;

        public override bool Init()
        {
            try
            {
                tcp = new TcpClient(info.ip, info.port);
            }
            catch (SocketException)
            {
                Console.WriteLine("ConnectErrorToSever");
                return false;
            }
            ns = tcp.GetStream();

            return true;
        }
        public override void Close()
        {
            Console.WriteLine("Disconnect");
            ns.Close();
            tcp.Close();
        }


    }
    public abstract class UDPBased: NetWorkBasedSocket
    {
        public Socket CS;
        public EndPoint EP;
        public override bool Init()
        {
            data = new byte[info.BUFFER_SIZE];

            CS = new Socket(AddressFamily.InterNetwork
                , SocketType.Dgram, ProtocolType.Udp);
            EP = new IPEndPoint(IPAddress.Loopback, info.port);

            return true;
        }
        public override void Close()
        {
            Console.WriteLine("Disconnect");
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
                return ByteTostring();
            }
            public override void Write(string input)
            {
                ns.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                ns.Flush();
            }
        }


        public class ThreadConnect : Client
        {
            private static int Count = 0;
            public static TcpListener listner;

            public override bool Init()
            {

                tcp = listner.AcceptTcpClient();
                ns = tcp.GetStream();
                Console.WriteLine("New Client Online. :{0} Actives.", Count);

                return true;
            }

            public void Run()
            {
                while (true)
                {
                    Read();
                    if (recv == 0)
                        break;
                    ns.Write(data, 0, recv);

                }
                Close();
                Count--;
                Console.WriteLine("Disconnect. :{0} Actives.", Count);
            }
        }

        public class Sever: Client
        {
            public TcpListener client;

            public override bool Init()
            {
                client = new TcpListener(info.port);
                client.Start();
                return true;
            }
        }
    }

    namespace UDP
    {
        public class Sever: UDPBased
        {
            public override bool Init()
            {
                base.Init();
                Console.WriteLine("UDP Sever is ON");
                return true;
            }
        }
    }
}



