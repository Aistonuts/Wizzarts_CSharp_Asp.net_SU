namespace MagicCardsHub.Web.ViewModels.Art
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;
    using MagicCardsHub.Web.ViewModels.Art;

    public class SingleArtViewModel : IMapFrom<Art>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string AddedByArtistName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, SingleArtViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.RemoteImageUrl != null ?
                        x.RemoteImageUrl :
                        "/Images/art/" + x.Id + "." + x.Extension));
        }
    }
}
