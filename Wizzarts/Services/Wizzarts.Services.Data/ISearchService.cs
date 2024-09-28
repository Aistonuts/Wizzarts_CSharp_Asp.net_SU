using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Search;

namespace Wizzarts.Services.Data
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchAutocompleteViewModel>> GetAllCardsByTerm(string term);
    }
}
