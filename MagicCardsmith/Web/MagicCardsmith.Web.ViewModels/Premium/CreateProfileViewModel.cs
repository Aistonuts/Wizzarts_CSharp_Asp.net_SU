namespace MagicCardsmith.Web.ViewModels.Premium
{
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class CreateProfileViewModel : IMapFrom<Avatar>, IHaveCustomMappings
    {
        public int AvatarId { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Avatar, CreateProfileViewModel>()
             .ForMember(x => x.AvatarUrl, opt =>
                opt.MapFrom(x =>
                   x.AvatarUrl));
        }
    }
}
