
namespace MagicCardsmith.Web.ViewModels.Card
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Mana;

    public class SingleCardViewModel : IMapFrom<Card>, IHaveCustomMappings, ISingleCardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public string ArtId { get; set; }

        public string Review { get; set; }

        public double AverageVote { get; set; }

        public string Suggestions { get; set; }

        public string PostedByUserId { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public IEnumerable<CardReviewInListViewModel> CardReviews { get; set; }

        public IEnumerable<ManaListViewModel> Mana { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Card, SingleCardViewModel>()
               .ForMember(x => x.AverageVote, opt =>
                   opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
              .ForMember(x => x.CardRemoteUrl, opt =>
                  opt.MapFrom(x =>
                     x.CardRemoteUrl))
              .ForMember(x => x.CardType, opt =>
                 opt.MapFrom(x =>
                    x.CardType.Name));
        }
    }
}
