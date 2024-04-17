namespace MagicCardsmith.Web.ViewModels.Card
{
    public abstract class BaseCreateCardInputModel
    {
        public const int DefaultManaValue = 1;
        public const int DefaultCardTypeValue = 1;

        public const int DefaultCardFrameValue = 3;

        public const int DefaultGameExpansionId = 2;

        public const string DefaultCardName = "Unknown Card. Nameless warrior or spell.";

        public const string DefaultCardFrameUrl = "/images/frames/createcard.jpg";

        public const string DefaultGameExpansionUrl = "/images/symbols/expansions/Beta.png";

        public const string DefaulCardType = "Not Defined Yet!!!";

        public string Name { get; set; }

        public int CardFrameColorId { get; set; }

        public string CardRemoteUrl { get; set; }

        public string EventMilestoneImage { get; set; }

        public string EventMilestoneTitle { get; set; }

        public string EventMilestoneDescription { get; set; }

        public string EventDescription { get; set; }

        public string CardFrameDefaultUrl { get; set; } = DefaultCardFrameUrl;

        public string CardExpansionSymbolDefaultUrl { get; set; } = DefaultGameExpansionUrl;

        public string CardDefaultType { get; set; } = DefaulCardType;

        public string CardDefaultName { get; set; } = DefaultCardName;

        public int BlackManaId { get; set; } = DefaultManaValue;

        public int BlueManaId { get; set; } = DefaultManaValue;

        public int RedManaId { get; set; } = DefaultManaValue;

        public int WhiteManaId { get; set; } = DefaultManaValue;

        public int GreenManaId { get; set; } = DefaultManaValue;

        public int ColorlessManaId { get; set; } = DefaultManaValue;

        public int CardTypeId { get; set; } = DefaultCardTypeValue;

        public int CardFrameId { get; set; } = DefaultCardFrameValue;

        public int GameExpansionId { get; set; } = DefaultGameExpansionId;

        public string AbilitiesAndFlavor { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public string ArtId { get; set; }

        public string CardSmithId { get; set; }
    }
}
