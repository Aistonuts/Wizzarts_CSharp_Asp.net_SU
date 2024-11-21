namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class WizzartsTeamInListViewModel : IMapFrom<WizzartsTeam>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        // User Controls in View Component
        public int CountOfArts { get; set; }

        public int CountOfArticles { get; set; }

        public int CountOfEvents { get; set; }

        public int CountOfCards { get; set; }

        // End of User Controls in View Component
        public virtual ICollection<Art> ArtPieces { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WizzartsTeam, WizzartsTeamInListViewModel>()
            .ForMember(x => x.AvatarUrl, opt =>
                    opt.MapFrom(x =>
                       x.AvatarUrl));
        }
    }
}
