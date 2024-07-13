namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class WizzartsTeamInListViewModel : IMapFrom<WizzartsTeam>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

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
