namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.PlayCard;

    using static Wizzarts.Common.AdminConstants;
    using static Wizzarts.Common.HardCodedConstants;

    public class PlayCardService : IPlayCardService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
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

        public async Task<int> GetCount()
        {
            return await this.cardRepository.All().CountAsync();
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
                cachedCards = this.cardRepository.AllAsNoTracking().Where(x => x.ApprovedByAdmin == true && x.ForMainPage == true).OrderByDescending(x => x.Id).To<T>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                this.cache.Set(CardsCacheKey, cachedCards, cacheOptions);
            }

            return cachedCards;
        }

        public async Task CreateAsync(CreateCardViewModel input, string userId, int id, string path, bool isEventCard, int eventCategoryId, string canvasCapture)
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
                CardGameExpansionId = BetaExpansion,
                AddedByMemberId = userId,
                IsEventCard = isEventCard,
                ForMainPage = false,
            };

            var manaBlack = await this.blackManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.BlackManaId);

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

            var manaBlue = await this.blueManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.BlueManaId);

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

            var manaGreen = await this.greenManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.GreenManaId);

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

            var manaRed = await this.redManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.RedManaId);

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

            var colorlessMana = await this.colorlessManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.ColorlessManaId);

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

            var manaWhite = await this.whiteManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.WhiteManaId);

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

            var cardType = await this.cardTypeRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.CardTypeId);

            if (cardType != null)
            {
                card.CardTypeId = cardType.Id;
            }

            var cardFrame = await this.cardFrameColorRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.CardFrameId);

            if (cardFrame != null)
            {
                card.CardFrameColorId = cardFrame.Id;
            }

            var physicalPath = string.Empty;

            if (!isEventCard)
            {
                Directory.CreateDirectory($"{path}/cardsByExpansion/PremiumUserCards");
                physicalPath = $"{path}/cardsByExpansion/PremiumUserCards/{input.Name}.png";
                card.CardRemoteUrl = $"/images/cardsByExpansion/PremiumUserCards/{input.Name}.png";
                card.ArtId = input.ArtId;
            }
            else
            {
                card.EventId = id;
                card.ArtId = null;
                if (eventCategoryId == ImagelessType)
                {
                    Directory.CreateDirectory($"{path}/cardsByExpansion/EventCards/Flavor");
                    physicalPath = $"{path}/cardsByExpansion/EventCards/Flavor/{input.Name}.png";
                    card.CardRemoteUrl = $"/images/cardsByExpansion/EventCards/Flavor/{input.Name}.png";
                }
                else if (eventCategoryId == FlavorlessType)
                {
                    Directory.CreateDirectory($"{path}/cardsByExpansion/EventCards/Flavorless");
                    physicalPath = $"{path}/cardsByExpansion/EventCards/Flavorless/{input.Name}.png";
                    card.CardRemoteUrl = $"/images/cardsByExpansion/EventCards/Flavorless/{input.Name}.png";
                }
                else
                {
                    Directory.CreateDirectory($"{path}/cardsByExpansion/PremiumUserCards");
                    physicalPath = $"{path}/cardsByExpansion/PremiumUserCards/{input.Name}.png";
                    card.CardRemoteUrl = $"/images/cardsByExpansion/PremiumUserCards/{input.Name}.png";
                    card.ArtId = input.ArtId;
                }
            }

            // string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";
            await using (FileStream fs = new FileStream(physicalPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(canvasCapture);
                    MemoryStream ms = new MemoryStream(data, 0, data.Length);

                    bw.Write(data);
                    bw.Close();
                }
            }

            // var extension = Path.GetExtension(input.Images.FileName).TrimStart('.');

            // if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            // {
            //    throw new Exception($"Invalid image extension {extension}");
            // }

            // var physicalPath = $"{path}/cardsByExpansion/EventCards/{input.Name}.{extension}";

            // using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            // await input.Images.CopyToAsync(fileStream);
            await this.cardRepository.AddAsync(card);
            await this.cardRepository.SaveChangesAsync();
            this.cache.Remove(CardsCacheKey);
        }

        public async Task<T> GetById<T>(string id)
        {
            var card = await this.cardRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return card;
        }

        public async Task<IEnumerable<T>> GetAllCardManaByCardId<T>(string id)
        {
            var mana = await this.cardManaRepository.AllAsNoTracking()
              .Where(x => x.CardId == id)
              .To<T>().ToListAsync();

            return mana;
        }

        public async Task<IEnumerable<T>> GetAllCardsByUserId<T>(string id, int page, int itemsPerPage = 12)
        {
            var cards = await this.cardRepository.AllAsNoTracking()
            .Where(x => x.AddedByMemberId == id)
            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
            .To<T>().ToListAsync();

            return cards;
        }

        public async Task<string> ApproveCard(string id)
        {
            var card = await this.cardRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (card != null && card.ApprovedByAdmin == false)
            {
                card.ApprovedByAdmin = true;
                await this.cardRepository.SaveChangesAsync();
                this.cache.Remove(CardsCacheKey);
                return card.AddedByMemberId;
            }

            return null;
        }

        public async Task<bool> CardExist(string id)
        {
            return await this.cardRepository
                .AllAsNoTracking().AnyAsync(a => a.Id == id);
        }

        public async Task<bool> HasUserWithIdAsync(string artId, string userId)
        {
            return await this.cardRepository.AllAsNoTracking()
                 .AnyAsync(a => a.Id == artId && a.AddedByMemberId == userId);
        }

        public async Task DeleteAsync(string id)
        {
            var card = await this.cardRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                this.cardRepository.Delete(card);
                await this.cardRepository.SaveChangesAsync();
                this.cache.Remove(CardsCacheKey);
            }
        }

        public async Task<IEnumerable<T>> GetAllEventCards<T>()
        {
            return await this.cardRepository.AllAsNoTracking()
          .Where(x => x.IsEventCard == true)
          .To<T>().ToListAsync();
        }

        public IEnumerable<T> GetAllNoPagination<T>()
        {
            var card = this.GetCachedData<T>()
            .ToList();
            return card;
        }

        public async Task<IEnumerable<T>> GetAllCardsByCriteria<T>(SingleDeckViewModel input)
        {
            if (input.SearchEvent == "Event" && input.SearchName != null && input.SearchType != null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard == true && x.Name == input.SearchName && x.CardType.Id == input.SearchTypeId)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Event" && input.SearchName == null && input.SearchType != null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard == true && x.CardType.Id == input.SearchTypeId)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Event" && input.SearchName != null && input.SearchType == null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard == true && x.Name == input.SearchName)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Event" && input.SearchName == null && input.SearchType == null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard == true)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Base" && input.SearchName != null && input.SearchType != null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard != true && x.Name == input.SearchName && x.CardType.Id == input.SearchTypeId)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Base" && input.SearchName == null && input.SearchType != null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard != true && x.CardType.Id == input.SearchTypeId)
             .To<T>().ToListAsync();
            }
            else if (input.SearchEvent == "Base" && input.SearchName != null && input.SearchType == null)
            {
                return await this.cardRepository.AllAsNoTracking()
             .Where(x => x.IsEventCard != true && x.Name == input.SearchName)
             .To<T>().ToListAsync();
            }
            else
            {
                var card = this.GetCachedData<T>()
                .ToList();
                return card;
            }
        }

        public T GetByName<T>(string name)
        {
            return this.cardRepository.AllAsNoTracking()
                .Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllCardsByExpansion<T>(int id)
        {
            return await this.cardRepository.AllAsNoTracking()
            .Where(x => x.CardGameExpansionId == id)
            .To<T>().ToListAsync();
        }

        public async Task AddAsync(CreateCardViewModel input, string userId, string path, bool isEventCard, string canvasCapture)
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
                CardGameExpansionId = BetaExpansion,
                AddedByMemberId = userId,
                IsEventCard = isEventCard,
                ArtId = input.ArtId,
            };

            var manaBlack = await this.blackManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.BlackManaId);

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

            var manaBlue = await this.blueManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.BlueManaId);

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

            var manaGreen = await this.greenManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.GreenManaId);

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

            var manaRed = await this.redManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.RedManaId);

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

            var colorlessMana = await this.colorlessManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.ColorlessManaId);

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

            var manaWhite = await this.whiteManaRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.WhiteManaId);

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

            var cardType = await this.cardTypeRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.CardTypeId);

            if (cardType != null)
            {
                card.CardTypeId = cardType.Id;
            }

            var cardFrame = await this.cardFrameColorRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == input.CardFrameId);

            if (cardFrame != null)
            {
                card.CardFrameColorId = cardFrame.Id;
            }

            var physicalPath = " ";

            Directory.CreateDirectory($"{path}/cardsByExpansion/PremiumUserCards/");
            physicalPath = $"{path}/cardsByExpansion/PremiumUserCards/{input.Name.Replace(" ", string.Empty)}.png";
            card.CardRemoteUrl = $"/images/cardsByExpansion/PremiumUserCards/{input.Name.Replace(" ", string.Empty)}.png";

            // string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";
            await using (FileStream fs = new FileStream(physicalPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(canvasCapture);
                    MemoryStream ms = new MemoryStream(data, 0, data.Length);

                    bw.Write(data);
                    bw.Close();
                }
            }

            // var extension = Path.GetExtension(input.Images.FileName).TrimStart('.');

            // if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            // {
            //    throw new Exception($"Invalid image extension {extension}");
            // }

            // var physicalPath = $"{path}/cardsByExpansion/EventCards/{input.Name}.{extension}";

            // using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            // await input.Images.CopyToAsync(fileStream);
            await this.cardRepository.AddAsync(card);
            await this.cardRepository.SaveChangesAsync();
            this.cache.Remove(CardsCacheKey);
        }

        public async Task Promote(string id)
        {
            var card = await this.cardRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            {
                card.CardGameExpansionId = SecondExpansion;
                await this.cardRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> CardTitleExist(string title)
        {
            return await this.cardRepository
              .AllAsNoTracking().AnyAsync(a => a.Name == title);
        }
    }
}
