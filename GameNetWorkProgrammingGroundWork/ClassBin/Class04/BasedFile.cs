/*
 * UDP/TCP �� ����/Ŭ���̾�Ʈ��
 * �̸� ĸ��ȭ ���ѵ� �����Դϴ�.
 */


using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;


namespace MySocket
{
    public class NetWorkInfo
    //������ ������ ��� Ŭ�������� NetWorkInfo ����.
    //NetworkSocket�� ���� Ŭ������ �ϳ��̸�, ���� ��ӹ����� �ʽ��ϴ�.
    {
        public string ip;
        public int port;
        public int BUFFER_SIZE;
        public string EncodeType;
    }

    public abstract class NetWorkBasedSocket
    //NetWorkBasedSocket-�� ��쿡�� ��� Ŭ������ ���̽� Ŭ�����̸�,
    //���������� TCPŬ������ ��ӹ޽��ϴ�.
    {
        public NetWorkInfo info;
        //����� Info�������̸�. Info.ip���·� �������� ���� �����Ͽ����ϴ�.

        protected int recv;
        protected byte[] data;

        protected Encoding encoder;

        public abstract bool Init();
        //�⺻���� ������ ����ϴ� �ʱ�ȭ �Լ��Դϴ�. ��� ��Ʈ��ũ ����ü�� �ʿ��մϴ�.
        public abstract void Close();
        //������ ���Ḧ ����ϴ� �ʱ�ȭ �Լ��Դϴ�. ��� ��Ʈ��ũ ����ü�� �ʿ��մϴ�.

        public string ByteTostring(byte[] mydata = null)
        {
            return encoder.GetString(mydata, 0, recv);
        }

        public byte[] StringToByte(string input)
        {
            return encoder.GetBytes(input);
        }

        //�Ź� Encoding.GetString Ȥ�� GetBytes�� �ݺ���� ��� ���� ���� �޼����Դϴ�.

        public abstract void Write(string input);
        public abstract string Read();
    }
    public abstract class TCPBased : NetWorkBasedSocket
    {
        protected TcpClient tcp;
        protected NetworkStream ns;




        public override bool Init()
        {

            encoder = Encoding.ASCII;

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
            encoder = Encoding.UTF8;

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



