namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class EventStatus : BaseDeletableModel<int>
    {
        [Comment("Event status.Seeded")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; }
    }
}
