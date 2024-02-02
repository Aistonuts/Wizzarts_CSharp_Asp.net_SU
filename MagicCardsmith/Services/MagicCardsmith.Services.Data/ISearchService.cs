namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.SearchCard;

    public interface ISearchService
    {
        Task<StatisticsServiceModel> GetStatisticsAsync();

        Task<IEnumerable<SearchAutocompleteViewModel>> GetAllCardsByTerm( string term);
    }
}
