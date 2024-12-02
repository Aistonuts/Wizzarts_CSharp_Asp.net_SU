namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Services.Data;
    using Wizzarts.Services.Messaging;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Order;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class OrderController : Controller
    {
        private readonly IEventService eventService;
        private readonly IDeckService deckService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IEmailSender emailSender;

        public OrderController(
              IEventService eventService,
              IDeckService deckService,
              IOrderService orderService,
              IUserService userService,
              IEmailSender emailSender)
        {
            this.eventService = eventService;
            this.deckService = deckService;
            this.orderService = orderService;
            this.userService = userService;
            this.emailSender = emailSender;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> All()
        {
            var viewModel = new OrderListViewModel
            {
                Orders = await this.orderService.GetAll<OrderInListViewModel>(),
                Events = await this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            if (order == null)
            {
                return this.BadRequest();
            }

            order.Cards = await this.orderService.GetAllCardsInOrderId<CardInListViewModel>(id);
            order.Decks = await this.deckService.GetAll<DeckInListViewModel>();
            return this.View(order);
        }

        public async Task<IActionResult> My()
        {
            var viewModel = new OrderListViewModel
            {
                Orders = await this.orderService.GetAllOrdersByUserId<OrderInListViewModel>(this.User.GetId()),
                Decks = await this.deckService.GetAll<DeckInListViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Pause(int id)
        {
            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            if (order != null)
            {
                await this.orderService.PauseOrder(id);
            }

            return this.RedirectToAction("All", "Order");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Ship(int id)
        {
            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            if (order != null)
            {
                await this.orderService.ShipOrder(id);
            }

            return this.RedirectToAction("All", "Order");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Ready(int id)
        {
            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            if (order != null)
            {
                await this.orderService.ReadyOrder(id);
            }

            return this.RedirectToAction("All", "Order");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            if (await this.orderService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            if (order != null)
            {
                await this.orderService.CancelOrder(id);
            }

            return this.RedirectToAction("All", "Deck");
        }

        public async Task<IActionResult> SendToEmail(int id)
        {
            var order = await this.orderService.GetById<OrderInListViewModel>(id);
            var html = new StringBuilder();
            var user = await this.userService.GetById<SingleMemberViewModel>(order.RecipientId);
            var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", string.Empty);
            html.AppendLine($"<h1>{order.Title}</h1>");
            html.AppendLine($"<h3>{order.OrderStatus}</h3>");
            html.AppendLine($"<h3>Confirmation key : {uid}</h3>");
            html.AppendLine($"<h3>To secure the dispatch of your deck and your place at the event, provide us with the confirmation key by mail, chat or phone.</h3>");
            html.AppendLine($"<img src=\"{order.DeckImageUrl}\" />");
            await this.emailSender.SendEmailAsync("drawgoon@aol.com", "Wizzarts", user.Email, order.Title, html.ToString());
            return this.RedirectToAction("All", "Order");
        }
    }
}
