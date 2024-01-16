namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Event;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventService eventService;

        public EventController(
            UserManager<ApplicationUser> userManager,
            IEventService eventService)
        {
            this.userManager = userManager;
            this.eventService = eventService;
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
            return this.View(newEvent);
        }
    }
}