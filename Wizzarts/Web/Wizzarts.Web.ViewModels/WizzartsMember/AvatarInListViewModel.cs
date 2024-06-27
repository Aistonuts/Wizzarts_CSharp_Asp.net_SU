using AutoMapper;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class AvatarInListViewModel : IMapFrom<Avatar>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Avatar, AvatarInListViewModel>()
             .ForMember(x => x.AvatarUrl, opt =>
                opt.MapFrom(x =>
                   x.AvatarUrl));
        }
    }
}
