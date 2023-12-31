using MagicCardsmith.Data.Common.Repositories;
using MagicCardsmith.Data.Models;
using MagicCardsmith.Services.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace MagicCardsmith.Services.Data
{
    public class ExpansionService : IExpansionService
    {
        private readonly IDeletableEntityRepository<GameExpansion> gameExpansionRepository;

        public ExpansionService(
            IDeletableEntityRepository<GameExpansion> gameExpansionRepository)
        {
            this.gameExpansionRepository = gameExpansionRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var expansions = this.gameExpansionRepository.AllAsNoTracking()
                 .OrderByDescending(x => x.Id)
                 .To<T>().ToList();

            return expansions;
        }
    }
}
