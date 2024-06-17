namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

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
                 .OrderBy(x => x.Id)
                 .Where(x => x.Cards.Count() > 0)
                 .To<T>().ToList();

            return expansions;
        }

        public T GetById<T>(int id)
        {
            var expansion = this.gameExpansionRepository.AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefault();
            return expansion;
        }
    }
}
