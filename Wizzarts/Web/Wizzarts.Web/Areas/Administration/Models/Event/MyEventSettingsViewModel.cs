namespace Wizzarts.Web.Areas.Administration.Models.Event
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.TagHelper;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class MyEventSettingsViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int ComponentId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EventTitleMaxLength, MinimumLength = EventTitleMinLength, ErrorMessage = LengthMessage)]
        [Comment("Event Component Title")]
        public string ComponentTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = LengthMessage)]
        [Comment("Event Description")]
        public string ComponentDescription { get; set; } = string.Empty;

        public string ComponentImageUrl { get; set; }

        public IFormFile Image { get; set; }

        public int EventCategoryId { get; set; }

        public int EventId { get; set; }

        public string EventTitle { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public string EventCreator { get; set; } = string.Empty;

        public string CreatorAvatar { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool OwnerBrowsing { get; set; }

        public string ActionId { get; set; } = string.Empty;

        public string ControllerId { get; set; } = string.Empty;

        public IEnumerable<EventCategoryInListViewModel> EventCategories { get; set; }

        public IEnumerable<EventComponentsInListViewModel> EventComponents { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, MyEventSettingsViewModel>()
             .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                    x.RemoteImageUrl))
             .ForMember(x => x.EventTitle, opt =>
                 opt.MapFrom(x =>
                    x.Title))
             .ForMember(x => x.EventDescription, opt =>
                 opt.MapFrom(x =>
                    x.EventDescription));
        }
    }
}
