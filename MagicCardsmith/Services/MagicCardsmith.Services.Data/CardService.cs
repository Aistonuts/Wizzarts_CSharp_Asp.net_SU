namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;

    public class CardService : ICardService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        private readonly IDeletableEntityRepository<Card> cardRepository;
        private readonly IDeletableEntityRepository<CardMana> cardManaRepository;
        private readonly IDeletableEntityRepository<BlackMana> blackManaRepository;
        private readonly IDeletableEntityRepository<BlueMana> blueManaRepository;
        private readonly IDeletableEntityRepository<RedMana> redManaRepository;
        private readonly IDeletableEntityRepository<WhiteMana> whiteManaRepository;
        private readonly IDeletableEntityRepository<GreenMana> greenManaRepository;
        private readonly IDeletableEntityRepository<ColorlessMana> colorlessManaRepository;
        private readonly IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository;
        private readonly IDeletableEntityRepository<CardType> cardTypeRepository;

        public CardService(
            IDeletableEntityRepository<Card> cardRepository,
            IDeletableEntityRepository<CardMana> cardManaRepository,
            IDeletableEntityRepository<BlackMana> blackManaRepository,
            IDeletableEntityRepository<BlueMana> blueManaRepository,
            IDeletableEntityRepository<RedMana> redManaRepository,
            IDeletableEntityRepository<WhiteMana> whiteManaRepository,
            IDeletableEntityRepository<GreenMana> greenManaRepository,
            IDeletableEntityRepository<ColorlessMana> colorlessManaRepository,
            IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository,
            IDeletableEntityRepository<CardType> cardTypeRepository)
        {
            this.cardRepository = cardRepository;
            this.cardManaRepository = cardManaRepository;
            this.blackManaRepository = blackManaRepository;
            this.blueManaRepository = blueManaRepository;
            this.redManaRepository = redManaRepository;
            this.whiteManaRepository = whiteManaRepository;
            this.greenManaRepository = greenManaRepository;
            this.colorlessManaRepository = colorlessManaRepository;
            this.cardFrameColorRepository = cardFrameColorRepository;
            this.cardTypeRepository = cardTypeRepository;
        }

        public async Task CreateAsync(CreateCardInputModel input, string userId, string id, string path)
        {
            var card = new Card
            {
                Name = input.Name,
                AbilitiesAndFlavor = input.AbilitiesAndFlavor,
                Power = input.Power,
                Toughness = input.Toughness,
                GameExpansionId = input.GameExpansionId,
                IsEventCard = input.IsEventCard,
                ArtId = input.ArtId,
                CardSmithId = input.CardSmithId,
            };

            var manaBlack = this.blackManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.BlackManaId);

            for (var i = 0; i < manaBlack.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = manaBlack.ColorName,
                    RemoteImageUrl = manaBlack.ImageUrl,
                });
            }

            var manaBlue = this.blueManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.BlueManaId);

            for (var i = 0; i < manaBlue.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = manaBlue.ColorName,
                    RemoteImageUrl = manaBlue.ImageUrl,
                });
            }

            var manaGreen = this.greenManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.GreenManaId);

            for (var i = 0; i < manaGreen.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = manaGreen.ColorName,
                    RemoteImageUrl = manaGreen.ImageUrl,
                });
            }

            var manaRed = this.redManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.RedManaId);

            for (var i = 0; i < manaRed.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = manaRed.ColorName,
                    RemoteImageUrl = manaRed.ImageUrl,
                });
            }

            var colorlessMana = this.colorlessManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.ColorlessManaId);

            for (var i = 0; i < colorlessMana.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = colorlessMana.ColorName,
                    RemoteImageUrl = colorlessMana.ImageUrl,
                });
            }

            var manaWhite = this.whiteManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.WhiteManaId);

            for (var i = 0; i < manaWhite.Cost; i++)
            {
                card.CardMana.Add(new CardMana()
                {
                    Color = manaWhite.ColorName,
                    RemoteImageUrl = manaWhite.ImageUrl,
                });
            }

            var cardType = this.cardTypeRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CardTypeId);

            card.CardType = cardType.Name;

            var cardFrame = this.cardFrameColorRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CardFrameId);

            card.CardFrameColor = cardFrame.Name;

            await this.cardRepository.AddAsync(card);
            await this.cardRepository.SaveChangesAsync();

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
