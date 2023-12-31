namespace MagicCardsmith.Web.ViewModels.Event
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using System;

    public class EventInListViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string EventCreatorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
