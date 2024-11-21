namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.CardComments;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public class SingleCardViewModel : IndexAuthenticationViewModel, IMapFrom<PlayCard>, IHaveCustomMappings
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string CardRemoteUrl { get; set; } = string.Empty;

        public string CardType { get; set; } = string.Empty;

        public string AbilitiesAndFlavor { get; set; } = string.Empty;

        public string Power { get; set; } = string.Empty;

        public string Toughness { get; set; } = string.Empty;

        public string CardExpansion { get; set; } = string.Empty;

        public string CardRarity { get; set; } = string.Empty;

        public string ArtId { get; set; } = string.Empty;

        public string CommentTitle { get; set; } = string.Empty;

        public string CommentDescription { get; set; } = string.Empty;

        public string CommentReview { get; set; } = string.Empty;

        public double AverageVote { get; set; }

        public string CommentSuggestions { get; set; } = string.Empty;

        public string AddedByMemberId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PlayCard, SingleCardViewModel>()
               .ForMember(x => x.AverageVote, opt =>
                   opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
              .ForMember(x => x.CardRemoteUrl, opt =>
                  opt.MapFrom(x =>
                     x.CardRemoteUrl))
              .ForMember(x => x.CardType, opt =>
                 opt.MapFrom(x =>
                    x.CardType.Name))
              .ForMember(x => x.CardExpansion, opt =>
                 opt.MapFrom(x =>
                    x.CardGameExpansion.Title));
        }
    }
}
