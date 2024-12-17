namespace Wizzarts.Web.ViewModels.Event
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class EventInListViewModel : IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel, ISingleMemberViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int EventCategoryId { get; set; }

        public string Creator { get; set; } = string.Empty;

        // public string CreatorId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public bool ForMainPage { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ActionName { get; set; } = string.Empty;

        public string ControllerName { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventInListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.RemoteImageUrl))
               .ForMember(x => x.ActionName, opt =>
                  opt.MapFrom(x =>
                     x.ActionName.Name.ToString()))
                .ForMember(x => x.ControllerName, opt =>
                  opt.MapFrom(x =>
                     x.ControllerName.Name.ToString()))
            .ForMember(x => x.Creator, opt =>
                  opt.MapFrom(x =>
                     x.EventCreator.UserName.ToString()))
            .ForMember(x => x.Username, opt =>
                  opt.MapFrom(x =>
                     x.EventCreator.UserName));
        }
    }
}
