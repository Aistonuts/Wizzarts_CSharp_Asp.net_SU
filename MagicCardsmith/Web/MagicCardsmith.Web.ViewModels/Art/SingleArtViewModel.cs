namespace MagicCardsmith.Web.ViewModels.Art
{
    using System;
    using System.Linq;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Art;

    public class SingleArtViewModel : IMapFrom<Art>, IHaveCustomMappings, ISingleArtViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string ArtistName { get; set; }

        public string ArtistBio { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, SingleArtViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                       x.RemoteImageUrl))
                .ForMember(x => x.ArtistName, opt =>
                    opt.MapFrom(x => x.Artist.Nickname))
                .ForMember(x => x.ArtistBio, opt =>
                    opt.MapFrom(x => x.Artist.Bio));
        }
    }
}
