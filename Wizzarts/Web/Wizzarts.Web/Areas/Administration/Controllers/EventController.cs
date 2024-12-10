namespace Wizzarts.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateEventViewModel
            {
                Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3),
                TagHelpControllers = await this.eventService.GetAllTagHelpControllers<SingleTagHelpControllerViewModel>(),
                TagHelperActions = await this.eventService.GetAllTagHelpActions<SingleTagHelperActionViewModel>(),
                EventCategories = await this.eventService.GetAllEventCategories<EventCategoryInListViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (input.ControllerId == PlayCardControllerId && input.ActionId == CreateActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible Combination PlayCard and Create");
            }

            if (input.ActionId == ByIdActionId && input.ControllerId != EventControllerId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible Controller Action");
            }

            if (input.ActionId == CreateActionId && input.CategoryId != RedirectType)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible combination");
            }

            if (input.CategoryId == ImageType && input.ActionId != ByIdActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible combination");
            }

            if (input.CategoryId == TextType && input.ActionId != ByIdActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible combination");
            }

            if (input.CategoryId == FlavorlessType && input.ActionId != ByIdActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible combination");
            }

            if (input.CategoryId == ImagelessType && input.ActionId != ByIdActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible combination");
            }

            if (input.ControllerId == PlayCardControllerId && input.ActionId == CreateActionId)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Impossible Combination PlayCard and Create");
            }

            if (await this.eventService.TagHelpControllerExist(input.ControllerId) == false)
            {
                this.ModelState.AddModelError(nameof(input.ControllerId), "Controller title does not exist.");
            }

            if (await this.eventService.TagHelpActionExist(input.ActionId) == false)
            {
                this.ModelState.AddModelError(nameof(input.ActionId), "Action title does not exist.");
            }

            if (await this.eventService.EventCategoryExist(input.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(input.ControllerId), "Category title does not exist.");
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

            if (currentRole.Contains(PremiumRoleName) || currentRole.Contains(ArtistRoleName))
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
            var newEvent = await this.eventService.GetById<MyEventSettingsViewModel>(id);
            newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(id);
            newEvent.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
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
        public async Task<IActionResult> My(MyEventSettingsViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            var user = await this.userManager.GetUserAsync(this.User);
            var newEvent = await this.eventService.GetById<MyEventSettingsViewModel>(input.EventId);
            if (await this.eventService.EventTypeRequireArt(input.EventId) == true && input.Image == null)
            {
                this.ModelState.AddModelError(nameof(input.Image), "Event content require image.");
            }

            if (input.EventCategoryId == ImagelessType || input.EventCategoryId == FlavorlessType)
            {
                input.ControllerId = PlayCardControllerId;
                input.ActionId = CreateActionId;
            }

            if (!this.ModelState.IsValid)
            {
                newEvent.EventComponents = await this.eventService.GetAllEventComponents<EventComponentsInListViewModel>(input.EventId);
                newEvent.Events = await this.eventService.GetAllEventsByUserId<EventInListViewModel>(this.User.GetId(), 1, 3);
                newEvent.EventId = input.EventId;
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
            return this.RedirectToAction(nameof(this.My), new { id });
        }
    }
}
