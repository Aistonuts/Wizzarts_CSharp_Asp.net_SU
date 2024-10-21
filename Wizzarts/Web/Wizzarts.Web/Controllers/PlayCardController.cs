using Wizzarts.Data.Models.Enums;
using Wizzarts.Data.Models;

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
    using Wizzarts.Data;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.Infrastructure.Extensions;
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.CardComments;
    using Wizzarts.Web.ViewModels.CardGameExpansion;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    using static Wizzarts.Common.GlobalConstants;

    public class PlayCardController : BaseController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPlayCardService cardService;
        private readonly ICommentService commentService;
        private readonly IPlayCardComponentsService playCardComponentsService;
        private readonly IPlayCardExpansionService playCardExpansionService;
        private readonly IEventService eventService;
        private readonly IArticleService articleService;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<PlayCard> cardRepository;


        public PlayCardController(
            ApplicationDbContext dbContext,
            IPlayCardService cardService,
            ICommentService commentService,
            IPlayCardComponentsService playCardComponentsService,
            IPlayCardExpansionService playCardExpansionService,
            IEventService eventService,
            IArticleService articleService,
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IDeletableEntityRepository<PlayCard> cardRepository)
        {
            this.dbContext = dbContext;
            this.cardService = cardService;
            this.commentService = commentService;
            this.playCardComponentsService = playCardComponentsService;
            this.playCardExpansionService = playCardExpansionService;
            this.eventService = eventService;
            this.articleService = articleService;
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.cardRepository = cardRepository;

        }

        [HttpGet]
        public IActionResult Add(int id)
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

            //var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ArtByUserId = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCardViewModel input, int id, string canvasCapture)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            bool isEventCard = false;

            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            var art = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());

            if(art.Any(x => x.Id == input.ArtId))
            {
                this.ModelState.AddModelError(nameof(input.ArtId), "Play card with this Art already exist");
            }

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

                input.ArtByUserId = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(user.Id);

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
            var eventComponent = this.eventService.GetEventComponentById<EventComponentsInListViewModel>(id);
            var currentEvent = this.eventService.GetById<EventInListViewModel>(eventComponent.EventId);

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

         
            //var user = await this.userManager.GetUserAsync(this.User);

            viewModel.ArtByUserId = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(this.User.GetId());

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
                await this.cardService.CreateAsync(input, this.User.GetId(), eventComponent.EventId, $"{this.environment.WebRootPath}/images", isEventCard, eventComponent.RequireArtInput, canvasCapture);
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
                Cards = this.cardService.GetRandom<CardInListViewModel>(20),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                Articles = this.articleService.GetRandom<ArticleInListViewModel>(4),
            };

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult AllEventCards(int id = 1)
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
                Cards = this.cardService.GetAllEventCards<CardInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var card = this.cardService.GetById<SingleCardViewModel>(id);

            if (card != null)
            {
                card.Mana = this.cardService.GetAllCardManaByCardId<ManaListViewModel>(id);
            }
            var commentsByAdmin = this.commentService.GetCommentsByCardId<CardCommentInListViewModel>(id);
            card.CommentsByAdmin = commentsByAdmin;
            card.Events = this.eventService.GetAll<EventInListViewModel>();
            card.Articles = this.articleService.GetRandom<ArticleInListViewModel>(4);
            return this.View(card);
        }

        //[HttpPost]
        //public async Task<IActionResult> AttackComment(SingleCardViewModel model, string cardId)
        //{
        //    bool IsAdmin = false;
        //    if (this.User.IsInRole(AdministratorRoleName))
        //    {
        //        IsAdmin = true;
        //    }
        //    this.ModelState.Remove("UserName");
        //    this.ModelState.Remove("Password");
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    var user = await this.userManager.GetUserAsync(this.User);

        //    try
        //    {
        //        await this.commentService.AttackCommentAsync(model, user.Id, cardId, IsAdmin);
        //    }
        //    catch (Exception ex)
        //    {

        //        this.ModelState.AddModelError(string.Empty, ex.Message);

        //        return this.View(model);
        //    }

        //    this.TempData["Message"] = "Comment added successfully.";

        //    return this.RedirectToAction("ById", "PlayCard", new { id = $"{model.Id}", Area = "" });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Defense(SingleCardViewModel model, int data)
        //{
        //    bool IsAdmin = false;
        //    var currentComment = this.commentService.GetAttackCommentById<CardCommentInListViewModel>(data);

        //    if (this.User.IsInRole(AdministratorRoleName))
        //    {
        //        IsAdmin = true;
        //    }
        //    this.ModelState.Remove("UserName");
        //    this.ModelState.Remove("Password");
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    var user = await this.userManager.GetUserAsync(this.User);

        //    try
        //    {
        //        await this.commentService.DefenseReplyCommentAsync(model, user.Id,  currentComment.CardId, data, IsAdmin);
        //    }
        //    catch (Exception ex)
        //    {

        //        this.ModelState.AddModelError(string.Empty, ex.Message);

        //        return this.View(model);
        //    }

        //    this.TempData["Message"] = "Reply added successfully.";

        //    return this.RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveCard(string id)
        {
            var userId = await this.cardService.ApproveCard(id);
            if (userId != null)
            {
                return this.RedirectToAction("ById", "User", new { id = $"{userId}", Area = "Administration" });
            }
            else
            {
                return this.BadRequest();
            }
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

            return this.RedirectToAction("User", "MyData");
        }
    }
}
