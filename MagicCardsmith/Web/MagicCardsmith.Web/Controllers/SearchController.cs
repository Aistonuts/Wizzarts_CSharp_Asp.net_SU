namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Data;
    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Data;
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.SearchArt;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IDeletableEntityRepository<Artist> artistRepository;

        public SearchController(
           IArtService artService,
           UserManager<ApplicationUser> userManager,
           IWebHostEnvironment environment,
           IDeletableEntityRepository<Artist> artistRepository)
        {
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
            this.artistRepository = artistRepository;
        }

        public IActionResult All(int id)
        {
            var viewModel = new ArtByArtistListViewModel
            {
                Art = this.artService.GetAllByArtistId<ArtInListViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
