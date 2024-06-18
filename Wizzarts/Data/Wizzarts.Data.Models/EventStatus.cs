namespace Wizzarts.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class EventStatus : BaseDeletableModel<int>
    {
        [Comment("Event status.Seeded")]
        public string Name { get; set; } = string.Empty;
    }
}
