namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Wizzarts.Web.ViewModels.Search;

    public interface ISearchService
    {
        Task<IEnumerable<SearchAutocompleteViewModel>> GetAllCardsByTerm(string term);
    }
}
