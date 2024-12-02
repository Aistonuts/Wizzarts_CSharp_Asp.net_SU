namespace Wizzarts.Web.ViewModels.Art
{
    using System.ComponentModel.DataAnnotations;

    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Home;

    using static Wizzarts.Common.DataConstants;
    using static Wizzarts.Common.MessageConstants;

    public class BaseArtViewModel : IndexAuthenticationViewModel, IMapFrom<Art>, ISingleArtViewModel, ISingleEventViewModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(ArtTitleMaxLength, MinimumLength = ArtTitleMinLength, ErrorMessage = LengthMessage)]
        public string Title { get; set; } = null!;

        [StringLength(ArtDescriptionMaxLength, MinimumLength = ArtDescriptionMinLength, ErrorMessage = LengthMessage)]
        public string Description { get; set; } = null!;

        public string RemoteImageUrl { get; set; } = null!;
    }
}
