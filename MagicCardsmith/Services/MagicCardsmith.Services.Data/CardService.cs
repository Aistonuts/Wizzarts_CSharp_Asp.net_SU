namespace MagicCardsmith.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services;
    using MagicCardsmith.Services.Mapping;
    using MagicCardsmith.Web.ViewModels.Card;
    using Microsoft.EntityFrameworkCore;

    public class CardService : ICardService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        private readonly IDeletableEntityRepository<Card> cardRepository;
        private readonly IDeletableEntityRepository<Art> artRepository;
        private readonly IDeletableEntityRepository<CardMana> cardManaRepository;
        private readonly IDeletableEntityRepository<BlackMana> blackManaRepository;
        private readonly IDeletableEntityRepository<BlueMana> blueManaRepository;
        private readonly IDeletableEntityRepository<RedMana> redManaRepository;
        private readonly IDeletableEntityRepository<WhiteMana> whiteManaRepository;
        private readonly IDeletableEntityRepository<GreenMana> greenManaRepository;
        private readonly IDeletableEntityRepository<ColorlessMana> colorlessManaRepository;
        private readonly IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository;
        private readonly IDeletableEntityRepository<CardType> cardTypeRepository;
        private readonly ApplicationDbContext dbContext;

        public CardService(
            IDeletableEntityRepository<Card> cardRepository,
            IDeletableEntityRepository<Art> artRepository,
            IDeletableEntityRepository<CardMana> cardManaRepository,
            IDeletableEntityRepository<BlackMana> blackManaRepository,
            IDeletableEntityRepository<BlueMana> blueManaRepository,
            IDeletableEntityRepository<RedMana> redManaRepository,
            IDeletableEntityRepository<WhiteMana> whiteManaRepository,
            IDeletableEntityRepository<GreenMana> greenManaRepository,
            IDeletableEntityRepository<ColorlessMana> colorlessManaRepository,
            IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository,
            IDeletableEntityRepository<CardType> cardTypeRepository,
            ApplicationDbContext dbContext)
        {
            this.cardRepository = cardRepository;
            this.artRepository = artRepository;
            this.cardManaRepository = cardManaRepository;
            this.blackManaRepository = blackManaRepository;
            this.blueManaRepository = blueManaRepository;
            this.redManaRepository = redManaRepository;
            this.whiteManaRepository = whiteManaRepository;
            this.greenManaRepository = greenManaRepository;
            this.colorlessManaRepository = colorlessManaRepository;
            this.cardFrameColorRepository = cardFrameColorRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateCardInputModel input, string userId, int id, string path)
        {
            var card = new Card
            {
                Name = input.Name,
                BlackManaId = input.BlackManaId,
                BlueManaId = input.BlueManaId,
                RedManaId = input.RedManaId,
                WhiteManaId = input.WhiteManaId,
                GreenManaId = input.GreenManaId,
                ColorlessManaId = input.ColorlessManaId,
                CardFrameColorId = input.CardFrameColorId,
                CardTypeId = input.CardTypeId,
                AbilitiesAndFlavor = input.AbilitiesAndFlavor,
                Power = input.Power,
                Toughness = input.Toughness,
                GameExpansionId = input.GameExpansionId,
                IsEventCard = input.IsEventCard,
                CardSmithId = userId,
            };

            var manaBlack = this.blackManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.BlackManaId);

            if (manaBlack != null)
            {
                for (var i = 0; i < manaBlack.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = manaBlack.ColorName,
                        RemoteImageUrl = manaBlack.ImageUrl,
                    });
                }
            }

            var manaBlue = this.blueManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.BlueManaId);

            if (manaBlue != null)
            {
                for (var i = 0; i < manaBlue.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = manaBlue.ColorName,
                        RemoteImageUrl = manaBlue.ImageUrl,
                    });
                }
            }

            var manaGreen = this.greenManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.GreenManaId);

            if (manaGreen != null)
            {
                for (var i = 0; i < manaGreen.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = manaGreen.ColorName,
                        RemoteImageUrl = manaGreen.ImageUrl,
                    });
                }
            }

            var manaRed = this.redManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.RedManaId);

            if (manaRed != null)
            {
                for (var i = 0; i < manaRed.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = manaRed.ColorName,
                        RemoteImageUrl = manaRed.ImageUrl,
                    });
                }
            }

            var colorlessMana = this.colorlessManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.ColorlessManaId);

            if (colorlessMana != null)
            {
                for (var i = 0; i < colorlessMana.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = colorlessMana.ColorName,
                        RemoteImageUrl = colorlessMana.ImageUrl,
                    });
                }
            }

            var manaWhite = this.whiteManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.WhiteManaId);

            if (manaWhite != null)
            {
                for (var i = 0; i < manaWhite.Cost; i++)
                {
                    card.CardMana.Add(new CardMana()
                    {
                        Color = manaWhite.ColorName,
                        RemoteImageUrl = manaWhite.ImageUrl,
                    });
                }
            }

            var cardType = this.cardTypeRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CardTypeId);

            if (cardType != null)
            {
                card.CardTypeId = cardType.Id;
            }

            var cardFrame = this.cardFrameColorRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.CardFrameId);

            if (cardFrame != null)
            {
                card.CardFrameColorId = cardFrame.Id;
            }

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
            var card = this.cardRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return card;
        }

        public int GetCount()
        {
            return this.cardRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.cardRepository.All()
               .OrderBy(x => x.Id)
               .Take(count)
               .To<T>().ToList();
        }

        public IEnumerable<T> GetByTypeCards<T>(IEnumerable<int> cardTypeIds)
        {
            var query = this.cardRepository.All().AsQueryable();
            query = query.Where(x => cardTypeIds.Contains((int)x.CardTypeId));

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllTypes<T>()
        {
            return this.cardTypeRepository.All()
                .To<T>().ToList();
        }

        public IEnumerable<T> GetByName<T>(string name)
        {
            return this.cardRepository.AllAsNoTracking()
                .Where(x => x.Name == name)
                .To<T>().ToList();
        }
    }
}
