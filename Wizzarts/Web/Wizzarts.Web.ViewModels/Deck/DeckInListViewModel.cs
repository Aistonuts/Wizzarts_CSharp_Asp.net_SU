namespace Wizzarts.Web.ViewModels.Deck
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class DeckInListViewModel : IMapFrom<CardDeck>, IHaveCustomMappings, ISingleDeckViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public int StoreId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string IsLocked { get; set; } = string.Empty;

        //public string CreatedByMemberId { get; set; } = string.Empty;

        public string CreatedByMember { get; set; } = string.Empty;

        public string CreatedByUser { get; set; } = string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardDeck, DeckInListViewModel>()
                .ForMember(x => x.IsLocked, opt =>
                    opt.MapFrom(x =>
                       x.IsLocked ? "Ready" : "Open"))
                .ForMember(x => x.ShippingAddress, opt =>
                    opt.MapFrom(x =>
                       x.Store.Name.ToString()))
                .ForMember(x => x.CreatedByMember, opt =>
                    opt.MapFrom(x =>
                       x.CreatedByMember.Nickname))
                .ForMember(x => x.CreatedByUser, opt =>
                    opt.MapFrom(x =>
                        x.CreatedByMember.UserName));
        }
    }
}
