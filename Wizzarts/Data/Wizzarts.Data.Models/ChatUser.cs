using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Data.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
