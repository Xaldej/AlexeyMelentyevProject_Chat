﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Data.Entities
{
    public class MessageToUser
    {
        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public string Text { get; set; }
    }
}
