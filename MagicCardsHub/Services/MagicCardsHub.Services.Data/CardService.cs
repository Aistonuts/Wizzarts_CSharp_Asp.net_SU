namespace MagicCardsHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    using MagicCardsHub.Web.ViewModels.Card;

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
                Title = input.CardExpansion + " " + input.CardRarity,
            };

            card.VariousSymbols.Add(symbol);

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
