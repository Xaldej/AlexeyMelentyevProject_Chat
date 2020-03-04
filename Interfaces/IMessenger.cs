﻿using AlexeyMelentyevProject_ChatServer.Data.Entities;
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

        TcpClient TcpClient { get; set; }

        void ListenMessages();

        void SendMessage(string message, Guid contactId);

        void SendCommand(string command);
    }
}
