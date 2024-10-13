namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class PlayCardExpansionService : IPlayCardExpansionService
    {
        private readonly IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository;

        public PlayCardExpansionService(IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository)
        {
            this.cardGameExpansionRepository = cardGameExpansionRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var expansions = this.cardGameExpansionRepository.AllAsNoTracking()
                 .OrderBy(x => x.Id)
                 .Where(x => x.Cards.Any())
                 .To<T>().ToList();

            return expansions;
        }

        public T GetById<T>(int id)
        {
            var expansion = this.cardGameExpansionRepository.AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefault();
            return expansion;
        }
    }
}
