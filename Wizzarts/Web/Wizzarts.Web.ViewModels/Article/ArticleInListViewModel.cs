namespace Wizzarts.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class ArticleInListViewModel : IndexAuthenticationViewModel, IMapFrom<Article>, IHaveCustomMappings, ISingleArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string ArticleCreatorName { get; set; } = string.Empty;

        public string ArticleCreatorId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, ArticleInListViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName))
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                       x.ImageUrl));
        }
    }
}
