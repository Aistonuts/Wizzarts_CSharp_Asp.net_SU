namespace MagicCardsHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;

    using MagicCardsHub.Services.Mapping;
    using MagicCardsHub.Web.ViewModels.Card;
    using Microsoft.AspNetCore.Http;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    using WebDriverManager.Helpers;

    public class CardService : ICardService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Card> cardsRepository;
        private readonly IDeletableEntityRepository<Art> artsRepository;

        public CardService(
            IDeletableEntityRepository<Card> cardsRepository,
            IDeletableEntityRepository<Art> artsRepository)
        {
            this.cardsRepository = cardsRepository;
            this.artsRepository = artsRepository;

        }

        public async Task CreateAsync(CreateCardByArtIdInputModel input, string userId, string id, string imagePath)
        {
            var card = new Card
            {
                Name = input.Name,
                FrameColorUrl = input.Frame,
                SymbolUrl = " ",
                CardType = input.CardType,
                AbilitiesAndFlavor = input.CardAbilitiesFlavor,
                Power = input.CardPower,
                Toughness = input.CardToughness,
                ArtId = id,
                CardExpansion = input.CardExpansion,
                CardRarity = input.CardRarity,
            };

            var redMana = new ManaSymbol
            {
                Color = "Red",
                Quantity = input.ManaRedCost,
            };

            var blueMana = new ManaSymbol
            {
                Color = "Blue",
                Quantity = input.ManaBlueCost,
            };

            var greenMana = new ManaSymbol
            {
                Color = "Green",
                Quantity = input.ManaGreenCost,
            };

            var blackMana = new ManaSymbol
            {
                Color = "Black",
                Quantity = input.ManaBlackCost,
            };

            var whiteMana = new ManaSymbol
            {
                Color = "Red",
                Quantity = input.ManaWhiteCost,
            };

            var colorlessMana = new ManaSymbol
            {
                Color = "Colorless",
                Quantity = input.ManaColorelessCost,
            };
            card.ManaSymbols.Add(redMana);
            card.ManaSymbols.Add(blueMana);
            card.ManaSymbols.Add(greenMana);
            card.ManaSymbols.Add(blueMana);
            card.ManaSymbols.Add(blackMana);
            card.ManaSymbols.Add(whiteMana);
            card.ManaSymbols.Add(colorlessMana);

            var symbol = new VarSymbols
            {
                CardExpansion = input.CardExpansion,
                CardRarity = input.CardRarity,
                RemoteImageUrl = "/symbols/various/" + input.CardExpansion + "_" + input.CardRarity + ".png",
            };

            card.VariousSymbols.Add(symbol);
            Directory.CreateDirectory($"{imagePath}/cardSmith/");
            var chromeDriverPath = new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            // Get a path of chromedriver
            string chromeDirectoryPath = System.IO.Path.GetDirectoryName(chromeDriverPath);

            var options = new ChromeOptions();
            options.AddArgument("--window-size=2560,1340");
            options.AddArgument("--headless=new");


            // Create instance by passing the path of the chromedriver directory
            IWebDriver driver = new ChromeDriver(chromeDirectoryPath, options);

            // Navigate to URL
            driver.Navigate().GoToUrl("https://localhost:5001/Card/Create/" + id);

            // Find the element where the screenshot should be taken
            var element = driver.FindElement(By.Id("second"));
            var elementScreenshot = (element as ITakesScreenshot).GetScreenshot();

            elementScreenshot.SaveAsFile($"{imagePath}/cardSmith/" + card.Id + ".png");

            card.ForgedCard = elementScreenshot.AsBase64EncodedString;

            card.CardRemoteURL = "/Images/cardSmith/" + card.Id + ".png";

            await this.cardsRepository.AddAsync(card);
            await this.cardsRepository.SaveChangesAsync();
        }

        public T GetArtById<T>(string id)
        {
            var art = this.artsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return art;
        }

        public T GetCardById<T>(string id)
        {
            var card = this.cardsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return card;
        }
    }
}
