using System; 
using System.Net; 
using System.Net.Sockets;

namespace _AddressTest
{   class IPAddressExample
    {

        String LocalHostName;
        
        public IPAddressExample()
        {
            LocalHostName = Dns.GetHostName();
        }
        public void PrintHostInfo(String host)
        {
            try
            {
                IPHostEntry hostInfo;
                hostInfo = Dns.GetHostEntry(host);
                Console.WriteLine("\t Name: " + hostInfo.HostName);
                Console.Write("\t Adresss: ");

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                Console.Write(ipaddr.ToString() + " ");
                }
                Console.WriteLine("\n");

                Console.Write("\t Aliases: ");

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                Console.Write(ipaddr.ToString() + " ");
                }
                Console.WriteLine("\n");
            }
            catch(Exception)
            {
                Console.WriteLine("\t Unable Host: "+ host+ "\n");
            }
        }
    }

}