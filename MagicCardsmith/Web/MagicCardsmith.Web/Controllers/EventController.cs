namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardTesting;
    using MagicCardsmith.Web.ViewModels.Event;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventService eventService;
        private readonly ICardService cardService;

        public EventController(
            UserManager<ApplicationUser> userManager,
            IEventService eventService,
            ICardService cardService)
        {
            this.userManager = userManager;
            this.eventService = eventService;
            this.cardService = cardService;

        }

        public IActionResult All()
        {
            var viewModel = new EventListViewModel
            {
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id, int pageId = 1)
        {

            var newEvent = this.eventService.GetById<SingleEventViewModel>(id);
            newEvent.EventMilestones = this.eventService.GetAllMilestones<MilestonesInListViewModel>(id);
            newEvent.EventId = id;
            newEvent.EventCards = this.eventService.GetAllEventCards<EventCardsInListViewModel>();
            return this.View(newEvent);
        }
    }
}