namespace Wizzarts.Web.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;

    public class BaseEventViewModel : IndexAuthenticationViewModel
    {
        public const int StatusId = 3;

        [MaxLength(EventTitleMaxLength)]
        [Comment("Event Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        [Comment("Event Description")]
        public string EventDescription { get; set; } = string.Empty;

        public int EventStatusId { get; set; } = StatusId;
    }
}
