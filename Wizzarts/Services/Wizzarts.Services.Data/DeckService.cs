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
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<DeckStatus> deckStatusRepository;
        private readonly IDeletableEntityRepository<DeckOfCards> deckOfCardsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IPlayCardService cardService;
        private readonly IArtService artService;

        public DeckService(
            IDeletableEntityRepository<CardDeck> deckRepository,
            IDeletableEntityRepository<Order> deckOrderRepository,
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<PlayCard> playCardRepository,
            IDeletableEntityRepository<DeckOfCards> deckOfCardsRepository,
            IDeletableEntityRepository<DeckStatus> deckStatusRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IPlayCardService cardService,
            IArtService artService)
        {
            this.deckRepository = deckRepository;
            this.deckOrderRepository = deckOrderRepository;
            this.eventRepository = eventRepository;
            this.playCardRepository = playCardRepository;
            this.deckOfCardsRepository = deckOfCardsRepository;
            this.deckStatusRepository = deckStatusRepository;
            this.userRepository = userRepository;
            this.cardService = cardService;
            this.artService = artService;
        }

        public async Task CreateAsync(CreateDeckViewModel input, string userId, string imagePath)
        {
            var arts = this.artService.GetRandom<ArtInListViewModel>(1);
            var artUrl = arts.FirstOrDefault()?.ImageUrl;
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == input.EventId);

            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var deck = new CardDeck
            {
                Name = input.Name,
                Description = input.Description,

                StoreId = input.StoreId,
                CreatedByMemberId = userId,
                StatusId = 1,
                ImageUrl = artUrl,
                IsLocked = false,
            };

            if (currentEvent != null)
            {
                currentEvent.Participants.Add(user);
            }

            await this.eventRepository.SaveChangesAsync();

            await this.deckRepository.AddAsync(deck);
            await this.deckRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditDeckViewModel input, int id)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == input.Id);
            if (deck != null)
            {
                deck.Name = input.Name;
                deck.Description = input.Description;
                deck.StoreId = input.Id;

                await this.deckRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            var decks = this.deckRepository.AllAsNoTracking()
                .Where(x => x.IsLocked == true)
                .To<T>().ToList();
            return decks;
        }

        public IEnumerable<T> GetAllDecksByUserId<T>(string id)
        {
            var decks = this.deckRepository.AllAsNoTracking()
                .Where(x => x.CreatedByMemberId == id)
                .To<T>().ToList();

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
            var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == cardId);

            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == deckId);
            if (deck != null && card != null && !deck.IsLocked)
            {
                deck.PlayCards.Add(card);
                await this.deckRepository.SaveChangesAsync();
            }

            return deckId;
        }

        public IEnumerable<T> GetAllCardsInDeckId<T>(int id)
        {
            var listOfCards = new List<T>();
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().Where(x => x.DeckId == id);
            foreach (var item in deckOfCards)
            {
                var card = this.cardService.GetById<T>(item.PlayCardId);
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
                IsCustomOrder = true,
                DeckImageUrl = deck.ImageUrl,
                RecipientId = userId,
                Description = deck.Description,
                EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40)),
                ShippingAddress = deck.DeliveryLocation,
            };

            foreach (var item in deckOfCards)
            {
                var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == item.PlayCardId);
                order.CardsInOrder.Add(card);
            }

            await this.deckOrderRepository.AddAsync(order);
            await this.deckOrderRepository.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(int deckId, string cardId)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == deckId);
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().FirstOrDefault(x => x.DeckId == deckId && x.PlayCardId == cardId);
            var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == cardId);
            if (deck != null && card != null && !deck.IsLocked)
            {
                this.deckOfCardsRepository.Delete(deckOfCards);
                await this.deckOfCardsRepository.SaveChangesAsync();
            }

            return deckId;
        }

        public bool HasEventCards(int id)
        {
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().Where(x => x.DeckId == id);

            var eventCards = this.cardService.GetAllEventCards<CardInListViewModel>();

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
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == id);

            if (deck != null && !deck.IsLocked)
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

        public IEnumerable<DeckStatusListViewModel> GetAllDeckStatuses()
        {
            var deckStatuses = this.deckStatusRepository
                .AllAsNoTracking()
                .Select(x => new DeckStatusListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Id)
               .ToList();

            return deckStatuses;
        }

        public async Task ChangeStatusAsync(SingleDeckViewModel input)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == input.Id);
            deck.StatusId = input.DeckStatusId;
            await this.deckRepository.SaveChangesAsync();
        }

        public bool HasOpenDecks(string id)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.CreatedByMemberId == id && x.IsLocked == false);

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
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == input.Id);
            deck.StoreId = input.StoreId;
            await this.deckRepository.SaveChangesAsync();
        }
    }
}
