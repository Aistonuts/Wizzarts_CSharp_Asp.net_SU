namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardTesting;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Http;

    public class SingleEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string CreatorId { get; set; }

        public string ImageUrl { get; set; }

        // Store event components
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Display(Name = "Country")]
        public string StoreCountry { get; set; }

        [Display(Name = "City")]
        public string StoreCity { get; set; }

        [Display(Name = "Phone number")]
        public string StorePhoneNumber { get; set; }

        [Display(Name = "Store address")]
        public string StoreAddress { get; set; }

        [Display(Name = "Store image")]
        public IFormFile StoreImage { get; set; }

        public string StoreOwnerId { get; set; }

        // end
        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<ExpansionInListViewModel> GameExpansions { get; set; }

        public IEnumerable<MilestonesInListViewModel> EventMilestones { get; set; }

        public IEnumerable<EventCardsInListViewModel> EventCards { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventViewModel>()
             .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                    x.RemoteImageUrl));
        }
    }
}
