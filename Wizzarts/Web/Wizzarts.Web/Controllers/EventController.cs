namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Event;

    using static Wizzarts.Common.GlobalConstants;

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

        [AllowAnonymous]
        public IActionResult ByUser()
        {
            var viewModel = new EventListViewModel
            {
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id, int pageId = 1)
        {
            if (id == 3)
            {
                return this.RedirectToAction("Create", "Store", new { id = id });
            }
            else if (id == 4)
            {
                return this.RedirectToAction("Create", "Deck", new { id = id });
            }
            else
            {
                var newEvent = this.eventService.GetById<SingleEventViewModel>(id);
                newEvent.EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
                newEvent.EventId = id;
                return this.View(newEvent);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateEventViewModel
            {
                Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3),
            };
            return this.View(viewModel);
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
            var currentRole = await this.userManager.GetRolesAsync(user);
            bool isContentCreator = false;

            if(currentRole.Contains(AdministratorRoleName) || currentRole.Contains(AdministratorRoleName))
            {
                isContentCreator = true;
            }
            try
            {
                await this.eventService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images", isContentCreator);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfully.";

            return this.RedirectToAction("Create", "Event");
        }

        public async Task<IActionResult> My(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var newEvent = this.eventService.GetById<MyEventSettingsViewModel>(id);
            newEvent.EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
            newEvent.Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
            newEvent.EventId = id;
            newEvent.CreatorAvatar = user.AvatarUrl;
            newEvent.OwnerBrowsing = false;
            bool isOwner = await this.eventService.HasUserWithIdAsync(newEvent.EventId, this.User.GetId());
            if (isOwner)
            {
                newEvent.OwnerBrowsing = true;
            }

            return this.View(newEvent);
        }

        [HttpPost]
        public async Task<IActionResult> My(MyEventSettingsViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                var newEvent = this.eventService.GetById<MyEventSettingsViewModel>(input.EventId);
                newEvent.EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(input.EventId);
                newEvent.Events = this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
                newEvent.EventId = input.EventId;
                newEvent.CreatorAvatar = user.AvatarUrl;
                newEvent.OwnerBrowsing = false;
                bool isOwner = await this.eventService.HasUserWithIdAsync(newEvent.EventId, this.User.GetId());
                if (isOwner)
                {
                    newEvent.OwnerBrowsing = true;
                }

                return this.View(newEvent);
            }

            try
            {
                await this.eventService.AddComponentAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfully.";

            return this.RedirectToAction(nameof(this.My), new { id = input.EventId });
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.eventService.GetById<EditEventViewModel>(id);

            if (inputModel != null)
            {
                inputModel.EventComponents = this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
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
                return this.RedirectToAction("ById", "Member", new { id = $"{userId}", Area = "Administration" });
            }
            else
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await this.eventService.EventExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.eventService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.eventService.DeleteAsync(id);

            return this.RedirectToAction("MyData", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            if (await this.eventService.EventComponentExist(id) == false)
            {
                return this.BadRequest();
            }

            var currentEventComponent = this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);

            if (await this.eventService.HasUserWithIdAsync(currentEventComponent.EventId, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.eventService.DeleteComponentAsync(id);

            return this.RedirectToAction(nameof(this.Edit), new { id });
        }
    }
}
