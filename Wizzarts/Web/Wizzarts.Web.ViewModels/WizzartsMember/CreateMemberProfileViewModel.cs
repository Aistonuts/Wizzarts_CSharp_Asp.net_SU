namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class CreateMemberProfileViewModel : IndexAuthenticationViewModel, IMapFrom<Avatar>, IHaveCustomMappings
    {
        public int AvatarId { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        [Comment("Information about the artist")]
        public string Bio { get; set; } = string.Empty;

        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Avatar, CreateMemberProfileViewModel>()
             .ForMember(x => x.AvatarUrl, opt =>
                opt.MapFrom(x =>
                   x.AvatarUrl));
        }
    }
}
