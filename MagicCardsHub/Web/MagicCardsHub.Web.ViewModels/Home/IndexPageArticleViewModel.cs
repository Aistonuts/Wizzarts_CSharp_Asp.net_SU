namespace MagicCardsHub.Web.ViewModels.Home
{
    using System.Linq;

    using AutoMapper;

    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class IndexPageArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, IndexPageArticleViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Art.RemoteImageUrl != null ?
                        x.Art.RemoteImageUrl :
                        "/Images/art/" + x.Art.Id + "." + x.Art.Extension));
        }
    }
}
