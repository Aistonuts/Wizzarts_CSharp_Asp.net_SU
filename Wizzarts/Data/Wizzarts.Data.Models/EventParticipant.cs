namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Wizzarts.Data.Common.Models;

    public class EventParticipant : BaseDeletableModel<int>
    {
        public int EventId { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
