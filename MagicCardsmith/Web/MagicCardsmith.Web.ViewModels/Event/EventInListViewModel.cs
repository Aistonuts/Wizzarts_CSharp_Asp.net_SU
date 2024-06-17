namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class EventInListViewModel : IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }

        public string CreatorId { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventInListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.RemoteImageUrl));
        }
    }
}
