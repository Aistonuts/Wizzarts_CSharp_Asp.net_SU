namespace Wizzarts.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class ArticleInListViewModel : IndexAuthenticationViewModel, IMapFrom<Article>, IHaveCustomMappings, ISingleArticleViewModel, ISingleMemberViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string ArticleCreatorName { get; set; } = string.Empty;

        // public string ArticleCreatorId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, ArticleInListViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName))
               .ForMember(x => x.Nickname, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.Nickname))
               .ForMember(x => x.Username, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName))
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                       x.ImageUrl));
        }
    }
}
