namespace MagicCardsmith.Web.ViewModels.Event
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class MilestonesInListViewModel : IMapFrom<EventMilestone>, IHaveCustomMappings, ISingleMilestoneViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public int EventId { get; set; }

        public bool IsCompleted { get; set; }

        public bool RequireArtInput { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EventMilestone, MilestonesInListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.ImageUrl));
        }
    }
}
