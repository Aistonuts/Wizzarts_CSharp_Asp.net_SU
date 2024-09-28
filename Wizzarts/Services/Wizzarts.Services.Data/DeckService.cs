using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Common.Repositories;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;
using Wizzarts.Web.ViewModels.Art;
using Wizzarts.Web.ViewModels.Deck;
using Wizzarts.Web.ViewModels.PlayCard;

namespace Wizzarts.Services.Data
{
    public class DeckService : IDeckService
    {
        private readonly IDeletableEntityRepository<CardDeck> deckRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<DeckStatus> deckStatusRepository;
        private readonly IDeletableEntityRepository<DeckOfCards> deckOfCardsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IPlayCardService cardService;
        private readonly IArtService artService;

        public DeckService(
            IDeletableEntityRepository<CardDeck> deckRepository,
            IDeletableEntityRepository<Event> eventRepository,
            IDeletableEntityRepository<PlayCard> playCardRepository,
            IDeletableEntityRepository<DeckOfCards> deckOfCardsRepostiory,
            IDeletableEntityRepository<DeckStatus> deckStatusRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IPlayCardService cardService,
            IArtService artService)
        {
            this.deckRepository = deckRepository;
            this.eventRepository = eventRepository;
            this.playCardRepository = playCardRepository;
            this.deckOfCardsRepository = deckOfCardsRepostiory;
            this.deckStatusRepository = deckStatusRepository;
            this.userRepository = userRepository;
            this.cardService = cardService;
            this.artService = artService;

        }

        public async Task CreateAsync(CreateDeckViewModel input, string userId, string imagePath)
        {
            var arts = this.artService.GetRandom<ArtInListViewModel>(1);
            string artUrl = arts.FirstOrDefault().ImageUrl;
            var currentEvent = this.eventRepository.All().FirstOrDefault(x => x.Id == input.EventId);

            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var deck = new CardDeck
            {
                Name = input.Name,
                Description = input.Description,
                ShippingAddress = input.ShippingAddress,
                StoreId = input.StoreId,
                CreatedByMemberId = userId,
                StatusId = 1,
                ImageUrl = artUrl,
            };

            currentEvent.Participants.Add(user);
            await this.eventRepository.SaveChangesAsync();

            await this.deckRepository.AddAsync(deck);
            await this.deckRepository.SaveChangesAsync();
        }

        public Task UpdateAsync(EditDeckViewModel input, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var decks = this.deckRepository.AllAsNoTracking()
                .Where(x=>x.IsLocked == true)
                .To<T>().ToList();
            return decks;
        }

        public IEnumerable<T> GetAllDecksByUserId<T>(string id)
        {
            var decks = this.deckRepository.AllAsNoTracking()
                .Where( x => x.CreatedByMemberId == id)
                .To<T>().ToList();

            return decks;
        }

        public T GetById<T>(int id)
        {
            var deck = this.deckRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return deck;
        }

        public async Task<int> AddAsync(int deckId, string cardId)
        {
            var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == cardId);

            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == deckId);
            if (!deck.IsLocked)
            {
                deck.PlayCards.Add(card);
                await this.deckRepository.SaveChangesAsync();
            }
            return deckId;
        }

        public IEnumerable<T> GetAllCardsInDeckId<T>(int id)
        {
            var listOfCards = new List<T>();
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().Where( x => x.DeckId == id);
            foreach (var item in deckOfCards)
            {
                var card = this.cardService.GetById<T>(item.PlayCardId);
                listOfCards.Add(card);
            }

            return listOfCards;
        }

        public async Task<int> RemoveAsync(int deckId, string cardId)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == deckId);
            var deckOfCards = this.deckOfCardsRepository.AllAsNoTracking().FirstOrDefault(x => x.DeckId == deckId && x.PlayCardId == cardId);
            var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == cardId);
            if (!deck.IsLocked)
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
                    if(card.PlayCardId == item.Id)
                    {
                        hasEventCards = true;
                    }
                }
            }

            return hasEventCards;
        }

        public async Task ChangeStatusAsync(int id)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == id);

            if (deck.StatusId == 1)
            {
                deck.StatusId = 2;
            }
           else if (deck.StatusId == 2)
            {
                deck.StatusId = 1;
            }

            await this.deckRepository.SaveChangesAsync();
        }

        public async Task<int> LockDeck(int id)
        {
            var deck = this.deckRepository.All().FirstOrDefault(x => x.Id == id);

            if (!deck.IsLocked)
            {
                deck.IsLocked = true;

            }
            else
            {
                deck.IsLocked = false;

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
    }
}