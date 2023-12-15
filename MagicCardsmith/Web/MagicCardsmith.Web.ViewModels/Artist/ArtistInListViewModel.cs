namespace MagicCardsmith.Web.ViewModels.Artist
{
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class ArtistInListViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public virtual ICollection<Art> ArtPieces { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Artist, ArtistInListViewModel>()
            .ForMember(x => x.AvatarUrl, opt =>
                    opt.MapFrom(x =>
                       x.AvatarUrl));
        }
    }
}
