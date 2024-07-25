namespace Wizzarts.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class ArticleInListViewModel : IndexAuthenticationViewModel, IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        public string ArticleCreatorName { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, ArticleInListViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName));

            // ImapFrom requires same name
            configuration.CreateMap<Article, ArticleInListViewModel>()
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                       x.ImageUrl));
        }
    }
}
