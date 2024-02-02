
namespace MagicCardsmith.Web.ViewModels.Card
{
    using System.Collections.Generic;
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    using MagicCardsmith.Web.ViewModels.Mana;

    public class SingleCardViewModel : IMapFrom<Card>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public string ArtId { get; set; }

        public IEnumerable<ManaListViewModel> Mana { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Card, SingleCardViewModel>()
              .ForMember(x => x.CardRemoteUrl, opt =>
                  opt.MapFrom(x =>
                     x.CardRemoteUrl));
        }
    }
}
