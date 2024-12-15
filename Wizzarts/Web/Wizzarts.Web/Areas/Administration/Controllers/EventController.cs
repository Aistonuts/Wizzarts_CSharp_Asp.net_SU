namespace Wizzarts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Controllers;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.TagHelper;

    using static Wizzarts.Common.GlobalConstants;
    using static Wizzarts.Common.HardCodedConstants;

    public class EventController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminEventService eventService;
        private readonly IStoreService storeService;
        private readonly IWebHostEnvironment environment;

        public EventController(
            UserManager<ApplicationUser> userManager,
            IAdminEventService eventService,
            IStoreService storeService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.eventService = eventService;
            this.storeService = storeService;
            this.environment = environment;
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> ApproveEvent(int id)
        {
            var userId = await this.eventService.ApproveEvent(id);
            if (userId != null)
            {
                return this.RedirectToAction("Mine", "Event", new { id = $"{id}", area = "Administration" });
            }
            else
            {
                return this.RedirectToAction("Mine", "Event", new { id = $"{id}", area = "Administration" });
            }
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]/{id}")]
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

            return this.RedirectToAction("All", "Event", new { area = "Administration" } );
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            if (await this.eventService.EventComponentExist(id) == false)
            {
                return this.BadRequest();
            }

            var currentEventComponent = await this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);
            var eventId = currentEventComponent.EventId;
            if (await this.eventService.HasUserWithIdAsync(currentEventComponent.EventId, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.eventService.DeleteComponentAsync(id);

            return this.RedirectToAction(nameof(this.Edit), new { id = eventId });
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> Promote(int id)
        {
            var userId = await this.eventService.PromoteEvent(id);
            if (userId != null)
            {
                return this.RedirectToAction("Mine", "Event", new { id = $"{id}", Area = "Administration" });
            }
            else
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Publish()
        {
            var viewModel = new CreateEventViewModel
            {
                Events = await this.eventService.GetAllPaginationless<EventInListViewModel>(),
                TagHelpControllers = await this.eventService.GetAllTagHelpControllers<SingleTagHelpControllerViewModel>(),
                TagHelperActions = await this.eventService.GetAllTagHelpActions<SingleTagHelperActionViewModel>(),
                EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Publish(CreateEventViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (await this.eventService.EventCategoryExist(input.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(input.ControllerId), "Category does not exist.");
            }

            if (!this.ModelState.IsValid)
            {
                input.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
                input.TagHelpControllers = await this.eventService.GetAllTagHelpControllers<SingleTagHelpControllerViewModel>();
                input.TagHelperActions = await this.eventService.GetAllTagHelpActions<SingleTagHelperActionViewModel>();
                input.EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var currentRole = await this.userManager.GetRolesAsync(user);
            bool isContentCreator = false;

            if (currentRole.Contains(PremiumRoleName) || currentRole.Contains(ArtistRoleName) || currentRole.Contains(AdministratorRoleName))
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

            return this.RedirectToAction("Publish", "Event", new { area = "Administration" });
        }

        [HttpGet]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> Mine(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var newEvent = await this.eventService.GetById<MyEventSettingsViewModel>(id);
            newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
            newEvent.Events = await this.eventService.GetAllEventsByUserIdPageless<EventInListViewModel>(this.User.GetId());
            newEvent.EventId = id;
            newEvent.CreatorAvatar = user.AvatarUrl;
            newEvent.OwnerBrowsing = false;
            newEvent.EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>();
            bool isOwner = await this.eventService.HasUserWithIdAsync(newEvent.EventId, this.User.GetId());
            if (isOwner)
            {
                newEvent.OwnerBrowsing = true;
            }

            return this.View(newEvent);
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> Mine(MyEventSettingsViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            var user = await this.userManager.GetUserAsync(this.User);
            var newEvent = await this.eventService.GetById<MyEventSettingsViewModel>(input.EventId);
            if (await this.eventService.EventTypeRequireArt(input.EventId) == true && input.Image == null)
            {
                this.ModelState.AddModelError(nameof(input.Image), "Event content require image.");
            }

            if (!this.ModelState.IsValid)
            {
                newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(input.EventId);
                newEvent.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
                newEvent.EventId = input.EventId;
                newEvent.CreatorAvatar = user.AvatarUrl;
                newEvent.OwnerBrowsing = false;
                newEvent.EventCategoryId = input.EventCategoryId;
                newEvent.EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>();
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
                newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(input.EventId);
                newEvent.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
                newEvent.EventId = input.EventId;
                newEvent.CreatorAvatar = user.AvatarUrl;
                newEvent.OwnerBrowsing = false;
                newEvent.EventCategoryId = input.EventCategoryId;
                newEvent.EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>();
                bool isOwner = await this.eventService.HasUserWithIdAsync(newEvent.EventId, this.User.GetId());
                if (isOwner)
                {
                    newEvent.OwnerBrowsing = true;
                }
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(input);
            }

            this.TempData["Message"] = "Event added successfully.";

            return this.RedirectToAction(nameof(this.Mine), new { id = input.EventId, area= "Administration"});
        }

        [HttpGet]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = await this.eventService.GetById<EditEventViewModel>(id);

            if (inputModel != null)
            {
                inputModel.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
                return this.View(inputModel);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> Edit(EditEventViewModel inputModel, int id)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            if (!this.ModelState.IsValid)
            {
                inputModel.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
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
            return this.RedirectToAction(nameof(this.Mine), new { id });
        }

        [HttpGet]
        [Route("[area]/[controller]/[action]/{id}")]
        public async Task<IActionResult> ById(int id, int pageId = 1)
        {
            var newEvent = await this.eventService.GetById<SingleEventViewModel>(id);

            newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
            newEvent.EventId = id;
            return this.View(newEvent);
        }

        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 4;
            var viewModel = new EventListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = await this.eventService.GetCount(),
                Events = await this.eventService.GetAll<EventInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
