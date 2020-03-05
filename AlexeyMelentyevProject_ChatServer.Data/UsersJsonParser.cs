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
            var user = JsonConvert.DeserializeObject<User>(json);

            return user;
        }

        public static IEnumerable<User> JsonToManyUsers(string json)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }

        public static string ManyUsersToJson(IEnumerable<User> users)
        {
            var json = JsonConvert.SerializeObject(users);

            return json;
        }

        public static string OneUserToJson(User user)
        {
            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
