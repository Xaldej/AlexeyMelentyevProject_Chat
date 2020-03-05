using AlexeyMelentyevProject_ChatServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessenger
    {
        User User { get; set; }

        List<User> UserContacts { get; set; }

        TcpClient TcpClient { get; set; }

        void ListenMessages();

        void SendCommand(string command);
    }
}
