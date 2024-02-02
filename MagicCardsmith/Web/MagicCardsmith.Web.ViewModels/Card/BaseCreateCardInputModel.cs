namespace MagicCardsmith.Web.ViewModels.Card
{
    using System.Net.NetworkInformation;

    public abstract class BaseCreateCardInputModel
    {
        public string Name { get; set; }

        public int CardFrameColorId { get; set; }

        public string CardRemoteUrl { get; set; }

        public string EventMilestoneImage { get; set; }

        public string EventMilestoneDescription { get; set; }

        public string EventDescription { get; set; }

        public int BlackManaId { get; set; }

        public int BlueManaId { get; set; }

        public int RedManaId { get; set; }

        public int WhiteManaId { get; set; }

        public int GreenManaId { get; set; }

        public int ColorlessManaId { get; set; }

        public int CardTypeId { get; set; }

        public int CardFrameId { get; set; }

        public int GameExpansionId { get; set; }

        public string AbilitiesAndFlavor { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }

        public string ArtId { get; set; }

        public string CardSmithId { get; set; }
    }
}
