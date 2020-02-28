using AlexeyMelentyevProject_ChatServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    public class Client
    {
        public User User { get; set; }

        public TcpClient TcpClient { get; set; }

        public Client()
        {

        }

        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        public void InitiateProcess()
        {
            LoginUser();

            Process();
        }

        private void Process()
        {
            string response = "Hellow World!";

            byte[] responseData = Encoding.UTF8.GetBytes(response);


            using (TcpClient)
            {
                using (TcpClient)
                {
                    using (NetworkStream stream = TcpClient.GetStream())
                    {

                        stream.Write(responseData, 0, responseData.Length);

                        Console.WriteLine("Response is send}");
                    }
                }
            }
            
        }

        private void LoginUser()
        {
            // TO DO
        }
    }
}
