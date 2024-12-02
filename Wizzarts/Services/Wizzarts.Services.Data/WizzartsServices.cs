namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<T>> GetAllGameRulesData<T>()
        {
            var components = await this.gameRulesDataRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>().ToListAsync();
            return components;
        }

        public async Task<IEnumerable<T>> GetAllWizzartsTeamMembers<T>()
        {
            var team = await this.wizzartsTeamRepository.AllAsNoTracking().
                OrderByDescending(x => x.Id)
                .To<T>().ToListAsync();
            return team;
        }

        public async Task<T> GetGameRules<T>(int id = 1)
        {
            var rules = await this.gameRulesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return rules;
        }

        public string GetUserIdByArtistId(int artistId)
        {
            WizzartsTeam teamMember = this.wizzartsTeamRepository.AllAsNoTracking()
                .FirstOrDefault(a => a.Id == artistId);

            if (teamMember == null)
            {
                return null;
            }

            return teamMember.UserId.ToString();
        }
    }
}
