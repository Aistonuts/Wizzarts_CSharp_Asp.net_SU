﻿#nullable enable
namespace Wizzarts.Web.ViewModels.PlayCard
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;

    public class BaseCardViewModel : IndexAuthenticationViewModel
    {
        // Model view properties
        [Required(ErrorMessage = "Card Name is required!")]
        [StringLength(CardNameMaxLength, MinimumLength = CardNameMinLength, ErrorMessage = "Card name should be between 5 and 30 characters long")]
        public string Name { get; set; } = DefaultCardName;

        public int CardFrameColorId { get; set; }

        public string? CardRemoteUrl { get; set; } = string.Empty;

        public string CardFrameDefaultUrl { get; set; } = DefaultCardFrameUrl;

        public string CardExpansionSymbolDefaultUrl { get; set; } = DefaultGameExpansionUrl;

        public string CardDefaultType { get; set; } = DefaulCardType;

        public string CardDefaultName { get; set; } = DefaultCardName;

        public string CardDefaultDescription { get; set; } = DefaultCardDescription;

        public string CardDefaultImage { get; set; } = DefaultCardImage;

        public int BlackManaCost { get; set; } = DefaultManaValue;

        public int BlueManaCost { get; set; } = DefaultManaValue;

        public int RedManaCost { get; set; } = DefaultManaValue;

        public int WhiteManaCost { get; set; } = DefaultManaValue;

        public int GreenManaCost { get; set; } = DefaultManaValue;

        public int ColorlessManaCost { get; set; } = DefaultManaValue;

        [Range(2, 8, ErrorMessage = "Card type should be defined!")]
        public int CardTypeId { get; set; } = DefaultCardTypeValue;

        public int CardFrameId { get; set; } = DefaultCardFrameValue;

        public int GameExpansionId { get; set; } = DefaultGameExpansionId;

        [Required(ErrorMessage = "Card Abilities are required!")]
        [StringLength(CardAbilitiesAndFlavorMaxLength, MinimumLength = CardAbilitiesAndFlavorMinLength, ErrorMessage = "Card Abilities description should be between 28 and 200 characters long")]
        [Display(Name = "Abilities And Flavor")]
        public string AbilitiesAndFlavor { get; set; } = DefaultCardDescription;

        [StringLength(CardPowerMaxLength, MinimumLength = CardPowerMinLength, ErrorMessage = "Card power should be 1 character long")]
        public string? Power { get; set; }

        [StringLength(CardToughnessMaxLength, MinimumLength = CardToughnessMinLength, ErrorMessage = "Card toughness should be 1 character long")]
        public string? Toughness { get; set; }

        public string? ArtId { get; set; } = string.Empty;
    }
}
