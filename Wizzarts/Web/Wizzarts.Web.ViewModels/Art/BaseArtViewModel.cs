namespace Wizzarts.Web.ViewModels.Art
{
    using System.ComponentModel.DataAnnotations;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Data.Models;
    using static Wizzarts.Common.DataConstants;

    public class BaseArtViewModel : IndexAuthenticationViewModel, IMapFrom<Art>
    {
        [Required(ErrorMessage = "Art title is required!")]
        [StringLength(ArtTitleMaxLength, MinimumLength = ArtTitleMinLength, ErrorMessage = "Art title should be between 1 and 30 characters long")]
        public string Title { get; set; } = null!;

        [StringLength(ArtDescriptionMaxLength, MinimumLength = ArtDescriptionMinLength, ErrorMessage = "Art description is not required however it  should be between 2 and 500 characters long")]
        public string Description { get; set; } = null!;

        public string RemoteImageUrl { get; set; } = null!;
    }
}
