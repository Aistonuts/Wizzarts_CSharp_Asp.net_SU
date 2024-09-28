namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using System.Threading.Tasks;
    using System;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Wizzarts.Common;
    using Wizzarts.Web.ViewModels.Art;

    public class EventController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventService eventService;
        private readonly IStoreService storeService;
        private readonly IWebHostEnvironment environment;

        public EventController(
            UserManager<ApplicationUser> userManager,
            IEventService eventService,
            IStoreService storeService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.eventService = eventService;
            this.storeService = storeService;
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

        public IActionResult ById(int id, int pageId = 1)
        {
            var newEvent = this.eventService.GetById<SingleEventViewModel>(id);
            newEvent.EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
            newEvent.EventId = id;
            return this.View(newEvent);
        }

        public IActionResult Create()
        {

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.eventService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfully.";

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var inputModel = this.eventService.GetById<EditEventViewModel>(id);

            if (inputModel != null)
            {
                return this.View(inputModel);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel inputModel, int id)
        {
            if (!this.ModelState.IsValid)
            {

                return this.View(inputModel);
            }

            if (await this.eventService.EventExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.eventService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.eventService.UpdateAsync(inputModel, id);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveEvent(int id)
        {
            var userId = await this.eventService.ApproveEvent(id);
            if (userId != null)
            {
                return this.RedirectToAction("ById", "User", new { id = $"{userId}", Area = "Administration" });
            }
            else
            {
                return this.BadRequest();
            }

        }
    }
}
