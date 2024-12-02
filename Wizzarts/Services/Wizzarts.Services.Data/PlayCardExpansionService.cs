namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class PlayCardExpansionService : IPlayCardExpansionService
    {
        private readonly IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository;

        public PlayCardExpansionService(
            IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository)
        {
            this.cardGameExpansionRepository = cardGameExpansionRepository;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var expansions = await this.cardGameExpansionRepository.AllAsNoTracking()
                 .OrderBy(x => x.Id)
                 .Where(x => x.Cards.Any())
                 .To<T>().ToListAsync();

            return expansions;
        }

        public async Task<T> GetById<T>(int id)
        {
            var expansion = await this.cardGameExpansionRepository.AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefaultAsync();
            return expansion;
        }
    }
}
