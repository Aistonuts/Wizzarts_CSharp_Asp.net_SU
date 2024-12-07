namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleMemberViewModel : IndexAuthenticationViewModel, IMapFrom<ApplicationUser>, ISingleMemberViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SingleMemberViewModel>()
            .ForMember(x => x.AvatarUrl, opt =>
                    opt.MapFrom(x =>
                       x.AvatarUrl))
            .ForMember(x => x.Username, opt =>
                    opt.MapFrom(x =>
                       x.UserName))
            .ForMember(x => x.Nickname, opt =>
                opt.MapFrom(x =>
                    x.Nickname));
        }
    }
}
