using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data;
using Wizzarts.Web.ViewModels.Search;

namespace Wizzarts.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SearchAutocompleteViewModel>> GetAllCardsByTerm(string term)
        {
            IEnumerable<SearchAutocompleteViewModel> allCards = await this.dbContext
               .PlayCards
               .Where(c => c.Name.Contains(term)).Select(x => new SearchAutocompleteViewModel
               {
                   CardName = x.Name,
                   CombinedName = x.Name,
                   Text = x.AbilitiesAndFlavor,
               }).ToArrayAsync();

            return allCards;
        }
    }
}
