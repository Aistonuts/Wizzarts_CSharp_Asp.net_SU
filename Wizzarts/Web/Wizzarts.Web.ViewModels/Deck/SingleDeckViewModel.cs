using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Home;
using Wizzarts.Web.ViewModels.PlayCard;
using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

namespace Wizzarts.Web.ViewModels.Deck
{
    public class SingleDeckViewModel : IndexAuthenticationViewModel, IMapFrom<CardDeck>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SearchName { get; set; }

        public string SearchType { get; set; }

        public int SearchTypeId { get; set; }

        public int DeckStatusId { get; set; }

        public string DeckStatus { get; set; }

        public string DeliveryLocation { get; set; }

        public string IsLocked { get; set; }

        public string SearchEvent { get; set; }

        public IEnumerable<DeckInListViewModel> Decks { get; set; }

        public IEnumerable<CardInListViewModel> CardsInDeck { get; set; }

        public IEnumerable<DeckStatusListViewModel> DeckStatuses { get; set; }

        public IEnumerable<CardTypeViewModel> SelectType { get; set; } = new List<CardTypeViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardDeck, SingleDeckViewModel>()
                .ForMember(x => x.IsLocked, opt =>
                    opt.MapFrom(x =>
                       x.IsLocked ? "Locked" : "Open"))
                .ForMember(x => x.DeliveryLocation, opt =>
                    opt.MapFrom(x =>
                       x.Store.Name.ToString()))
                .ForMember(x => x.DeckStatus, opt =>
                    opt.MapFrom(x =>
                       x.Status.Name.ToString()));
        }
    }
}
