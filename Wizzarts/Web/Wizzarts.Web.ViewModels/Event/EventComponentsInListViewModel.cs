namespace Wizzarts.Web.ViewModels.Event
{
    using AutoMapper;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class EventComponentsInListViewModel : IMapFrom<EventComponent>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int EventId { get; set; }

        public bool IsCompleted { get; set; }

        public int EventCategoryId { get; set; }

        public string ActionName { get; set; } = string.Empty;

        public string ControllerName { get; set; } = string.Empty;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EventComponent, EventComponentsInListViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                     x.ImageUrl))
              .ForMember(x => x.ActionName, opt =>
                  opt.MapFrom(x =>
                     x.ActionName.Name.ToString()))
                .ForMember(x => x.ControllerName, opt =>
                  opt.MapFrom(x =>
                     x.ControllerName.Name.ToString()));
        }
    }
}
