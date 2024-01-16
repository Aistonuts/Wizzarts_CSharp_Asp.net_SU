namespace MagicCardsmith.Web.ViewModels.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class SingleEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string EventDescription { get; set; }

        public string CreatorId { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<MilestonesInListViewModel> EventMilestones { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, SingleEventViewModel>()
             .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                    x.RemoteImageUrl));
        }
    }
}
