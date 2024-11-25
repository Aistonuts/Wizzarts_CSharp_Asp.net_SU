namespace Wizzarts.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class SingleArticleViewModel : IndexAuthenticationViewModel, IMapFrom<Article>, IHaveCustomMappings, ISingleArticleViewModel, ISingleMemberViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string ArticleCreatorId { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; }

        public string ArticleCreatorAvatar { get; set; } = string.Empty;

        public string ArticleCreatorBio { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, SingleArticleViewModel>()
               .ForMember(x => x.Nickname, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.Nickname))
               .ForMember(x => x.Username, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName))
            .ForMember(x => x.ArticleCreatorAvatar, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.AvatarUrl))
            .ForMember(x => x.ArticleCreatorBio, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.Bio));
        }
    }
}
