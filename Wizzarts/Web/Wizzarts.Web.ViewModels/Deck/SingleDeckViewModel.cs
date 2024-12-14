namespace Wizzarts.Web.ViewModels.Deck
{
    using System.Collections.Generic;

    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public class SingleDeckViewModel : IndexAuthenticationViewModel, ISingleDeckViewModel, IMapFrom<CardDeck>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string SearchName { get; set; } = string.Empty;

        public string SearchType { get; set; } = string.Empty;

        public int SearchTypeId { get; set; }

        public int DeckStatusId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string DeckStatus { get; set; } = string.Empty;

        public string DeliveryLocation { get; set; } = string.Empty;

        //public string CreatedByMemberId { get; set; } = string.Empty;

        public string CreatedByMember { get; set; } = string.Empty;

        public bool Locked { get; set; }

        public string IsLocked { get; set; } = string.Empty;

        public string SearchEvent { get; set; } = string.Empty;

        public int StoreId { get; set; }

        public IEnumerable<CardInListViewModel> CardsInDeck { get; set; }

        public IEnumerable<CardTypeViewModel> SelectType { get; set; } = new List<CardTypeViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardDeck, SingleDeckViewModel>()
                .ForMember(x => x.IsLocked, opt =>
                    opt.MapFrom(x =>
                       x.IsLocked ? "Unlock" : "Lock"))
                .ForMember(x => x.Locked, opt =>
                    opt.MapFrom(x =>
                        x.IsLocked))
                .ForMember(x => x.DeliveryLocation, opt =>
                    opt.MapFrom(x =>
                       x.Store.Name.ToString()))
                .ForMember(x => x.DeckStatus, opt =>
                    opt.MapFrom(x =>
                       x.Status.Name.ToString()))
                .ForMember(x => x.CreatedByMember, opt =>
                    opt.MapFrom(x =>
                       x.CreatedByMember.UserName))
                .ForMember(x => x.DeliveryLocation, opt =>
                    opt.MapFrom(x =>
                       x.Store.Name + "," + x.Store.Country + ", " + x.Store.City + ", " + x.Store.Address));
        }
    }
}
