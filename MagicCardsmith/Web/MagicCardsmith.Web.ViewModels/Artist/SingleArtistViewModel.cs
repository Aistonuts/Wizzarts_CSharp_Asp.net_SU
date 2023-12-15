namespace MagicCardsmith.Web.ViewModels.Artist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Art;

    public class SingleArtistViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public IEnumerable<ArtListViewModel> Arts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {

                configuration.CreateMap<Artist, ArtistInListViewModel>()
                .ForMember(x => x.AvatarUrl, opt =>
                        opt.MapFrom(x =>
                           x.AvatarUrl));
        }
    }
}
