using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public List<User> Contacts;
    }
}
