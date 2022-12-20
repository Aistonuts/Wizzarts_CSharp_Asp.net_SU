using Microsoft.AspNetCore.Mvc;
using MyFirstAspNetCoreApp.Services;
using MyFirstAspNetCoreApp.ViewModels.NavBar;

namespace MyFirstAspNetCoreApp.ViewComponents
{
    [ViewComponent(Name = "NavBar")]
    public class NavBarViewComponents:ViewComponent
    {
        private readonly IYearsService yearsService;
        public NavBarViewComponents(IYearsService yearsService)
        {
            this.yearsService = yearsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new NavBarViewModel();
            viewModel.Years = this.yearsService.GetLastYears(5);
            return this.View(viewModel);
        }
    }


}
