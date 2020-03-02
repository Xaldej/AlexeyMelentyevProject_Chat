using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data.Entities
{
    public class ContactRelationship : BaseEntity
    {
        public User User { get; set; }

        public Guid UserId { get; set; }
        
        public User Contact { get; set; }

        public Guid ContactId { get; set; }
    }
}
