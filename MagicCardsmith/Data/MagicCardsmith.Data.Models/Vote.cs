namespace MagicCardsmith.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class Vote : BaseModel<int>
    {
        [Comment("Vote added to.")]
        public int CardId { get; set; }

        public virtual Card Card { get; set; }

        [Comment("Vote casted by.")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        [Comment("Vote value")]
        public byte Value { get; set; }
    }
}
