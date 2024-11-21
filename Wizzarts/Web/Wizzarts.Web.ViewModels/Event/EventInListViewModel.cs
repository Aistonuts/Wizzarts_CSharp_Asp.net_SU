namespace Wizzarts.Web.ViewModels.Event
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventInListViewModel : IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty;

        public string CreatorId { get; set; } = string.Empty;

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
