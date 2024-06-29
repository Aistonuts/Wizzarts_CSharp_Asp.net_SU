using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using Wizzarts.Data.Models;
using Wizzarts.Services.Data;
using Wizzarts.Web.ViewModels.Home;

namespace Wizzarts.Web.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<IndexAuthenticationViewModel> _logger;

        public RegistrationController(
          SignInManager<ApplicationUser> _signInManager,
          UserManager<ApplicationUser> _userManager,
          RoleManager<ApplicationRole> _roleManager,
          IUserStore<ApplicationUser> _userStore,
          ILogger<IndexAuthenticationViewModel> _logger,
          IArticleService articlesServic,
          IArtService artService,
          IPlayCardService playCardService,
          IEventService eventService,
          IStoreService storeService,
          IPlayCardExpansionService cardExpansionService,
          IMemoryCache cache)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._userStore = _userStore;
            this._logger = _logger;
        }

        public IActionResult Registration()
        {

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(IndexAuthenticationViewModel viewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            this.ModelState.Remove("UserName");
            this.ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, viewModel.RegisterEmail, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, viewModel.RegisterPassword);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = viewModel.RegisterEmail, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }


        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
