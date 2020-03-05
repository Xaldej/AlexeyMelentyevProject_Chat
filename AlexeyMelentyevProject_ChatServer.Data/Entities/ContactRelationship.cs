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
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ContactId { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
