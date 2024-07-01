namespace Wizzarts.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Wizzarts.Services.Data;
    using Wizzarts.Web.ViewModels.Art;

    public class ArtController : BaseController
    {
        private readonly IArtService artService;

        public ArtController(
            IArtService artService)
        {
            this.artService = artService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }
            const int ItemsPerPage = 10;
            var viewModel = new ArtListViewModel
            {
                Art = this.artService.GetRandom<ArtInListViewModel>(20),
            };

            return this.View(viewModel);
        }
    }
}
