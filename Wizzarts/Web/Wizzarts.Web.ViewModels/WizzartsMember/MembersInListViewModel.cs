namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class MembersInListViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings, ISingleMemberViewModel
    {
        //public string Id { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; }

        public string AvatarUrl { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public int CountOfArts { get; set; }

        public int CountOfArticles { get; set; }

        public int CountOfEvents { get; set; }

        public int CountOfCards { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, MembersInListViewModel>()
                .ForMember(x => x.AvatarUrl, opt =>
                    opt.MapFrom(x =>
                        x.AvatarUrl))
                .ForMember(x => x.Nickname, opt =>
                    opt.MapFrom(x =>
                        x.Nickname))
                .ForMember(x => x.Username, opt =>
                    opt.MapFrom(x =>
                        x.UserName));
        }
    }
}
