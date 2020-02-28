using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project
{
    public class AmMessenger : IMessenger
    {
        public Action<string> MessageIsGotten;

        public ClientSettings ClientSettings { get; set; }

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

        public void SendMessage(string message1, string contactName)
        {

        }

        public void Process()
        {
            var client = new TcpClient();
            client.Connect(ClientSettings.EndPoint);

            using (var stream = client.GetStream())
            {
                string message = "test request\n";
                byte[] data = Encoding.Unicode.GetBytes(message);

                while(true)
                {
                    stream.Write(data, 0, data.Length);

                    data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    MessageIsGotten(message);
                }
                
            }

                
        }
    }
}
