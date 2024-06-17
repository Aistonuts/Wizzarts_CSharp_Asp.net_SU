namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static MagicCardsmith.Common.DataConstants;

    public class CreateEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event title is required!")]
        [StringLength(EventTitleMaxLength, MinimumLength = EventTitleMinLength, ErrorMessage = "Event title should be between 5 and 30 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event description is required!")]
        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = "Event description should be between 10 and 1000 characters long")]
        public string EventDescription { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        public int StatusId { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<EventStatusInListViewModel> EventStatuses { get; set; }
    }
}
