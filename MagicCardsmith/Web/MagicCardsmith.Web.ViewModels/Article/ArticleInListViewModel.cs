namespace MagicCardsmith.Web.ViewModels.Article
{
    using System;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Home;

    public class ArticleInListViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        public string? ArtId { get; set; }

        public string ArticleCreatorName { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, IndexPageArticleViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName));

            // ImapFrom requires same name
            configuration.CreateMap<Article, IndexPageArticleViewModel>()
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                       x.ImageUrl));
        }
    }
}
