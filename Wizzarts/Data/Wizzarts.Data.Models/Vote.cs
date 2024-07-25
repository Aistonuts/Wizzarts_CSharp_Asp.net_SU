namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        [Comment("Vote added to.")]
        public string CardId { get; set; }

        public virtual PlayCard Card { get; set; }

        [Comment("Vote casted by.")]
        public string AddedByMemberId { get; set; } = string.Empty;

        [ForeignKey(nameof(AddedByMemberId))]
        public virtual ApplicationUser AddedByMember { get; set; }

        [Comment("Vote value")]
        public byte Value { get; set; }
    }
}
