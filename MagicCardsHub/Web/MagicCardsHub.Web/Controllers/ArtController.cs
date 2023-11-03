namespace MagicCardsHub.Web.Controllers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Data;
    using MagicCardsHub.Web.ViewModels.Art;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    using WebDriverManager.Helpers;

    public class ArtController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IArtService artService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ArtController(
            IArtService artService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.artService = artService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArtInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.artService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/Images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Art added successfully.";

            // TODO: Redirect to article info page
            return this.RedirectToAction("All");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ArtListViewModel
            {
                Art = this.artService.GetAll<ArtInListViewModel>(id, 3),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var digitalArt = this.artService.GetById<SingleArtViewModel>(id);
            return this.View(digitalArt);
        }

        public IActionResult Screenshot(string id)
        {
            // Download the same version of chromedriver
            var chromeDriverPath = new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            // Get a path of chromedriver
            string chromeDirectoryPath = System.IO.Path.GetDirectoryName(chromeDriverPath);

            var options = new ChromeOptions();
            options.AddArgument("--window-size=2560,1340");
            options.AddArgument("--headless=new");


            // Create instance by passing the path of the chromedriver directory
            IWebDriver driver = new ChromeDriver(chromeDirectoryPath, options);

            // Navigate to URL
            driver.Navigate().GoToUrl("https://localhost:5001/Art/ById/" + id);

            // Find the element where the screenshot should be taken
            var element = driver.FindElement(By.Id("second"));
            var elementScreenshot = (element as ITakesScreenshot).GetScreenshot();

            elementScreenshot.SaveAsFile(AppDomain.CurrentDomain.BaseDirectory + "//Screenshot1.png");
            //Take the screenshot
            // Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshotA();
            //
            // var img = (Bitmap)Image.FromStream(new MemoryStream(screenshot.AsByteArray));
            //
            // // Crop the element to be captured
            // var image = img.Clone(new Rectangle(element.Location, element.Size), img.PixelFormat);
            //
            // //Save the screenshot
            // image.Save(AppDomain.CurrentDomain.BaseDirectory + "//Screenshot.png");
            return this.RedirectToAction("ById", new { id });
        }
    }
}
