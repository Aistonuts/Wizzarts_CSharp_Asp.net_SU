namespace MagicCardsHub.Web.ViewModels.Article
{
    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class ArticleInListViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ArticleArt { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, ArticleInListViewModel>()
                .ForMember(x => x.ArticleArt, opt =>
                    opt.MapFrom(x =>
                        x.Art.RemoteImageUrl != null ?
                        x.Art.RemoteImageUrl :
                        "/Images/art/" + x.Art.Id + "." + x.Art.Extension));

        }
    }
}
