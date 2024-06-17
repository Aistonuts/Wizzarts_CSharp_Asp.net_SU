namespace MagicCardsmith.Web.ViewModels.Event
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardTesting;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using Microsoft.AspNetCore.Http;

    using static MagicCardsmith.Common.DataConstants;

    public class SingleEventViewModel : IMapFrom<Event>, IHaveCustomMappings, ISingleEventViewModel
    {
        public int EventId { get; set; }

        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string CreatorId { get; set; }

        public string ImageUrl { get; set; }

        // Store event components
        [Required(ErrorMessage = "Store name is required!")]
        [StringLength(StoreNameMaxLength, MinimumLength = StoreNameMinLength, ErrorMessage = "Store name  should be between 5 and 30 characters long")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "Country")]
        public string StoreCountry { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "City")]
        public string StoreCity { get; set; }

        [Required(ErrorMessage = "Store phone number is required!")]
        [StringLength(StorePhoneMaxLength, MinimumLength = StorePhoneMinLength, ErrorMessage = "Store phone number should be between 7 and 15 characters long")]
        [Display(Name = "Phone mumber")]
        public string StorePhoneNumber { get; set; }

        [Required(ErrorMessage = "Store location is required!")]
        [StringLength(StoreLocationMaxLength, MinimumLength = StoreLocationMinLength, ErrorMessage = "Store location should be between 5 and 50 characters long")]
        [Display(Name = "Address")]
        public string StoreAddress { get; set; }

        [Required(ErrorMessage = "Store image is required!")]
        public IFormFile StoreImage { get; set; }

        public string StoreOwnerId { get; set; }

        public bool ApprovedByAdmin { get; set; }

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
