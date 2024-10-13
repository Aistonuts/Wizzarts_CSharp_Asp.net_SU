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

    public class SingleCardViewModel : IndexAuthenticationViewModel, IMapFrom<PlayCard>, IHaveCustomMappings, ISingleCardViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardType { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public string CardExpansion { get; set; }

        public string CardRarity { get; set; }

        public string ArtId { get; set; }

        public string CommentTitle { get; set; }

        public string CommentDescription { get; set; }

        public string CommentReview { get; set; }

        public double AverageVote { get; set; }

        public string CommentSuggestions { get; set; }

        public string AddedByMemberId { get; set; }

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
                    x.CardType.Name));
        }
    }
}
