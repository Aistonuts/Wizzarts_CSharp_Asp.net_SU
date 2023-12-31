namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class CardService : ICardService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        private readonly IDeletableEntityRepository<Card> cardRepository;
        private readonly IDeletableEntityRepository<CardMana> cardManaRepository;

        public CardService(
            IDeletableEntityRepository<Card> cardRepository,
            IDeletableEntityRepository<CardMana> cardManaRepository)
        {
            this.cardRepository = cardRepository;
            this.cardManaRepository = cardManaRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var card = this.cardRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return card;
        }

        public IEnumerable<T> GetAllByCardId<T>(int id)
        {
            var mana = this.cardManaRepository.AllAsNoTracking()
              .Where(x => x.CardId == id)
              .To<T>().ToList();

            return mana;
        }

        public T GetById<T>(int id)
        {
            var art = this.cardRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return art;
        }

        public int GetCount()
        {
            return this.cardRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.cardRepository.All()
               .OrderBy(x => Guid.NewGuid())
               .Take(count)
               .To<T>().ToList();
        }
    }
}
