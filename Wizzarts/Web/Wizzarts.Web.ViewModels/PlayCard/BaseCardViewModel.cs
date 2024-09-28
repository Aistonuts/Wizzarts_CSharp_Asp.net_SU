namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;

    public class BaseCardViewModel : IndexAuthenticationViewModel
    {
        // Default values
        public const int DefaultManaValue = 1;

        public const int DefaultCardTypeValue = 1;

        public const int DefaultCardFrameValue = 3;

        public const int DefaultGameExpansionId = 2;

        public const string DefaultCardName = "Unknown Card.";

        public const string DefaultCardFrameUrl = "/images/frames/createcard.jpg";

        public const string DefaultGameExpansionUrl = "/images/symbols/expansions/Nameless.png";

        public const string DefaulCardType = "Not Defined !!!";

        public const string DefaultCardDescription = "Unknown Abilites and Flavour.";

        public const string DefaultCardImage = "/images/navigation/0.jpg";

        // Model view properties
        [Required(ErrorMessage = "Card Name is required!")]
        [StringLength(CardNameMaxLength, MinimumLength = CardNameMinLength, ErrorMessage = "Card name should be between 5 and 30 characters long")]
        public string Name { get; set; } = DefaultCardName;

        public int CardFrameColorId { get; set; }

        public string CardRemoteUrl { get; set; }

        public string CardFrameDefaultUrl { get; set; } = DefaultCardFrameUrl;

        public string CardExpansionSymbolDefaultUrl { get; set; } = DefaultGameExpansionUrl;

        public string CardDefaultType { get; set; } = DefaulCardType;

        public string CardDefaultName { get; set; } = DefaultCardName;

        public string CardDefaultDescription { get; set; } = DefaultCardDescription;

        public string CardDefaultImage { get; set; } = DefaultCardImage;

        public int BlackManaId { get; set; } = DefaultManaValue;

        public int BlueManaId { get; set; } = DefaultManaValue;

        public int RedManaId { get; set; } = DefaultManaValue;

        public int WhiteManaId { get; set; } = DefaultManaValue;

        public int GreenManaId { get; set; } = DefaultManaValue;

        public int ColorlessManaId { get; set; } = DefaultManaValue;

        public int CardTypeId { get; set; } = DefaultCardTypeValue;

        public int CardFrameId { get; set; } = DefaultCardFrameValue;

        public int GameExpansionId { get; set; } = DefaultGameExpansionId;

        [Required(ErrorMessage = "Card Abilities are required!")]
        [StringLength(CardAbilitiesAndFlavorMaxLenght, MinimumLength = CardAbilitiesAndFlavorMinLenght, ErrorMessage = "Card Abilities description should be between 28 and 200 characters long")]
        [Display(Name = "Abilities And Flavor")]
        public string AbilitiesAndFlavor { get; set; } = DefaultCardDescription;

        [StringLength(CardPowerMaxLenght, MinimumLength = CardPowerMinLenght, ErrorMessage = "Card power should be 1 character long")]
        public string? Power { get; set; }

        [StringLength(CardToughnessMaxLenght, MinimumLength = CardToughnessMinLenght, ErrorMessage = "Card toughness should be 1 character long")]
        public string? Toughness { get; set; }

        public string ArtId { get; set; }

        public string CardSmithId { get; set; }
    }
}
