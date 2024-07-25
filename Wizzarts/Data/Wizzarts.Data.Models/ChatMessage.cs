using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
    public class ChatMessage : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
