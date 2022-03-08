

using System;
using System.Net;



class CIPAdress// IP Adress Class
{
    IPAddress    m_IPParse;
    IPAddress    m_IPLoopBack;
    IPAddress    m_IPBroadCast;
    IPAddress    m_IPAny;
    IPAddress    m_IPNone;
    IPAddress    m_IPMyAdress;

    IPHostEntry  m_IPHostEntry;

    public CIPAdress()
    {
        m_IPParse       = IPAddress.Parse("192.168.1.1");
        m_IPLoopBack    = IPAddress.Loopback;
        m_IPBroadCast   = IPAddress.Broadcast;
        m_IPAny         = IPAddress.Any;
        m_IPNone        = IPAddress.None;

        m_IPHostEntry   = Dns.GetHostEntry(Dns.GetHostName());
        m_IPMyAdress    = m_IPHostEntry.AddressList[0];

    }

    public void Run()
    {
        if (IPAddress.IsLoopback(IPAddress.Loopback))
        {
            Console.WriteLine("The LoopBak IP:{0}\n",
            m_IPLoopBack.ToString());
        }

        else
        {
            Console.WriteLine("Error to loopBack adress\n");
        }


        if (m_IPMyAdress == m_IPLoopBack)
        {
            Console.WriteLine("Loopback adress is The Same as Local Adress\n");
        }
        else
        {
            Console.WriteLine("Loopback adress is not same local adress\n ");
        }

        //Write Class's IP

        Console.WriteLine("Parse adress : {0}"      , m_IPNone.ToString());
        Console.WriteLine("Broadcast adress : {0}"  , m_IPNone.ToString());
        Console.WriteLine("ANY address : {0}"       , m_IPNone.ToString());
        Console.WriteLine("None adrdress : {0}"     , m_IPNone.ToString());
    }
}
