namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class EventStatus : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        [Comment("Event status.Seeded")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; }
    }
}
