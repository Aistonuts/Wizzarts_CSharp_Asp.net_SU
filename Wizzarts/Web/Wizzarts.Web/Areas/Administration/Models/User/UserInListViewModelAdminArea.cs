namespace Wizzarts.Web.Areas.Administration.Models.User
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class UserInListViewModelAdminArea : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserInListViewModelAdminArea>()
           .ForMember(x => x.AvatarUrl, opt =>
                   opt.MapFrom(x =>
                      x.AvatarUrl));
        }
    }
}
