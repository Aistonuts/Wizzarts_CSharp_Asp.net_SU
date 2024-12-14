namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class SingleCardViewModel : IndexAuthenticationViewModel, IMapFrom<PlayCard>, IHaveCustomMappings, ISingleCardViewModel, ISingleMemberViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card Name is required!")]
        [StringLength(CardNameMaxLength, MinimumLength = CardNameMinLength, ErrorMessage = "Card name should be between 5 and 30 characters long")]
        public string Name { get; set; } = null!;

        public string CardRemoteUrl { get; set; } = string.Empty;

        public string CardType { get; set; } = string.Empty;

        public string AbilitiesAndFlavor { get; set; } = string.Empty;

        public string Power { get; set; } = string.Empty;

        public string Toughness { get; set; } = string.Empty;

        public string CardExpansion { get; set; } = string.Empty;

        public string CardRarity { get; set; } = string.Empty;

        public string ArtId { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        [Comment("Card Title this  comment is about")]
        public string CommentTitle { get; set; } = string.Empty;

        [Comment("Card description this  comment is about")]
        public string CommentDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CardReviewMaxLength, MinimumLength = CardReviewMinLength, ErrorMessage = LengthMessage)]
        public string CommentReview { get; set; } = string.Empty;

        public double AverageVote { get; set; }

        [StringLength(CardReviewSuggestionMaxLength, MinimumLength = CardReviewSuggestionMinLength, ErrorMessage = LengthMessage)]
        public string CommentSuggestions { get; set; } = string.Empty;

        public string AddedByMemberId { get; set; } = string.Empty;

        public string AddedByMember { get; set; } = string.Empty;

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
                    x.CardGameExpansion.Title))
              .ForMember(x => x.Username, opt =>
                  opt.MapFrom(x =>
                      x.AddedByMember.UserName))
              .ForMember(x => x.Nickname, opt =>
                  opt.MapFrom(x =>
                      x.AddedByMember.Nickname))
              .ForMember(x => x.AddedByMember, opt =>
                  opt.MapFrom(x =>
                      x.AddedByMember.UserName));
        }
    }
}
