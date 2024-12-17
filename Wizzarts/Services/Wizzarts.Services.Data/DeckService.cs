namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class DeckService : IDeckService
    {
        private readonly IDeletableEntityRepository<CardDeck> deckRepository;
        private readonly IDeletableEntityRepository<Order> deckOrderRepository;
        private readonly IDeletableEntityRepository<CardOrder> cardOrderRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<DeckStatus> deckStatusRepository;
        private readonly IDeletableEntityRepository<DeckOfCards> deckOfCardsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IPlayCardService cardService;
        private readonly IArtService artService;
        private readonly IFileService fileService;

        public DeckService(
            IDeletableEntityRepository<CardDeck> deckRepository,
            IDeletableEntityRepository<Order> deckOrderRepository,
            IDeletableEntityRepository<CardOrder> cardOrderRepository,
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<PlayCard> playCardRepository,
            IDeletableEntityRepository<DeckOfCards> deckOfCardsRepository,
            IDeletableEntityRepository<DeckStatus> deckStatusRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IPlayCardService cardService,
            IArtService artService,
            IFileService fileService)
        {
            this.deckRepository = deckRepository;
            this.deckOrderRepository = deckOrderRepository;
            this.cardOrderRepository = cardOrderRepository;
            this.eventRepository = eventRepository;
            this.playCardRepository = playCardRepository;
            this.deckOfCardsRepository = deckOfCardsRepository;
            this.deckStatusRepository = deckStatusRepository;
            this.userRepository = userRepository;
            this.cardService = cardService;
            this.artService = artService;
            this.fileService = fileService;

        }

        public async Task CreateAsync(CreateDeckViewModel input, string userId, string imagePath)
        {
            var arts = this.artService.GetRandom<ArtInListViewModel>(1);
            var artUrl = arts.FirstOrDefault()?.ImageUrl;
            var currentEvent = await this.eventRepository.All().FirstOrDefaultAsync(x => x.Id == input.EventId);

            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);

            var deck = new CardDeck
            {
                Name = await this.fileService.Sanitize(input.Name),
                Description = await this.fileService.Sanitize(input.Description),

                StoreId = input.StoreId,
                CreatedByMemberId = userId,
                StatusId = 1,
                ImageUrl = artUrl,
                IsLocked = false,
            };

            await this.deckRepository.AddAsync(deck);
            await this.deckRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditDeckViewModel input, int id)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (deck != null)
            {
                deck.Name = await this.fileService.Sanitize(input.Name);
                deck.Description = await this.fileService.Sanitize(input.Description);
                deck.StoreId = input.StoreId;

                await this.deckRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var decks = await this.deckRepository.AllAsNoTracking()
                .Where(x => x.IsLocked == true)
                .To<T>().ToListAsync();
            return decks;
        }

        public async Task<IEnumerable<T>> GetAllDecksByUserId<T>(string id)
        {
            var decks = await this.deckRepository.AllAsNoTracking()
                .Where(x => x.CreatedByMemberId == id)
                .To<T>().ToListAsync();

            return decks;
        }

        public async Task<T> GetById<T>(int id)
        {
            var deck = await this.deckRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return deck;
        }

        public async Task<int> AddAsync(int deckId, string cardId)
        {
            var card = await this.playCardRepository.All().FirstOrDefaultAsync(x => x.Id == cardId);

            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == deckId);
            if (deck != null && card != null && !deck.IsLocked)
            {
                deck.PlayCards.Add(card);
                await this.deckRepository.SaveChangesAsync();
            }

            return deckId;
        }

        public async Task<IEnumerable<T>> GetAllCardsInDeckId<T>(int id)
        {
            var listOfCards = new List<T>();
            var deckOfCards = await this.deckOfCardsRepository.AllAsNoTracking().Where(x => x.DeckId == id).ToListAsync();
            foreach (var item in deckOfCards)
            {
                var card = await this.cardService.GetById<T>(item.PlayCardId);
                listOfCards.Add(card);
            }

            return listOfCards;
        }

        public async Task OrderAsync(int deckId, string userId)
        {
            var deck = await this.deckRepository.AllAsNoTracking()
               .Where(x => x.Id == deckId)
               .To<SingleDeckViewModel>().FirstOrDefaultAsync();
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().Where(x => x.DeckId == deckId);
            var order = new Order()
            {
                Title = deck.Name,
                DeckId = deckId,
                OrderStatusId = 1,
                DeckImageUrl = deck.ImageUrl,
                RecipientId = userId,
                Description = deck.Description,
                EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40)),
                ShippingAddress = deck.DeliveryLocation,
            };

            foreach (var item in deckOfCards)
            {
                var card = await this.playCardRepository.All().FirstOrDefaultAsync(x => x.Id == item.PlayCardId);
                order.CardsInOrder.Add(card);
            }

            await this.deckOrderRepository.AddAsync(order);
            await this.deckOrderRepository.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int deckId, string cardId)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == deckId);
            var deckOfCards = this.deckOfCardsRepository.All().FirstOrDefault(x => x.DeckId == deckId && x.PlayCardId == cardId);
            var card = await this.playCardRepository.All().FirstOrDefaultAsync(x => x.Id == cardId);
            if (deck != null && card != null && !deck.IsLocked)
            {
                this.deckOfCardsRepository.Delete(deckOfCards);
                await this.deckOfCardsRepository.SaveChangesAsync();
            }

            return deckId;
        }

        public async Task<bool> HasEventCards(int id)
        {
            var deckOfCards = await this.deckOfCardsRepository.AllAsNoTracking().Where(x => x.DeckId == id).ToListAsync();

            var eventCards = await this.cardService.GetAllEventCards<CardInListViewModel>();

            bool hasEventCards = false;

            foreach (var card in deckOfCards)
            {
                foreach (var item in eventCards)
                {
                    if (card.PlayCardId == item.Id)
                    {
                        hasEventCards = true;
                    }
                }
            }

            return hasEventCards;
        }

        public async Task<int> LockDeck(int id)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (deck != null)
            {
                if (!deck.IsLocked)
                {
                    deck.IsLocked = true;
                    deck.StatusId = 2;
                }
                else
                {
                    deck.IsLocked = false;
                    deck.StatusId = 1;
                }

                await this.deckRepository.SaveChangesAsync();

                return deck.Id;
            }

            return id;
        }

        public async Task ChangeStatusAsync(SingleDeckViewModel input)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);
            deck.StatusId = input.DeckStatusId;
            await this.deckRepository.SaveChangesAsync();
        }

        public async Task<bool> HasOpenDecks(string id)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.CreatedByMemberId == id && x.IsLocked == false);

            if (deck != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateShippingAsync(SingleDeckViewModel input)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == input.Id);
            deck.StoreId = input.StoreId;
            await this.deckRepository.SaveChangesAsync();
        }

        public async Task<bool> IsLocked(int id)
        {
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return deck.IsLocked;
        }

        public async Task DeleteAsync(int id)
        {
            var deckOfCards = this.deckOfCardsRepository.All().Where(x => x.DeckId == id);
            var deck = await this.deckRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            foreach (var card in deckOfCards)
            {
                this.deckOfCardsRepository.Delete(card);
            }

            if (deck != null)
            {
             this.deckRepository.Delete(deck);
             await this.deckOfCardsRepository.SaveChangesAsync();
             await this.deckRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> HasUserWithIdAsync(int deckId, string userId)
        {
            return await this.deckRepository.AllAsNoTracking()
               .AnyAsync(a => a.Id == deckId && a.CreatedByMemberId == userId);
        }

    }
}
