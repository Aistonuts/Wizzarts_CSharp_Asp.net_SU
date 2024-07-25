using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;
using Wizzarts.Data.Models.Enums;

namespace Wizzarts.Data.Models
{
    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Messages = new HashSet<ChatMessage>();
            this.Users = new HashSet<ChatUser>();
        }

        public string Name { get; set; }

        public string RelationKey { get; set; }

        public ChatType Type { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }

        public ICollection<ChatUser> Users { get; set; }
    }
}
