namespace MagicCardsmith.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Art;

    public class CardInListViewModel : IMapFrom<Card>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CardExpansion { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Card, CardInListViewModel>()
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                      x.CardRemoteUrl));
        }
    }
}
