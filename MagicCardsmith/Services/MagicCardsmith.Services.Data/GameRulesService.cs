using System.Collections.Generic;

namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class GameRulesService : IGameRulesService
    {
        private readonly IDeletableEntityRepository<MagicCardsmithGameRules> gameRulesRepository;
        private readonly IDeletableEntityRepository<GameRulesComponents> gameRulesComponentsRepository;

        public GameRulesService(
            IDeletableEntityRepository<MagicCardsmithGameRules> gameRulesRepository,
            IDeletableEntityRepository<GameRulesComponents> gameRulesComponentsRepository)
        {
            this.gameRulesRepository = gameRulesRepository;
            this.gameRulesComponentsRepository = gameRulesComponentsRepository;

        }

        public T Get<T>(int id = 1)
        {
            var rules = this.gameRulesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return rules;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var components = this.gameRulesComponentsRepository.AllAsNoTracking()
                .OrderByDescending( x => x.Id)
                .To<T>().ToList();
            return components;
        }
    }
}