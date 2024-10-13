namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleMemberViewModel : IndexAuthenticationViewModel, IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SingleMemberViewModel>()
            .ForMember(x => x.AvatarUrl, opt =>
                    opt.MapFrom(x =>
                       x.AvatarUrl));
        }
    }
}
