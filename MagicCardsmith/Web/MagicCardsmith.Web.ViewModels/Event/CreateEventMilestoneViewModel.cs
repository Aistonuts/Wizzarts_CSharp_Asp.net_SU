namespace MagicCardsmith.Web.ViewModels.Event
{
    public class CreateEventMilestoneViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public int EventId { get; set; }

        public bool IsCompleted { get; set; }

        public bool RequireArtInput { get; set; }
    }
}
