namespace Wizzarts.Web.ViewModels.PlayCard
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class CardInListViewModel : IMapFrom<PlayCard>, IHaveCustomMappings, ISingleCardViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string CardRemoteUrl { get; set; } = string.Empty;

        public string CardExpansion { get; set; } = string.Empty;

        public string CardTypeId { get; set; } = string.Empty;

        public string AbilitiesAndFlavor { get; set; } = string.Empty;

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
