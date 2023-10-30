namespace MagicCardsHub.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class CreateArticleInputModel : BaseCreateArticleInputModel, IMapFrom<Article>, IHaveCustomMappings
    {
        [MinLength(3)]
        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, CreateArticleInputModel>()

                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.RemoteImageUrl != null ?
                        x.RemoteImageUrl :
                        "/Images/art/" + x.Art.Id + "." + x.Art.Extension));
        }
    }
}
