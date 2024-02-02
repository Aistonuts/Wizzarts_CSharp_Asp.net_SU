using MagicCardsmith.Web.ViewModels.Card;
using System.Net.Http;

namespace MagicCardsmith.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;

        public SearchService(
            ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SearchAutocompleteViewModel>> GetAllCardsByTerm(string term)
        {
            IEnumerable<SearchAutocompleteViewModel> allCards = await this.dbContext
                .Cards
                .Where(c => c.Name.Contains(term)).Select(x => new SearchAutocompleteViewModel
                {
                    CardName = x.Name,
                    CombinedName = x.Name,
                    Text = x.AbilitiesAndFlavor,
                }).ToArrayAsync();

            return allCards;
        }

        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel()
            {
                TotalCards = await this.dbContext.Cards.CountAsync(),
                TotalArt = await this.dbContext.Arts.CountAsync(),
                Names = await this.dbContext
                .Cards
                .Select(x => x.Name).ToListAsync(),
            };
        }
    }
}
