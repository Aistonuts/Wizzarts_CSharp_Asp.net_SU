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

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, IndexPageArticleViewModel>()
                .ForMember(x => x.Image, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/Images/images/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
