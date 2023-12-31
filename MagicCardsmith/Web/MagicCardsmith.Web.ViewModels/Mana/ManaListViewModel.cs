namespace MagicCardsmith.Web.ViewModels.Mana
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class ManaListViewModel : IMapFrom<CardMana>, IHaveCustomMappings
    {
        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardMana, ManaListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.RemoteImageUrl));
        }
    }
}
