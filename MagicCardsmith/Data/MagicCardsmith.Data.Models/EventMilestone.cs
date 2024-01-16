namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class EventMilestone : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Instructions { get; set; }

        public string? ImageUrl { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public bool IsCompleted { get; set; }
    }
}
