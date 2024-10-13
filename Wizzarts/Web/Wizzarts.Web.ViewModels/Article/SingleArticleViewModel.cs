namespace Wizzarts.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleArticleViewModel : IndexAuthenticationViewModel, IMapFrom<Article>, IHaveCustomMappings, ISingleArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        public string ArticleCreatorName { get; set; }

        public string ArticleCreatorAvatar { get; set; }

        public string ArticleCreatorBio { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, SingleArticleViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
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
