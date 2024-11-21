namespace Wizzarts.Web.ViewModels.Art
{
    using System;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleArtViewModel : IndexAuthenticationViewModel, IMapFrom<Art>, IHaveCustomMappings, ISingleArtViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string AddedByMember { get; set; } = string.Empty;

        public string MemberNickname { get; set; } = string.Empty;

        public string MemberAvatar { get; set; } = string.Empty;

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, SingleArtViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                       x.RemoteImageUrl))
                .ForMember(x => x.AddedByMember, opt =>
                   opt.MapFrom(x =>
                       x.AddedByMember.UserName));
        }
    }
}
