namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class EventStatus : BaseDeletableModel<int>
    {
        [Comment("Event status.Seeded")]
        public string Name { get; set; } = string.Empty;
    }
}
