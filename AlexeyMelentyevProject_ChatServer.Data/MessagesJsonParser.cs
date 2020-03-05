using AlexeyMelentyevProject_ChatServer.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data
{
    public static class MessagesJsonParser
    {
        public static MessageToUser JsonToOneMessage(string json)
        {
            return JsonConvert.DeserializeObject<MessageToUser>(json);
        }

        public static IEnumerable<MessageToUser> JsonToManyMessages(string json)
        {
            return JsonConvert.DeserializeObject<List<MessageToUser>>(json);
        }

        public static string ManyMessagesToJson(List<MessageToUser> messages)
        {
            return JsonConvert.SerializeObject(messages);
        }

        public static string OneMessageToJson(MessageToUser message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}