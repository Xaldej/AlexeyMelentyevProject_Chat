using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project
{
    public interface IMessenger
    {
        string GetMessage();

        bool TrySendMessage(string message, string contactName);
    }
}
