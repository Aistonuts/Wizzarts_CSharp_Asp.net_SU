namespace Wizzarts.Web.ViewModels.Event
{
    using AutoMapper;
    using System.Collections.Generic;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Card;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleEventViewModel : IndexAuthenticationViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string CreatorId { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }


        public IEnumerable<EventComponentsInListViewModel> EventComponents { get; set; }

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
