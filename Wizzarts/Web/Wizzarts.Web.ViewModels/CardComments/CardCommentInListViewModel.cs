namespace Wizzarts.Web.ViewModels.CardComments
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class CardCommentInListViewModel : IMapFrom<CommentCard>, IHaveCustomMappings, ISingleMemberViewModel
    {
        public int Id { get; set; }

        [Comment("Card Title this  comment is about")]
        public string Title { get; set; } = string.Empty;

        [Comment("Card description this  comment is about")]
        public string Description { get; set; } = string.Empty;

        public string Review { get; set; }

        public string Suggestions { get; set; }

        [Comment("Card id this  comment is about")]
        public string CardId { get; set; }

        public string PostedByUserId { get; set; } = string.Empty;

        public string PostedByUser { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public bool IsAdminComment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CommentCard, CardCommentInListViewModel>()
                .ForMember(x => x.Username, opt =>
                   opt.MapFrom(x =>
                       x.PostedByUser.UserName))
                  .ForMember(x => x.Nickname, opt =>
                   opt.MapFrom(x =>
                       x.PostedByUser.Nickname))
                .ForMember(x => x.PostedByUser, opt =>
                    opt.MapFrom(x =>
                        x.PostedByUser.UserName));
        }
    }
}
