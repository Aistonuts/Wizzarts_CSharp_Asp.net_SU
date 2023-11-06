namespace MagicCardsHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;


    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels.Art;
    using MagicCardsHub.Web.ViewModels.Card;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CardController : Controller
    {
         private readonly ApplicationDbContext db;
         private readonly ICardService cardService;
         private readonly UserManager<ApplicationUser> userManager;
         private readonly IWebHostEnvironment environment;

         public CardController(
             ICardService cardService, 
             UserManager<ApplicationUser> userManager,
             IWebHostEnvironment environment)
        {
            this.cardService = cardService;
            this.userManager = userManager;
            this.environment = environment;
        }

         public IActionResult Create(string id)
        {
            var digitalArt = this.cardService.GetArtById<CreateCardByArtIdInputModel>(id);
            return this.View(digitalArt);
        }

         [HttpPost]
         public async Task<IActionResult> Create(CreateCardByArtIdInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.cardService.CreateAsync(input, user.Id, id, $"{this.environment.WebRootPath}/Images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Card added successfully.";
            return this.RedirectToAction("Create");
        }
    }
}
