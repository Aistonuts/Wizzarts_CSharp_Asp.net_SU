namespace Wizzarts.Web.ViewModels.Event
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class SingleEventViewModel : IndexAuthenticationViewModel, IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel
    {
        public int EventId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public string CreatorId { get; set; } = string.Empty;

        public string EventCreator { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public IEnumerable<CardInListViewModel> CardsFromEvent { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventViewModel>()
             .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                    x.RemoteImageUrl));
        }
    }
}
