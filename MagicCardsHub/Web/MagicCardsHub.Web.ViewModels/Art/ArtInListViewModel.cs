namespace MagicCardsHub.Web.ViewModels.Art
{
    using System.Linq;

    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class ArtInListViewModel : IMapFrom<Art>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, ArtInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.RemoteImageUrl != null ?
                        x.RemoteImageUrl :
                        "/Images/art/" + x.Id + "." + x.Extension));
        }
    }
}
