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
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.Store;
    using static Wizzarts.Common.GlobalConstants;

    public class StoreController : BaseController
    {
        private readonly IStoreService storeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public StoreController(
            IStoreService storeService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.storeService = storeService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateStoreViewModel();
            viewModel.Stores = this.storeService.GetAll<StoreInListViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateStoreViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var currentRole = await this.userManager.GetRolesAsync(user);
            try
            {
                await this.storeService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images");
                if (!currentRole.Contains(StoreOwnerRoleName))
                {
                    await this.userManager.AddToRoleAsync(user, StoreOwnerRoleName);
                }
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Store added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        [AllowAnonymous]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }
            const int ItemsPerPage = 4;
            var viewModel = new StoreListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.storeService.GetCount(),
                Stores = this.storeService.GetAll<StoreInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveStore(int id)
        {
          var userId = await this.storeService.ApproveStore(id);

          return this.RedirectToAction("ById", "User", new { id = $"{userId}", Area = "Administration" });
        }
    }
}
