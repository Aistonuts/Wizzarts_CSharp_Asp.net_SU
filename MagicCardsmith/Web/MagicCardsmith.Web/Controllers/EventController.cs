namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardTesting;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Stores;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class EventController : Controller
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

        public IActionResult All()
        {
            var viewModel = new EventListViewModel
            {
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var newEvent = this.eventService.GetById<SingleEventViewModel>(id);
            newEvent.EventMilestones = this.eventService.GetAllMilestones<MilestonesInListViewModel>(id);
            newEvent.EventId = id;
            newEvent.EventCards = this.eventService.GetAllEventCards<EventCardsInListViewModel>();
            newEvent.GameExpansions = this.expansionService.GetAll<ExpansionInListViewModel>();
            newEvent.Cards = this.cardService.GetAllCardsByExpansionId<CardInListViewModel>(3);
            return this.View(newEvent);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateStore (CreateStoreInputModel input)
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