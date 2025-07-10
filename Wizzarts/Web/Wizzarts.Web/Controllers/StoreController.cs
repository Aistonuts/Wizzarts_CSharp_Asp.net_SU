﻿namespace Wizzarts.Web.Controllers
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
    using Wizzarts.Web.ViewModels.Article;
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
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateStoreViewModel();
            viewModel.Stores = await this.storeService.GetAll<StoreInListViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStoreViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (!this.ModelState.IsValid)
            {
                input.Stores = await this.storeService.GetAll<StoreInListViewModel>();
                return this.View(input);
            }

            if (await this.storeService.StoreNameExist(input.StoreName))
            {
                this.ModelState.AddModelError(nameof(input.StoreName), "Store name exist.");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.storeService.CreateAsync(input, this.User.GetId(), $"{this.environment.WebRootPath}/images");
                if (this.User.IsPremiumUser() == false)
                {
                    await this.userManager.AddToRoleAsync(user, PremiumRoleName);
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Stores = await this.storeService.GetAll<StoreInListViewModel>();
                return this.View(input);
            }

            this.TempData["Message"] = "Store added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        [AllowAnonymous]
        public async Task<IActionResult> All(int id = 1)
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
                Count = await this.storeService.GetCount(),
                Stores = await this.storeService.GetAll<StoreInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> ApproveStore(int id)
        {
            var userId = await this.storeService.ApproveStore(id);

            return this.RedirectToAction("ById", "Member", new { id = $"{userId}", Area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await this.storeService.ExistsAsync(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.storeService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.storeService.DeleteAsync(id);

            return this.RedirectToAction("MyData", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await this.storeService.GetById<StoreInListViewModel>(id);
            var viewModel = new EditStoreViewModel();
            if (await this.storeService.ExistsAsync(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.storeService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            viewModel.StoreName = store.Name;
            viewModel.StoreCountry = store.Country;
            viewModel.StoreCity = store.City;
            viewModel.StorePhoneNumber = store.PhoneNumber;
            viewModel.StoreAddress = store.Address;
            viewModel.Image = store.Image;
            viewModel.StoreOwner = store.StoreOwner;
            viewModel.Id = store.Id;
            viewModel.Stores = await this.storeService.GetAll<StoreInListViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStoreViewModel input)
        {
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");
            this.ModelState.Remove("Image");
            if (!this.ModelState.IsValid)
            {
                input.Stores = await this.storeService.GetAll<StoreInListViewModel>();
                return this.View(input);
            }

            var store = await this.storeService.GetById<StoreInListViewModel>(id);
            if (await this.storeService.StoreNameExist(input.StoreName) && store.Id != id)
            {
                this.ModelState.AddModelError(nameof(input.StoreName), "Article title exist.");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (await this.storeService.ExistsAsync(id) == false)
            {
                return this.BadRequest();
            }

            if (await this.storeService.HasUserWithIdAsync(id, this.User.GetId()) == false
                && this.User.IsAdmin() == false)
            {
                return this.Unauthorized();
            }

            await this.storeService.UpdateAsync(id, input);

            this.TempData["Message"] = "Store updated successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction(nameof(this.Edit), new { id });
        }
    }
}
