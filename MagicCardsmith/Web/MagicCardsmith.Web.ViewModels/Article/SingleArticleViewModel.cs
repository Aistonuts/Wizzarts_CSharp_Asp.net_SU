namespace MagicCardsmith.Web.ViewModels.Article
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Home;

    public class SingleArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ArticleCreatorName { get; set; }

        public string ArticleCreatorAvatar { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<IndexPageArticleViewModel> Articles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, SingleArticleViewModel>()
               .ForMember(x => x.ArticleCreatorName, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.UserName))
            .ForMember(x => x.ArticleCreatorAvatar, opt =>
                   opt.MapFrom(x =>
                       x.ArticleCreator.AvatarUrl));
        }
    }
}
