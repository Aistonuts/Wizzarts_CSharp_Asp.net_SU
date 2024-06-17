namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        [Comment("Vote added to.")]
        public int CardId { get; set; }

        public virtual PlayCard Card { get; set; }

        [Comment("Vote casted by.")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual WizzartsMember User { get; set; }

        [Comment("Vote value")]
        public byte Value { get; set; }
    }
}
