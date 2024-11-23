namespace Wizzarts.Web.ViewModels.Art
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class ArtInListViewModel : IMapFrom<Art>, IHaveCustomMappings, ISingleArtViewModel, ISingleMemberViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string ArtistName { get; set; } = string.Empty;

        public string AddedByMemberId { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, ArtInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                       x.RemoteImageUrl))
                .ForMember(x => x.Nickname, opt =>
                    opt.MapFrom(x =>
                        x.AddedByMember.Nickname))
                .ForMember(x => x.Username, opt =>
                    opt.MapFrom(x =>
                        x.AddedByMember.UserName))
                .ForMember(x => x.ArtistName, opt =>
                   opt.MapFrom(x =>
                       x.AddedByMember.UserName));
        }
    }
}
