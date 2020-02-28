using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AlexeyMelentyev_chat_project
{
    public class ClientSettings
    {
        public int Port { get; set; }
        public string Ip { get; set; }

        public IPAddress IpAddr => IPAddress.Parse(Ip);
        public IPEndPoint EndPoint => new IPEndPoint(IpAddr, Port);

        ClientSettings()
        {
            Port = 8888;
            Ip = "127.0.0.1";
        }

        public ClientSettings(int port, string ip)
        {
            Port = port;
            Ip = ip;
        }
    }
}
