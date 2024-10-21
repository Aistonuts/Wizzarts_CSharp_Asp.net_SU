namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;

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

        [HttpGet]
        public async Task<IActionResult> Add(SingleDeckViewModel input, int Id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return RedirectToAction("Create", "Deck");
            }

            input.Id = Id;
            input.Cards = this.cardService.GetAllCardsByCriteria<CardInListViewModel>(input);
            input.Decks = decks;
            input.Cards = this.deckService.GetAllCardsInDeckId<CardInListViewModel>(Id);
            input.SelectType = this.playCardComponentsService.GetAllCardType();

            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Dispatch(SingleDeckViewModel input)
        {

            await this.deckService.ChangeStatusAsync(input);

            return this.RedirectToAction(nameof(this.ById), new { id = input.Id });
        }

        public async Task<IActionResult> AddCard(string data, int Id)
        {

            var decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int currentDeckId = await this.deckService.AddAsync(Id, data);

            return this.RedirectToAction(nameof(this.Add), new { id = currentDeckId });
        }

        public async Task<IActionResult> Remove(string data, int Id)
        {

            var decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int currentDeckId = await deckService.RemoveAsync(Id, data);

            return this.RedirectToAction(nameof(this.Add), new { id = currentDeckId });
        }

        [HttpGet]
        public IActionResult Create(int id = GameTestersEventValue)
        {
            var viewModel = new CreateDeckViewModel
            {
                Decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId()),
                Stores = this.storeService.GetAll<StoreInListViewModel>(),
                EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id),
                EventId = id,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDeckViewModel model)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                model.Decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
                model.Stores = this.storeService.GetAll<StoreInListViewModel>();
                return this.View(model);
            }

            try
            {
                await this.deckService.CreateAsync(model, this.User.GetId(), $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
                model.Stores = this.storeService.GetAll<StoreInListViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = "Deck added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("Create", "Deck");
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = new DeckListViewModel
            {
                Decks = this.deckService.GetAll<DeckInListViewModel>(),
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }


        public async Task<IActionResult> Change(int Id)
        {

            var decks = this.deckService.GetAllDecksByUserId<DeckInListViewModel>(this.User.GetId());
            if (!decks.Any())
            {
                return this.RedirectToAction("Create", "Deck");
            }

            int deckId = await this.deckService.LockDeck(Id);

            return this.RedirectToAction(nameof(this.Add), new { id = deckId });
        }

        public IActionResult ById(int id)
        {
            var deck = this.deckService.GetById<SingleDeckViewModel>(id);
            //if (information != art.GetInformation())
            //{
            //    return this.BadRequest(information);
            //}
            deck.DeckStatuses = this.deckService.GetAllDeckStatuses();
            deck.Cards = this.deckService.GetAllCardsInDeckId<CardInListViewModel>(id);
            deck.Decks = this.deckService.GetAll<DeckInListViewModel>();
            return this.View(deck);
        }
    }
}
