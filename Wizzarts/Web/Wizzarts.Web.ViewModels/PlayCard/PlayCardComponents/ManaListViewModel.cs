namespace Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class ManaListViewModel : IMapFrom<ManaInCard>, IHaveCustomMappings
    {
        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ManaInCard, ManaListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.RemoteImageUrl));
        }
    }
}
