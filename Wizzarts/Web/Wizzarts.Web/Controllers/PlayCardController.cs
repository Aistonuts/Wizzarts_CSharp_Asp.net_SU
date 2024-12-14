namespace Wizzarts.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Common;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Attributes;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.CardComments;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    using static Wizzarts.Common.HardCodedConstants;

    public class PlayCardController : BaseController
    {
        private readonly IPlayCardService cardService;
        private readonly ICommentService commentService;
        private readonly IPlayCardComponentsService playCardComponentsService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public PlayCardController(
            IPlayCardService cardService,
            ICommentService commentService,
            IPlayCardComponentsService playCardComponentsService,
            IEventService eventService,
            IArticleService articleService,
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.cardService = cardService;
            this.commentService = commentService;
            this.playCardComponentsService = playCardComponentsService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        [MustBeAnArtist]
        public async Task<IActionResult> Add(int id = 0)
        {
            if (this.User == null || (this.User.IsAdmin() == false && this.User.IsArtist() == false))
            {
                return this.Unauthorized();
            }

            var userArts = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());
            if (!userArts.Any())
            {
                return this.RedirectToAction("Add", "Art");
            }

            var viewModel = new CreateCardViewModel();
            viewModel.RedMana = await this.playCardComponentsService.GetAllRedMana();
            viewModel.BlueMana = await this.playCardComponentsService.GetAllBlueMana();
            viewModel.BlackMana = await this.playCardComponentsService.GetAllBlackMana();
            viewModel.GreenMana = await this.playCardComponentsService.GetAllGreenMana();
            viewModel.WhiteMana = await this.playCardComponentsService.GetAllWhiteMana();
            viewModel.ColorlessMana = await this.playCardComponentsService.GetAllColorlessMana();
            viewModel.SelectType = await this.playCardComponentsService.GetAllCardType();
            viewModel.SelectFrameColor = await this.playCardComponentsService.GetAllCardFrames();
            viewModel.SelectExpansion = await this.playCardComponentsService.GetAllExpansionInListView();
            viewModel.ArtByUserId = userArts;

            if (id != 0)
            {
                viewModel.EventId = id;
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [MustBeAnArtist]
        public async Task<IActionResult> Add(CreateCardViewModel input, string canvasCapture)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            bool isEventCard = false;

            if (await this.cardService.CardTitleExist(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), "Card name exist.");
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            var art = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());

            if (await this.playCardComponentsService.BlackManaExistsAsync(input.BlackManaId) == false ||
                await this.playCardComponentsService.BlueManaExistsAsync(input.BlueManaId) == false ||
                await this.playCardComponentsService.CardFrameExistsAsync(input.CardFrameId) == false ||
                await this.playCardComponentsService.CardTypeExistsAsync(input.CardTypeId) == false ||
                await this.playCardComponentsService.ColorlessManaExistsAsync(input.ColorlessManaId) == false ||
                await this.playCardComponentsService.GreenManaExistsAsync(input.GreenManaId) == false ||
                await this.playCardComponentsService.RedManaExistsAsync(input.RedManaId) == false ||
                await this.playCardComponentsService.WhiteManaExistsAsync(input.WhiteManaId) == false)
            {
                this.ModelState.AddModelError(nameof(input.CardTypeId), "Category does not exist");
            }

            if (art.Any(x => x.Id == input.ArtId) == false)
            {
                this.ModelState.AddModelError(nameof(input.ArtId), "Art with this id does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                var userArts = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());
                input.RedMana = await this.playCardComponentsService.GetAllRedMana();
                input.BlueMana = await this.playCardComponentsService.GetAllBlueMana();
                input.BlackMana = await this.playCardComponentsService.GetAllBlackMana();
                input.GreenMana = await this.playCardComponentsService.GetAllGreenMana();
                input.WhiteMana = await this.playCardComponentsService.GetAllWhiteMana();
                input.ColorlessMana = await this.playCardComponentsService.GetAllColorlessMana();
                input.SelectType = await this.playCardComponentsService.GetAllCardType();
                input.SelectFrameColor = await this.playCardComponentsService.GetAllCardFrames();
                input.SelectExpansion = await this.playCardComponentsService.GetAllExpansionInListView();

                input.ArtByUserId = userArts;

                return this.View(input);
            }

            if (!this.artService.IsBase64String(canvasCapture))
            {
                throw new Exception($"Invalid Base64String {canvasCapture}");
            }

            try
            {
                await this.cardService.AddAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images", isEventCard, canvasCapture);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Card added successfully.";
            return this.RedirectToAction("Add", "PlayCard");
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var eventComponent = await this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);
            var currentEvent = await this.eventService.GetById<EventInListViewModel>(eventComponent.EventId);

            var viewModel = new CreateCardViewModel();
            viewModel.RedMana = await this.playCardComponentsService.GetAllRedMana();
            viewModel.BlueMana = await this.playCardComponentsService.GetAllBlueMana();
            viewModel.BlackMana = await this.playCardComponentsService.GetAllBlackMana();
            viewModel.GreenMana = await this.playCardComponentsService.GetAllGreenMana();
            viewModel.WhiteMana = await this.playCardComponentsService.GetAllWhiteMana();
            viewModel.ColorlessMana = await this.playCardComponentsService.GetAllColorlessMana();
            viewModel.SelectType = await this.playCardComponentsService.GetAllCardType();
            viewModel.SelectFrameColor = await this.playCardComponentsService.GetAllCardFrames();
            viewModel.SelectExpansion = await this.playCardComponentsService.GetAllExpansionInListView();

            viewModel.ArtByUserId = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());

            viewModel.EventMilestoneImage = eventComponent.ImageUrl;
            viewModel.EventMilestoneTitle = eventComponent.Title;
            viewModel.EventMilestoneDescription = eventComponent.Description;
            viewModel.EventDescription = currentEvent.EventDescription;
            viewModel.EventId = eventComponent.EventId;
            viewModel.EventCategoryId = currentEvent.EventCategoryId;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCardViewModel input, int id, string canvasCapture)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var eventComponent = await this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);
            var isArtByUser = await this.artService.HasUserWithIdAsync(input.ArtId, this.User.GetId());

            if (eventComponent.EventCategoryId == ImagelessType && isArtByUser == false)
            {
                this.ModelState.AddModelError(nameof(input.ArtId), "Art does not exist");
            }

            if (await this.cardService.CardTitleExist(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), "Card name exist.");
            }

            if (await this.playCardComponentsService.BlackManaExistsAsync(input.BlackManaId) == false ||
                await this.playCardComponentsService.BlueManaExistsAsync(input.BlueManaId) == false ||
                await this.playCardComponentsService.CardFrameExistsAsync(input.CardFrameId) == false ||
                await this.playCardComponentsService.CardTypeExistsAsync(input.CardTypeId) == false ||
                await this.playCardComponentsService.ColorlessManaExistsAsync(input.ColorlessManaId) == false ||
                await this.playCardComponentsService.GreenManaExistsAsync(input.GreenManaId) == false ||
                await this.playCardComponentsService.RedManaExistsAsync(input.RedManaId) == false ||
                await this.playCardComponentsService.WhiteManaExistsAsync(input.WhiteManaId) == false)
            {
                this.ModelState.AddModelError(nameof(input.CardTypeId), "Category does not exist");
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                input.RedMana = await this.playCardComponentsService.GetAllRedMana();
                input.BlueMana = await this.playCardComponentsService.GetAllBlueMana();
                input.BlackMana = await this.playCardComponentsService.GetAllBlackMana();
                input.GreenMana = await this.playCardComponentsService.GetAllGreenMana();
                input.WhiteMana = await this.playCardComponentsService.GetAllWhiteMana();
                input.ColorlessMana = await this.playCardComponentsService.GetAllColorlessMana();
                input.SelectType = await this.playCardComponentsService.GetAllCardType();
                input.SelectFrameColor = await this.playCardComponentsService.GetAllCardFrames();
                input.SelectExpansion = await this.playCardComponentsService.GetAllExpansionInListView();

                var currentEvent = await this.eventService.GetById<EventInListViewModel>(eventComponent.EventId);

                input.ArtByUserId = await this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(user.Id);

                input.EventMilestoneImage = eventComponent.ImageUrl;
                input.EventMilestoneTitle = eventComponent.Title;
                input.EventMilestoneDescription = eventComponent.Description;
                input.EventDescription = currentEvent.EventDescription;
                input.EventId = eventComponent.EventId;
                input.EventCategoryId = currentEvent.EventCategoryId;

                return this.View(input);
            }

            if (eventComponent != null && eventComponent.EventCategoryId == ImagelessType)
            {
                var cardTitle = eventComponent.Title;
                var cardDescription = eventComponent.Description;
                input.Name = cardTitle;
                input.AbilitiesAndFlavor = cardDescription;
            }

            if (!this.artService.IsBase64String(canvasCapture))
            {
                throw new Exception($"Invalid Base64String {canvasCapture}");
            }

            try
            {
                await this.cardService.CreateAsync(input, this.User.GetId(), eventComponent.EventId, $"{this.environment.WebRootPath}/images", eventComponent.EventCategoryId, canvasCapture);
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
        public async Task<IActionResult> All(int id = 1)
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
                Count = await this.cardService.GetCount(),
                Cards = this.cardService.GetRandom<CardInListViewModel>(20),
                Events = await this.eventService.GetAll<EventInListViewModel>(),
                Articles = this.articleService.GetRandom<ArticleInListViewModel>(4),
            };

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllEventCards(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 5;
            var cards = await this.cardService.GetAllEventCards<CardInListViewModel>();
            var viewModel = new CardListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Cards = cards,
                Count = cards.Count(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(string id, string information)
        {
            var card = await this.cardService.GetById<SingleCardViewModel>(id);
            if (card == null || information != card.GetCardName())
            {
                return this.BadRequest();
            }

            if (card != null)
            {
                card.Mana = await this.cardService.GetAllCardManaByCardId<ManaListViewModel>(id);
            }

            card.Comments = await this.commentService.GetCommentsByCardId<CardCommentInListViewModel>(id);
            card.Events = await this.eventService.GetAll<EventInListViewModel>();
            card.Articles = this.articleService.GetRandom<ArticleInListViewModel>(4);
            return this.View(card);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(SingleCardViewModel model, string id)
        {
            if (this.User == null || (this.User.IsAdmin() == false && this.User.IsArtist() == false && this.User.IsPremiumUser() == false))
            {
                return this.Unauthorized();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user.Nickname.Length == 0 || user.Nickname == null)
            {
                return this.RedirectToAction("SelectAvatar", "User");
            }

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.commentService.CommentAsync(model, user.Id, id, this.User.IsAdmin());
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.View(model);
            }

            this.TempData["Message"] = "Comment added successfully.";

            return this.RedirectToAction("ById", "PlayCard", new { id = $"{id}", information = model.GetCardName(), Area = string.Empty });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveCard(string id)
        {
            var userId = await this.cardService.ApproveCard(id);
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Promote(string id)
        {
            if (await this.cardService.CardExist(id) == false)
            {
                return this.BadRequest();
            }

            var card = await this.cardService.GetById<SingleCardViewModel>(id);
            await this.cardService.Promote(id);

            return this.RedirectToAction("ById", "PlayCard", new { id = $"{id}", information = card.GetCardName(), Area = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.cardService.CardExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.cardService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.cardService.DeleteAsync(id);

            return this.RedirectToAction("MyData", "User");
        }
    }
}
