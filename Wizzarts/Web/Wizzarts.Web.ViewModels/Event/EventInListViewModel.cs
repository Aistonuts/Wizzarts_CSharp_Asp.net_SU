namespace Wizzarts.Web.ViewModels.Event
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventInListViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }

        public string Creator { get; set; }

        public string CreatorId { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventInListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.RemoteImageUrl))
            .ForMember(x => x.Creator, opt =>
                  opt.MapFrom(x =>
                     x.EventCreator.UserName.ToString()))
            .ForMember(x => x.CreatorId, opt =>
                  opt.MapFrom(x =>
                     x.EventCreator.Id));
        }
    }
}
