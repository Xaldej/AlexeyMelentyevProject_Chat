using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
    }
}
