namespace Wizzarts.Web.ViewModels.Deck
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class BaseDeckViewModel : IndexAuthenticationViewModel, IMapFrom<CardDeck>/*, IHaveCustomMappings*/
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CardDeckNameMaxLength, MinimumLength = CardDeckNameMinLength, ErrorMessage = LengthMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CardDeckDescriptionMaxMaxLength, MinimumLength = CardDeckDescriptionMinLength, ErrorMessage = LengthMessage)]
        public string Description { get; set; }

        // [Required(ErrorMessage = RequiredMessage)]
        // [StringLength(CardDeckShippingAddressMaxLength, MinimumLength = CardDeckShippingAddressMinLength, ErrorMessage = LengthMessage)]
        public string ShippingAddress { get; set; }

        public int StoreId { get; set; }

        public int EventId { get; set; }
    }
}
