namespace MagicCardsmith.Web.Controllers
{
    using MagicCardsmith.Web.ViewModels.ElevatedRights;
    using Microsoft.AspNetCore.Mvc;

    public class ClaimController : Controller
    {
        public ClaimController()
        {
            
        }

        public IActionResult Become()
        {
            var viewModel = new ClaimViewModel();

            return View();
        }
    }
}
