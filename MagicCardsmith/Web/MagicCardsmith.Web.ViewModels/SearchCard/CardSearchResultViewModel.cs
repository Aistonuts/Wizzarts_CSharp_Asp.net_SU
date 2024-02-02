namespace MagicCardsmith.Web.ViewModels.SearchCard
{
    using System.Collections.Generic;

    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel;

    public class CardSearchResultViewModel : IMapFrom<Card>
    {
        public IEnumerable<CardTypeViewModel> SelectType { get; set; }

        public IEnumerable<CardInListViewModel> SearchedCards { get; set; }
    }
}
