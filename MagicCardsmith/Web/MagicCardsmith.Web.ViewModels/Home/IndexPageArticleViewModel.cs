namespace MagicCardsmith.Web.ViewModels.Home
{
    using System;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class IndexPageArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        public string? ArtId { get; set; }

        public string ArticleCreatorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, IndexPageArticleViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName));
        }
    }

}
