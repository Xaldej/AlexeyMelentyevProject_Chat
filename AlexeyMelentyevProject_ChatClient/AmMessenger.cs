using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project
{
    public class AmMessenger : IMessenger
    {
        public Action<string> MessageIsGotten;

        public AmMessenger()
        {
        }

        public string GetMessage()
        {
            string message;

            //TO DO: implement

            //FOR TEST ONLY
            message = "test";
            MessageIsGotten(message);
            ///////////////

            return message;
        }

        public void SendMessage(string message, string contactName)
        {   
            // TO DO: implement
        }
    }
}
