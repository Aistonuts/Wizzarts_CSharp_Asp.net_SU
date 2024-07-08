namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.VisualBasic;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.PlayCard;
    using static Wizzarts.Common.GlobalConstants;

    public class PlayCardService : IPlayCardService
    {
        private readonly IDeletableEntityRepository<PlayCard> cardRepository;
        private readonly IDeletableEntityRepository<ManaInCard> cardManaRepository;
        private readonly IDeletableEntityRepository<BlackMana> blackManaRepository;
        private readonly IDeletableEntityRepository<BlueMana> blueManaRepository;
        private readonly IDeletableEntityRepository<RedMana> redManaRepository;
        private readonly IDeletableEntityRepository<WhiteMana> whiteManaRepository;
        private readonly IDeletableEntityRepository<GreenMana> greenManaRepository;
        private readonly IDeletableEntityRepository<ColorlessMana> colorlessManaRepository;
        private readonly IDeletableEntityRepository<PlayCardFrameColor> cardFrameColorRepository;
        private readonly IDeletableEntityRepository<PlayCardType> cardTypeRepository;
        private readonly IMemoryCache cache;

        public PlayCardService(
            IDeletableEntityRepository<PlayCard> cardRepository,
            IDeletableEntityRepository<Art> artRepository,
            IDeletableEntityRepository<ManaInCard> cardManaRepository,
            IDeletableEntityRepository<BlackMana> blackManaRepository,
            IDeletableEntityRepository<BlueMana> blueManaRepository,
            IDeletableEntityRepository<RedMana> redManaRepository,
            IDeletableEntityRepository<WhiteMana> whiteManaRepository,
            IDeletableEntityRepository<GreenMana> greenManaRepository,
            IDeletableEntityRepository<ColorlessMana> colorlessManaRepository,
            IDeletableEntityRepository<PlayCardFrameColor> cardFrameColorRepository,
            IDeletableEntityRepository<PlayCardType> cardTypeRepository,
            IMemoryCache cache)
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
            this.cache = cache;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var card = this.GetCachedData<T>()
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ToList();
            return card;
        }

        public int GetCount()
        {
            return this.cardRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.GetCachedData<T>()
            .Take(count)
            .ToList();
        }

        public IEnumerable<T> GetCachedData<T>()
        {
            var cachedCards = this.cache.Get<IEnumerable<T>>(CardsCacheKey);

            if (cachedCards == null)
            {
                cachedCards = this.cardRepository.AllAsNoTracking().Where(x => x.ApprovedByAdmin == true).OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(CardsCacheKey, cachedCards, cacheOptions);
            }

            return cachedCards;
        }

        public async Task CreateAsync(CreateCardViewModel input, string userId, int id, string path, bool isEventCard, bool requireArtInput, string canvasCapture)
        {
            var card = new PlayCard
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
                CardGameExpansionId = input.GameExpansionId,
                AddedByMemberId = userId,
                EventId = id,
                IsEventCard = isEventCard,
                ArtId = input.ArtId,
            };

            var manaBlack = this.blackManaRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.BlackManaId);

            if (manaBlack != null)
            {
                for (var i = 0; i < manaBlack.Cost; i++)
                {
                    card.CardMana.Add(new ManaInCard()
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
                    card.CardMana.Add(new ManaInCard()
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
                    card.CardMana.Add(new ManaInCard()
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
                    card.CardMana.Add(new ManaInCard()
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
                    card.CardMana.Add(new ManaInCard()
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
                    card.CardMana.Add(new ManaInCard()
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
            var physicalPath = " ";
            if (requireArtInput)
            {
                physicalPath = $"{path}/cardsByExpansion/EventCards/Flavor/{input.Name}.png";
                card.CardRemoteUrl = $"/images/cardsByExpansion/EventCards/Flavor/{input.Name}.png";
            }
            else
            {
                physicalPath = $"{path}/cardsByExpansion/EventCards/Flavorless/{input.Name}.png";
                card.CardRemoteUrl = $"/images/cardsByExpansion/EventCards/Flavorless/{input.Name}.png";
            }

            //string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";

            using (FileStream fs = new FileStream(physicalPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(canvasCapture);
                    MemoryStream ms = new MemoryStream(data, 0, data.Length);

                    bw.Write(data);
                    bw.Close();
                }
            }
            //var extension = Path.GetExtension(input.Images.FileName).TrimStart('.');


            //if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            //{
            //    throw new Exception($"Invalid image extension {extension}");
            //}

            //var physicalPath = $"{path}/cardsByExpansion/EventCards/{input.Name}.{extension}";

            //using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            //await input.Images.CopyToAsync(fileStream);
;

            await this.cardRepository.AddAsync(card);
            await this.cardRepository.SaveChangesAsync();
            this.cache.Remove(CardsCacheKey);
        }

        public T GetById<T>(int id)
        {
            var card = this.cardRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return card;
        }

        public IEnumerable<T> GetAllCardManaByCardId<T>(int id)
        {
            var mana = this.cardManaRepository.AllAsNoTracking()
              .Where(x => x.CardId == id)
              .To<T>().ToList();

            return mana;
        }
    }
}
