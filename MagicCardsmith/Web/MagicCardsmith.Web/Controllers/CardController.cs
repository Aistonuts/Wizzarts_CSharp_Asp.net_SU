namespace MagicCardsmith.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using MagicCardsmith.Common;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.Infrastructure.Extensions;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.CardReview;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Mana;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static MagicCardsmith.Common.GlobalConstants;

    public class CardController : BaseController
    {
        private readonly ICardService cardService;
        private readonly IArtistService artistService;
        private readonly ICategoryService categoryService;
        private readonly IEventService eventService;
        private readonly IReviewService reviewService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Card> cardRepository;

        public CardController(
            ICardService cardService,
            IArtistService artistService,
            ICategoryService categoryService,
            IEventService eventService,
            IReviewService reviewService,
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<Card> cardRepository)
        {
            this.cardService = cardService;
            this.artistService = artistService;
            this.categoryService = categoryService;
            this.eventService = eventService;
            this.reviewService = reviewService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;
        }



        public async Task<IActionResult> PremiumCreate()
        {
            if (this.User.IsInRole(PremiumAccountRoleName) || !this.User.IsInRole(AdministratorRoleName))
            {
                return this.RedirectToAction("Membership", "Premium");
            }

            var viewModel = new PremiumCreateCardInputModel();
            viewModel.RedMana = this.categoryService.GetAllRedMana();
            viewModel.BlueMana = this.categoryService.GetAllBlueMana();
            viewModel.BlackMana = this.categoryService.GetAllBlackMana();
            viewModel.GreenMana = this.categoryService.GetAllGreenMana();
            viewModel.WhiteMana = this.categoryService.GetAllWhiteMana();
            viewModel.ColorlessMana = this.categoryService.GetAllColorlessMana();
            viewModel.SelectType = this.categoryService.GetAllCardType();
            viewModel.SelectFrameColor = this.categoryService.GetAllCardFrames();
            viewModel.SelectExpansion = this.categoryService.GetAllExpansionInListView();

            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ArtByUserId = this.artService.GetAllArtByUserId<ArtInListViewModel>(user.Id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PremiumCreate(PremiumCreateCardInputModel input,  string canvasCapture)
        {
            if (this.User.IsInRole(PremiumAccountRoleName) || !this.User.IsInRole(AdministratorRoleName))
            {
                return this.RedirectToAction("Membership", "Premium");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (!this.artService.IsBase64String(canvasCapture))
            {
                throw new Exception($"Invalid Base64String {canvasCapture}");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            bool isEventCard = false;

            try
            {
                await this.cardService.PremiumCreateAsync(input, user.Id, $"{this.environment.WebRootPath}/Images", isEventCard, false, canvasCapture);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Card added successfully.";
            return this.RedirectToAction("All", "Event");

        }

        public async Task<IActionResult> Create(int id)
        {
            var viewModel = new CreateCardInputModel();
            viewModel.RedMana = this.categoryService.GetAllRedMana();
            viewModel.BlueMana = this.categoryService.GetAllBlueMana();
            viewModel.BlackMana = this.categoryService.GetAllBlackMana();
            viewModel.GreenMana = this.categoryService.GetAllGreenMana();
            viewModel.WhiteMana = this.categoryService.GetAllWhiteMana();
            viewModel.ColorlessMana = this.categoryService.GetAllColorlessMana();
            viewModel.SelectType = this.categoryService.GetAllCardType();
            viewModel.SelectFrameColor = this.categoryService.GetAllCardFrames();
            viewModel.SelectExpansion = this.categoryService.GetAllExpansionInListView();

            var milestone = this.eventService.GetMilestoneById<MilestonesInListViewModel>(id);
            var currentEvent = this.eventService.GetById<EventInListViewModel>(milestone.EventId);
            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ArtByUserId = this.artService.GetAllArtByUserId<ArtInListViewModel>(user.Id);

            viewModel.EventMilestoneImage = milestone.ImageUrl;
            viewModel.EventMilestoneTitle = milestone.Title;
            viewModel.EventMilestoneDescription = milestone.Description;
            viewModel.EventDescription = currentEvent.EventDescription;
            viewModel.EventId = milestone.EventId;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCardInputModel input, int id, string canvasCapture)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var milestone = this.eventService.GetMilestoneById<MilestonesInListViewModel>(id);

            bool isEventCard = false;

            if (milestone != null)
            {
                isEventCard = true;
            }

            if (milestone != null && milestone.RequireArtInput)
            {
                var cardTitle = milestone.Title;
                var cardDescription = milestone.Description;
                input.Name = cardTitle;
                input.AbilitiesAndFlavor = cardDescription;
            }

            if (!this.ModelState.IsValid)
            {
                input.RedMana = this.categoryService.GetAllRedMana();
                input.BlueMana = this.categoryService.GetAllBlueMana();
                input.BlackMana = this.categoryService.GetAllBlackMana();
                input.GreenMana = this.categoryService.GetAllGreenMana();
                input.WhiteMana = this.categoryService.GetAllWhiteMana();
                input.ColorlessMana = this.categoryService.GetAllColorlessMana();
                input.SelectType = this.categoryService.GetAllCardType();
                input.SelectFrameColor = this.categoryService.GetAllCardFrames();
                input.SelectExpansion = this.categoryService.GetAllExpansionInListView();

                var currentEvent = this.eventService.GetById<EventInListViewModel>(milestone.EventId);

                input.ArtByUserId = this.artService.GetAllArtByUserId<ArtInListViewModel>(user.Id);

                input.EventMilestoneImage = milestone.ImageUrl;
                input.EventMilestoneTitle = milestone.Title;
                input.EventMilestoneDescription = milestone.Description;
                input.EventDescription = currentEvent.EventDescription;
                input.EventId = milestone.EventId;

                return this.View(input);
            }

            if (!this.artService.IsBase64String(canvasCapture))
            {
                throw new Exception($"Invalid Base64String {canvasCapture}");
            }

            try
            {
                await this.cardService.CreateAsync(input, user.Id, milestone.EventId, $"{this.environment.WebRootPath}/Images", isEventCard, milestone.RequireArtInput, canvasCapture);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Card added successfully.";
            return this.RedirectToAction("All","Event");
        }

        [AllowAnonymous]
        public IActionResult All( int id = 1)
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

        public IActionResult ById(int id, string information)
        {
            var card = this.cardService.GetById<SingleCardViewModel>(id);
            card.Mana = this.cardService.GetAllByCardId<ManaListViewModel>(id);
            card.CardReviews = this.reviewService.GetAllReviews<CardReviewInListViewModel>();

            if (information != card.GetCardName())
            {
                return this.BadRequest(information);
            }

            return this.View(card);
        }

        [HttpPost]
        public async Task<IActionResult> CommentAsync(SingleCardViewModel model, int id)
        {
            if (this.User.IsInRole(PremiumAccountRoleName) || !this.User.IsInRole(AdministratorRoleName) || this.User.IsInRole(ArtistRoleName) || this.User.IsInRole(StoreOwnerRoleName))
            {
                return this.RedirectToAction("Membership", "Premium");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.reviewService.CreateAsync(model, user.Id, id);
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(model);
            }

            this.TempData["Message"] = "Review added successfully.";

            return this.RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveCard(int id)
        {
            await this.cardService.ApproveCard(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}