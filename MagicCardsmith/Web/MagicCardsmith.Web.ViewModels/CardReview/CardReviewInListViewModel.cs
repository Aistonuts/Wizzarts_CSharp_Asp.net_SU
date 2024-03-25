namespace MagicCardsmith.Web.ViewModels.CardReview
{
    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Expansion;

    public class CardReviewInListViewModel : IMapFrom<CardReview>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Review { get; set; }

        public string Suggestions { get; set; }

        public string PostedByUser { get; set; }

        public int CardId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CardReview, CardReviewInListViewModel>()
                .ForMember(x => x.PostedByUser, opt =>
                opt.MapFrom(x => x.PostedByUser.UserName));
        }
    }
}
