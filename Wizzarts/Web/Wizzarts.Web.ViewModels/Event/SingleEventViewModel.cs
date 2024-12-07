namespace Wizzarts.Web.ViewModels.Event
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class SingleEventViewModel : BaseEventViewModel, IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel
    {
        public int EventId { get; set; }

        public string CreatorId { get; set; } = string.Empty;

        public string EventCreator { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool ForMainPage { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public string ActionName { get; set; } = string.Empty;

        public string ControllerName { get; set; } = string.Empty;

        public IEnumerable<CardInListViewModel> CardsFromEvent { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventViewModel>()
             .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                    x.RemoteImageUrl))
              .ForMember(x => x.ActionName, opt =>
                  opt.MapFrom(x =>
                     x.ActionName.Name.ToString()))
                .ForMember(x => x.ControllerName, opt =>
                  opt.MapFrom(x =>
                     x.ControllerName.Name.ToString()));
        }
    }
}
