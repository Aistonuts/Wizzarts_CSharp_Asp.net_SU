using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
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
