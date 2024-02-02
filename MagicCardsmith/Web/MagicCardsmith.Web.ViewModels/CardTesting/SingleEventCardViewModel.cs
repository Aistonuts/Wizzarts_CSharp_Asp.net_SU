namespace MagicCardsmith.Web.ViewModels.CardTesting
{
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.CardReview;

    public class SingleEventCardViewModel : IMapFrom<Card>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CardExpansion { get; set; }

        public IEnumerable<CardReviewInListViewModel> CardReview { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Card, SingleEventCardViewModel>()
               .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                      x.CardRemoteUrl));
        }
    }
}
