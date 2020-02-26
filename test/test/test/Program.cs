using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new TcpClient())
            {
                IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                IPEndPoint endPoint = new IPEndPoint(ipAddr, 8888);

                try
                {
                    client.Connect(endPoint);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
