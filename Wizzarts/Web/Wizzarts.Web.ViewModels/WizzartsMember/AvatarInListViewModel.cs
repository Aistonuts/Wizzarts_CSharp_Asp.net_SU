namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class AvatarInListViewModel : IMapFrom<Avatar>, IHaveCustomMappings, ISingleAvatarViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Avatar, AvatarInListViewModel>()
             .ForMember(x => x.AvatarUrl, opt =>
                opt.MapFrom(x =>
                   x.AvatarUrl));
        }
    }
}
