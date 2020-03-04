using AlexeyMelentyevProject_ChatServer.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data
{
    public static class UsersJsonParser
    {
        public static IEnumerable<Guid> JsonToUsersId(string json)
        {
            var guid = JsonConvert.DeserializeObject<List<Guid>>(json);

            return guid;
        }

        public static string UsersIdToJson(IEnumerable<Guid> users)
        {
            var json = JsonConvert.SerializeObject(users);

            return json;
        }
    }
}
