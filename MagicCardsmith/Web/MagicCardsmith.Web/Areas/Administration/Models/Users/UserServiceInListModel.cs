namespace MagicCardsmith.Web.Areas.Administration.Models.Users
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class UserServiceInListModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Role { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserServiceInListModel>()
           .ForMember(x => x.AvatarUrl, opt =>
                   opt.MapFrom(x =>
                      x.AvatarUrl));
        }
    }
}
