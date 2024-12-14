namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;

    using static Wizzarts.Common.GlobalConstants;
    using static Wizzarts.Common.HardCodedConstants;

    public class DeckController : BaseController
    {
        private readonly IPlayCardService cardService;
        private readonly IArticleService articleService;
        private readonly IEventService eventService;
        private readonly IPlayCardComponentsService playCardComponentsService;
        private readonly IStoreService storeService;
        private readonly IDeckService deckService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public DeckController(
        IPlayCardService cardService,
        IArticleService articleService,
        IEventService eventService,
        IPlayCardComponentsService playCardComponentsService,
        IStoreService storeService,
        IDeckService deckService,
        UserManager<ApplicationUser> userManager,
        IWebHostEnvironment environment)
        {
            this.cardService = cardService;
            this.articleService = articleService;
            this.eventService = eventService;
            this.playCardComponentsService = playCardComponentsService;
            this.storeService = storeService;
            this.deckService = deckService;
            this.userManager = userManager;
            this.environment = environment;
        }

        // this is a page where on the left are the play cards from the db, on the right is the deck created by the user. Clicking on each card will place it the user's deck
        public async Task<IActionResult> Add(SingleDeckViewModel input, int id, string information)
        {
            var deck = await this.deckService.GetById<SingleDeckViewModel>(id);
            if (deck == null)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var currentRole = await this.userManager.GetRolesAsync(user);
            if (!currentRole.Contains(AdministratorRoleName) && deck.CreatedByMember != user.UserName)
            {
                return this.Unauthorized();
            }

            var decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());

            input.Id = id;
            input.Name = deck.Name;
            input.Cards = await this.cardService.GetAllCardsByCriteria<CardInListViewModel>(input);
            input.Decks = decks;
            input.CardsInDeck = await this.deckService.GetAllCardsInDeckId<CardInListViewModel>(id);
            input.SelectType = await this.playCardComponentsService.GetAllCardType();
            input.DeckStatus = deck.DeckStatus;
            input.DeliveryLocation = deck.DeliveryLocation;
            input.IsLocked = deck.IsLocked;
            return this.View(input);
        }


        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Dispatch(SingleDeckViewModel input)
        {
            await this.deckService.ChangeStatusAsync(input);

            return this.RedirectToAction(nameof(this.ById), new { id = input.Id, information = input.GetDeckName() });
        }

        [HttpPost]
        public async Task<IActionResult> Shipping(SingleDeckViewModel input)
        {
            if (await this.storeService.ExistsAsync(input.StoreId) == false)
            {
                this.ModelState.AddModelError(nameof(input.StoreId), "Store does not exist");
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            if (!this.ModelState.IsValid)
            {
                input.Decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
                input.Stores = await this.storeService.GetAll<StoreInListViewModel>();
                return this.View(input);
            }

            await this.deckService.UpdateShippingAsync(input);

            return this.RedirectToAction(nameof(this.ById), new { id = input.Id, information = input.GetDeckName() });
        }

        // this will take a copy of a card and place it the user deck
        public async Task<IActionResult> AddCard(string data, int id)
        {
            var decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int currentDeckId = await this.deckService.AddAsync(id, data);

            return this.RedirectToAction(nameof(this.Add), new { id = currentDeckId });
        }

        // this will remove the deck from the connection table (deck-play cards)
        public async Task<IActionResult> Remove(string data, int id)
        {
            var decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int currentDeckId = await this.deckService.RemoveAsync(id, data);

            return this.RedirectToAction(nameof(this.Add), new { id = currentDeckId });
        }

        // this will be used to create a new deck
        [HttpGet]
        public async Task<IActionResult> Create(int id = GameTestersEventValue)
        {
            var viewModel = new CreateDeckViewModel
            {
                Decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId()),
                Stores = await this.storeService.GetAll<StoreInListViewModel>(),
                EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id),
                EventId = id,
            };
            return this.View(viewModel);
        }

        // this will be used to create a new deck
        [HttpPost]
        public async Task<IActionResult> Create(CreateDeckViewModel model)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (await this.storeService.ExistsAsync(model.StoreId) == false)
            {
                this.ModelState.AddModelError(nameof(model.StoreId), "Store does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
                model.Stores = await this.storeService.GetAll<StoreInListViewModel>();
                return this.View(model);
            }

            try
            {
                await this.deckService.CreateAsync(model, this.User.GetId(), $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
                model.Stores = await this.storeService.GetAll<StoreInListViewModel>();

                return this.View(model);
            }

            this.TempData["Message"] = "Deck added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Create", "Deck");
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var viewModel = new DeckListViewModel
            {
                Decks = await this.deckService.GetAll<DeckInListViewModel>(),
                Events = await this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Order(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var deckStatus = await this.deckService.IsLocked(id);
            if (!deckStatus)
            {
                return this.RedirectToAction(nameof(this.Add), new { id = id });
            }

            try
            {
                await this.deckService.OrderAsync(id, this.User.GetId());
            }
            catch (Exception ex)
            {
                return this.RedirectToAction(nameof(this.Add), new { id = id });
            }

            this.TempData["Message"] = "Order added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("My", "Order");
        }

        public async Task<IActionResult> Change(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var currentRole = await this.userManager.GetRolesAsync(user);

            var decks = await this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!currentRole.Contains(AdministratorRoleName) && !decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int deckId = await this.deckService.LockDeck(id);

            return this.RedirectToAction(nameof(this.Add), new { id = deckId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> ById(int id, string information)
        {
            var deck = await this.deckService.GetById<SingleDeckViewModel>(id);
            if (deck == null || information != deck.GetDeckName())
            {
                return this.BadRequest();
            }

            deck.Stores = await this.storeService.GetAll<StoreInListViewModel>();
            deck.Cards = await this.deckService.GetAllCardsInDeckId<CardInListViewModel>(id);
            deck.Decks = await this.deckService.GetAll<DeckInListViewModel>();
            return this.View(deck);
        }
    }
}
