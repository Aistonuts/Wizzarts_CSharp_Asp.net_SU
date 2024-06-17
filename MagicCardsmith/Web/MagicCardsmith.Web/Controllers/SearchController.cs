namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.SearchArt;
    using MagicCardsmith.Web.ViewModels.SearchCard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Artist> artistRepository;
        private readonly ICardService cardService;
        private readonly ICategoryService categoryService;

        public SearchController(
           IArtService artService,
           ICardService cardService,
           ICategoryService categoryService,
           UserManager<ApplicationUser> userManager,
           IWebHostEnvironment environment,
           IDeletableEntityRepository<Artist> artistRepository)
        {
            this.artService = artService;
            this.cardService = cardService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.environment = environment;
            this.artistRepository = artistRepository;
        }

        [AllowAnonymous]
        public IActionResult All(int id)
        {
            var viewModel = new ArtByArtistListViewModel
            {
                Art = this.artService.GetAllByArtistId<ArtInListViewModel>(id),
            };

            return this.View(viewModel);
        }

        //[HttpGet]
        //public IActionResult AdvancedSearch(SearchListInputModel input)
        //{
        //    var viewModel = new CardSearchResultViewModel
        //    {
        //        SearchedCards = this.cardService
        //        .GetByTypeCards<CardInListViewModel>(input.TypeOfCards),
        //    };

        //    return this.View(viewModel);
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search(SearchListInputModel input)
        {

            var viewModel = new CardSearchResultViewModel
            {
                SearchedCards = this.cardService
                .GetByName<CardInListViewModel>(input.CardName),
            };
            viewModel.SelectType = this.categoryService.GetAllCardType();
            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdvancedSearch(SearchListInputModel input)
        {
            var viewModel = new CardSearchResultViewModel
            {
            };

            return this.View(viewModel);
        }
    }
}
