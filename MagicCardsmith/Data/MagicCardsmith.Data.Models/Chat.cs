using MagicCardsmith.Data.Common.Models;
using MagicCardsmith.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Data.Models
{
    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Messages = new HashSet<Message>();
            this.Users = new HashSet<ChatUser>();
        }

        public string Name { get; set; }

        public ChatType Type { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> Users { get; set; }
    }
}
