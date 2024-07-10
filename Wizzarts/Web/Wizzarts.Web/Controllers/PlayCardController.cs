namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Card;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public class PlayCardController : BaseController
    {
        private readonly IPlayCardService cardService;
        private readonly IPlayCardComponentsService playCardComponentsService;
        private readonly IEventService eventService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<PlayCard> cardRepository;


        public PlayCardController(
            IPlayCardService cardService,
            IPlayCardComponentsService playCardComponentsService,
            IEventService eventService,
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<PlayCard> cardRepository)
        {
            this.cardService = cardService;
            this.playCardComponentsService = playCardComponentsService;
            this.eventService = eventService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;
        }

        public async Task<IActionResult> Create(int id)
        {
            var viewModel = new CreateCardViewModel();
            viewModel.RedMana = this.playCardComponentsService.GetAllRedMana();
            viewModel.BlueMana = this.playCardComponentsService.GetAllBlueMana();
            viewModel.BlackMana = this.playCardComponentsService.GetAllBlackMana();
            viewModel.GreenMana = this.playCardComponentsService.GetAllGreenMana();
            viewModel.WhiteMana = this.playCardComponentsService.GetAllWhiteMana();
            viewModel.ColorlessMana = this.playCardComponentsService.GetAllColorlessMana();
            viewModel.SelectType = this.playCardComponentsService.GetAllCardType();
            viewModel.SelectFrameColor = this.playCardComponentsService.GetAllCardFrames();
            viewModel.SelectExpansion = this.playCardComponentsService.GetAllExpansionInListView();

            var eventComponent = this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);
            var currentEvent = this.eventService.GetById<EventInListViewModel>(eventComponent.EventId);
            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ArtByUserId = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(user.Id);

            viewModel.EventMilestoneImage = eventComponent.ImageUrl;
            viewModel.EventMilestoneTitle = eventComponent.Title;
            viewModel.EventMilestoneDescription = eventComponent.Description;
            viewModel.EventDescription = currentEvent.EventDescription;
            viewModel.EventId = eventComponent.EventId;
            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCardViewModel input, int id, string canvasCapture)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var eventComponent = this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);

            bool isEventCard = false;

            if (eventComponent != null)
            {
                isEventCard = true;
            }

            if (eventComponent != null && eventComponent.RequireArtInput)
            {
                var cardTitle = eventComponent.Title;
                var cardDescription = eventComponent.Description;
                input.Name = cardTitle;
                input.AbilitiesAndFlavor = cardDescription;
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                input.RedMana = this.playCardComponentsService.GetAllRedMana();
                input.BlueMana = this.playCardComponentsService.GetAllBlueMana();
                input.BlackMana = this.playCardComponentsService.GetAllBlackMana();
                input.GreenMana = this.playCardComponentsService.GetAllGreenMana();
                input.WhiteMana = this.playCardComponentsService.GetAllWhiteMana();
                input.ColorlessMana = this.playCardComponentsService.GetAllColorlessMana();
                input.SelectType = this.playCardComponentsService.GetAllCardType();
                input.SelectFrameColor = this.playCardComponentsService.GetAllCardFrames();
                input.SelectExpansion = this.playCardComponentsService.GetAllExpansionInListView();

                var currentEvent = this.eventService.GetById<EventInListViewModel>(eventComponent.EventId);

                input.ArtByUserId = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(user.Id);

                input.EventMilestoneImage = eventComponent.ImageUrl;
                input.EventMilestoneTitle = eventComponent.Title;
                input.EventMilestoneDescription = eventComponent.Description;
                input.EventDescription = currentEvent.EventDescription;
                input.EventId = eventComponent.EventId;

                return this.View(input);
            }

            if (!this.artService.IsBase64String(canvasCapture))
            {
                throw new Exception($"Invalid Base64String {canvasCapture}");
            }

            try
            {
                await this.cardService.CreateAsync(input, user.Id, eventComponent.EventId, $"{this.environment.WebRootPath}/Images", isEventCard, eventComponent.RequireArtInput, canvasCapture);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Card added successfully.";
            return this.RedirectToAction("All", "Event");
        }


        [AllowAnonymous]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 5;
            var viewModel = new CardListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.cardService.GetCount(),
                Cards = this.cardService.GetAll<CardInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var card = this.cardService.GetById<SingleCardViewModel>(id);
            card.Mana = this.cardService.GetAllCardManaByCardId<ManaListViewModel>(id);

            return this.View(card);
        }
    }
}
