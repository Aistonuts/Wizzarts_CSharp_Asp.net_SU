namespace MagicCardsmith.Web.ViewModels.User
{
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Stores;

    public class MyDataUserViewModel : PagingViewModel, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string AvatarUrl { get; set; }

        public IEnumerable<ArtInListViewModel> Arts { get; set; }

        public IEnumerable<ArticleInListViewModel> Articles { get; set; }

        public IEnumerable<EventInListViewModel> Events { get; set; }

        public IEnumerable<CardInListViewModel> Cards { get; set; }

        public IEnumerable<StoresInListViewModel> Stores { get; set; }

        public IEnumerable<CardReviewInListViewModel> CardReviews { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, MyDataUserViewModel>()
            .ForMember(x => x.AvatarUrl, opt =>
               opt.MapFrom(x =>
                  x.AvatarUrl));
        }
    }
}