using MagicCardsmith.Data.Models;
using MagicCardsmith.Web.ViewModels.Card;
using Microsoft.Extensions.Configuration;

namespace MagicCardsmith.Web.ViewModels.CardTesting
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;

    public class EventCardsInListViewModel : IMapFrom<Card>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CardExpansion { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Card, EventCardsInListViewModel>()
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                      x.CardRemoteUrl));
        }
    }
}