namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;

    public class CreateEventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
