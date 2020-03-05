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
        public static User JsonToOneUsers(string json)
        {
            return JsonConvert.DeserializeObject<User>(json);
        }

        public static IEnumerable<User> JsonToManyUsers(string json)
        {
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public static string ManyUsersToJson(IEnumerable<User> users)
        {
            return JsonConvert.SerializeObject(users);
        }

        public static string OneUserToJson(User user)
        {
            return JsonConvert.SerializeObject(user);
        }
    }
}
