namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Art;

    public class CardInListViewModel : IMapFrom<PlayCard>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardExpansion { get; set; }

        public string CardTypeId { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public bool IsEventCard { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PlayCard, CardInListViewModel>()
         .ForMember(x => x.CardRemoteUrl, opt =>
          opt.MapFrom(x => x.CardRemoteUrl));

            configuration.CreateMap<PlayCard, CardInListViewModel>()
         .ForMember(x => x.CardExpansion, opt =>
          opt.MapFrom(x => x.CardGameExpansionId));
        }
    }
}
