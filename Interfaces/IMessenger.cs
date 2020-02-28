using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessenger
    {
        string GetMessage();

        void SendMessage(string message, string contactName);
    }
}
