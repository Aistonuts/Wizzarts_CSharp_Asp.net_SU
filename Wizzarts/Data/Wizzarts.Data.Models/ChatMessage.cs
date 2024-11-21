namespace Wizzarts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
