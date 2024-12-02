namespace Wizzarts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.CardGameExpansion;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> deckOrderRepository;
        private readonly IDeletableEntityRepository<CardOrder> cardsInOrderRepository;
        private readonly IPlayCardService cardService;
        private readonly IPlayCardExpansionService expansionService;
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;

        public OrderService(
            IDeletableEntityRepository<Order> deckOrderRepository,
            IDeletableEntityRepository<CardOrder> cardsInOrderRepository,
            IPlayCardService cardService,
            IPlayCardExpansionService expansionService,
            IDeletableEntityRepository<PlayCard> playCardRepository)
        {
            this.deckOrderRepository = deckOrderRepository;
            this.cardsInOrderRepository = cardsInOrderRepository;
            this.cardService = cardService;
            this.expansionService = expansionService;
            this.playCardRepository = playCardRepository;
        }

        public async Task CancelOrder(int id)
        {
            var deckOfCards = this.cardsInOrderRepository.All().Where(x => x.OrderId == id);
            var order = await this.deckOrderRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            foreach (var card in deckOfCards)
            {
                this.cardsInOrderRepository.Delete(card);
            }

            this.deckOrderRepository.Delete(order);
            await this.cardsInOrderRepository.SaveChangesAsync();
            await this.deckOrderRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var orders = await this.deckOrderRepository.AllAsNoTracking()
                .To<T>().ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<T>> GetAllCardsInOrderId<T>(int id)
        {
            var listOfCards = new List<T>();
            var deckOfCards = await this.cardsInOrderRepository.AllAsNoTracking().Where(x => x.OrderId == id).ToListAsync();

            foreach (var item in deckOfCards)
            {
                var card = await this.cardService.GetById<T>(item.PlayCardId);
                listOfCards.Add(card);
            }

            return listOfCards;
        }

        public async Task<IEnumerable<T>> GetAllOrdersByUserId<T>(string id)
        {
            var orders = await this.deckOrderRepository.AllAsNoTracking()
                .Where(x => x.RecipientId == id)
                 .To<T>().ToListAsync();
            return orders;
        }

        public Task<T> GetById<T>(int id)
        {
            var order = this.deckOrderRepository.AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>().FirstOrDefaultAsync();

            return order;
        }

        public async Task<bool> HasUserWithIdAsync(int orderId, string userId)
        {
            return await this.deckOrderRepository.AllAsNoTracking()
               .AnyAsync(a => a.Id == orderId && a.RecipientId == userId);
        }

        // public async Task OrderAsync(SingleExpansionViewModel input, string userId)
        // {

        // var deckOfCards = this.cardService.GetAllCardsByExpansion<CardInListViewModel>(input.Id);
        //    var expansion = this.expansionService.GetById<ExpansionInListViewModel>(input.Id);
        //    var order = new Order()
        //    {
        //        Title = expansion.Title,
        //        OrderStatusId = 1,
        //        IsCustomOrder = false,
        //        DeckImageUrl = expansion.ExpansionSymbolUrl,
        //        RecipientId = userId,
        //        Description = expansion.Description,
        //        EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40)),
        //    };

        // foreach (var item in deckOfCards)
        //    {
        //        var card = this.playCardRepository.All().FirstOrDefault(x => x.Id == item.Id);
        //        order.CardsInOrder.Add(card);
        //    }

        // await this.deckOrderRepository.AddAsync(order);
        //    await this.deckOrderRepository.SaveChangesAsync();
        // }
        public async Task PauseOrder(int id)
        {
            var order = await this.deckOrderRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            order.OrderStatusId = 1;
            await this.deckOrderRepository.SaveChangesAsync();
        }

        public async Task ReadyOrder(int id)
        {
            var order = await this.deckOrderRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            order.OrderStatusId = 2;
            await this.deckOrderRepository.SaveChangesAsync();
        }

        public async Task ShipOrder(int id)
        {
            var order = await this.deckOrderRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            order.OrderStatusId = 3;
            await this.deckOrderRepository.SaveChangesAsync();
        }
    }
}
