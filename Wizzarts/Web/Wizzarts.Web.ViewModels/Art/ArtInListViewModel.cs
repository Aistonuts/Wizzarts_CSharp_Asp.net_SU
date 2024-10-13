namespace Wizzarts.Web.ViewModels.Art
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class ArtInListViewModel : IMapFrom<Art>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ArtistName { get; set; }

        public string AddedByMemberId { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Art, ArtInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                       x.RemoteImageUrl))
                .ForMember(x => x.ArtistName, opt =>
                   opt.MapFrom(x =>
                       x.AddedByMember.UserName));
        }
    }
}