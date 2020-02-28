using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    class Program
    {
        const int port = 8888;

        static void Main(string[] args)
        {
            var tcpServer = new TcpServer(8888, "127.0.0.1");
            tcpServer.StartServer();
        }
    }
}
