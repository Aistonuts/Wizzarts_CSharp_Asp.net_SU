namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class WizzartsServices : IWizzartsServices
    {
        private readonly IDeletableEntityRepository<WizzartsGameRules> gameRulesRepository;
        private readonly IDeletableEntityRepository<WizzartsGameRulesData> gameRulesDataRepository;
        private readonly IDeletableEntityRepository<WizzartsTeam> wizzartsTeamRepository;

        public WizzartsServices(
            IDeletableEntityRepository<WizzartsGameRules> gameRulesRepository,
            IDeletableEntityRepository<WizzartsGameRulesData> gameRulesDataRepository,
            IDeletableEntityRepository<WizzartsTeam> wizzartsTeamRepository)
        {
             this.gameRulesRepository = gameRulesRepository;
             this.gameRulesDataRepository = gameRulesDataRepository;
             this.wizzartsTeamRepository = wizzartsTeamRepository;
        }

        public IEnumerable<T> GetAllGameRulesData<T>()
        {
            var components = this.gameRulesDataRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToList();
            return components;
        }

        public IEnumerable<T> GetAllWizzartsTeamMembers<T>()
        {
            var team = this.wizzartsTeamRepository.AllAsNoTracking().
                OrderByDescending(x => x.Id)
                .To<T>().ToList();
            return team;
        }

        public T GetGameRules<T>(int id = 1)
        {
            var rules = this.gameRulesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return rules;
        }
    }
}
