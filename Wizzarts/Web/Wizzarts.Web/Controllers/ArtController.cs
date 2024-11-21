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
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.PlayCard;

    public class ArtController : BaseController
    {
        private readonly IArtService artService;
        private readonly IUserService userService;
        private readonly IArticleService articlesService;
        private readonly IPlayCardService playCardService;
        private readonly IEventService eventService;
        private readonly IWizzartsServices wizzartsServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArtController(
            IArtService artService,
            IUserService userService,
            IArticleService articlesService,
            IPlayCardService playCardService,
            IEventService eventService,
            IWizzartsServices wizzartsServices,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.artService = artService;
            this.userService = userService;
            this.articlesService = articlesService;
            this.playCardService = playCardService;
            this.eventService = eventService;
            this.wizzartsServices = wizzartsServices;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Add()
        {
            var viewModel = new AddArtViewModel
            {
                Events = this.eventService.GetAll<EventInListViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArtViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            bool isPremium = await this.userService.IsPremium(this.User.GetId());
            try
            {
                await this.artService.AddAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images", isPremium);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Art added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("MyData", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (await this.artService.ArtExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.artService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            var inputModel = this.artService.GetById<EditArtViewModel>(id);

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
        public async Task<IActionResult> Edit(EditArtViewModel inputModel, string id)
        {
            if (await this.artService.ArtExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.artService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {

                return this.View(inputModel);
            }

            if (await this.artService.ArtExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.artService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.artService.UpdateAsync(inputModel, id);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [AllowAnonymous]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ArtListViewModel
            {
                Arts = this.artService.GetRandom<ArtInListViewModel>(20),
                Articles = this.articlesService.GetRandom<ArticleInListViewModel>(3),
                Events = this.eventService.GetAll<EventInListViewModel>(),
                Cards = this.playCardService.GetRandom<CardInListViewModel>(3),
            };

            return this.View(viewModel);
        }

        public IActionResult ByUserId(int id)
        {
            var userId = this.wizzartsServices.GetUserIdByArtistId(id);
            var viewModel = new ArtListViewModel
            {
                Arts = this.artService.GetAllArtByUserIdPaginationless<ArtInListViewModel>(userId),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(string id, string information)
        {
            if (id == null)
            {
                return this.Unauthorized();
            }

            var art = await this.artService.GetById<SingleArtViewModel>(id);
            if (information != art.GetInformation())
            {
                return this.BadRequest(information);
            }

            art.Events = this.eventService.GetAll<EventInListViewModel>();
            art.Articles = this.articlesService.GetRandom<ArticleInListViewModel>(3);
            return this.View(art);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveArt(string id)
        {
           var userId = await this.artService.ApproveArt(id);
           if (userId != null)
           {
               return this.RedirectToAction("ById", "Member", new { id = $"{userId}", Area = "Administration" });
           }else
           {
               return this.BadRequest();
           }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.artService.ArtExist(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.artService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.artService.DeleteAsync(id);

            return this.RedirectToAction("MyData", "User");
        }
    }
}
