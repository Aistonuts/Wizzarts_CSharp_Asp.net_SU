using System.ComponentModel.DataAnnotations;

namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data.Common.Models;

    using static Wizzarts.Common.DataConstants;

    public class ChatMessage : BaseDeletableModel<int>
    {
        [MaxLength(ChatNameMaXLength)]
        public string Name { get; set; }

        [MaxLength(ChatMessageMaxLength)]
        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }

        [ForeignKey(nameof(ChatId))]
        public Chat Chat { get; set; }
    }
}
