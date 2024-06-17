namespace MagicCardsmith.Web.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;

    using static MagicCardsmith.Common.DataConstants;

    public class CreateEventMilestoneViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event milestone title is required!")]
        [StringLength(EventMilestoneTitleMaxLength, MinimumLength = EventMilestoneTitleMinLength, ErrorMessage = "Event milestone title  should be between 5 and 100 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event milestone description is required!")]
        [StringLength(EventMilestoneDescriptionMaxLength, MinimumLength = EventMilestoneDescriptionMinLength, ErrorMessage = "Event milestone description  should be between 10 and 1000 characters long")]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public int EventId { get; set; }

        public bool IsCompleted { get; set; }

        public bool RequireArtInput { get; set; }
    }
}
