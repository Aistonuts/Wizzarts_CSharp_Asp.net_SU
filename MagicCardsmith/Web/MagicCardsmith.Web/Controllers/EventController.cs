namespace MagicCardsmith.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.Infrastructure.Extensions;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardTesting;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventService eventService;
        private readonly ICardService cardService;
        private readonly IStoreService storeService;
        private readonly IExpansionService expansionService;
        private readonly IWebHostEnvironment environment;

        public EventController(
            UserManager<ApplicationUser> userManager,
            IEventService eventService,
            ICardService cardService,
            IStoreService storeService,
            IExpansionService expansionService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.eventService = eventService;
            this.cardService = cardService;
            this.storeService = storeService;
            this.expansionService = expansionService;
            this.environment = environment;

        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = new EventListViewModel
            {
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id, string information)
        {
            var newEvent = this.eventService.GetById<SingleEventViewModel>(id);
            newEvent.EventMilestones = this.eventService.GetAllMilestones<MilestonesInListViewModel>(id);
            newEvent.EventId = id;
            newEvent.EventCards = this.eventService.GetAllEventCards<EventCardsInListViewModel>();
            newEvent.GameExpansions = this.expansionService.GetAll<ExpansionInListViewModel>();
            newEvent.Cards = this.cardService.GetAllCardsByExpansionId<CardInListViewModel>(3);

            if (information != newEvent.GetEventTitle())
            {
                return this.BadRequest(information);
            }

            return this.View(newEvent);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateEventViewModel();
            viewModel.EventStatuses = this.eventService.GetAllStatuses<EventStatusInListViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEventViewModel input)
        {
            if (!this.ModelState.IsValid)
            {

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.eventService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(SingleEventViewModel input)
        {
            if (!this.ModelState.IsValid)
            {

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.storeService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Store added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveEvent(int id)
        {
            await this.eventService.ApproveEvent(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
