﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project
{
    public class AmMessenger : IMessenger
    {
        public Action<string> MessageIsGotten;

        public ClientSettings ClientSettings { get; set; }

        NetworkStream Stream { get; set; }

        public AmMessenger()
        {
        }

        public AmMessenger(ClientSettings clientSettings)
        {
            ClientSettings = clientSettings;
        }

        public void ListenMessages()
        {
        }

        public void SendMessage(string message, Guid contactId)
        {   
            byte[] data = Encoding.Unicode.GetBytes(message);
            Stream.Write(data, 0, data.Length);
        }

        public void Process()
        {
            var tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ClientSettings.EndPoint);
            }
            catch
            {
                string message = "Cannot connect to server";
                string caption = "Error";
                MessageBox.Show(message, caption);
                Application.Exit();
            }
            

            using (Stream = tcpClient.GetStream())
            {
                while(true)
                {
                    byte[] data = new byte[tcpClient.ReceiveBufferSize];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = Stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (Stream.DataAvailable);

                    var message = builder.ToString();
                    MessageIsGotten(message);
                }
                
            }

                
        }
    }
}
