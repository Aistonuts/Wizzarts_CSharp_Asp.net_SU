namespace Wizzarts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Wizzarts.Data.Common.Models;
    using Wizzarts.Data.Models.Enums;

    using static Wizzarts.Common.DataConstants;

    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Messages = new HashSet<ChatMessage>();
            this.Users = new HashSet<ChatUser>();
        }

        [Required]
        [MaxLength(ChatNameMaXLength)]
        public string Name { get; set; }

        [MaxLength(ChatRelationKeyMaxLength)]
        public string RelationKey { get; set; }

        public ChatType Type { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }

        public virtual ICollection<ChatUser> Users { get; set; }
    }
}
