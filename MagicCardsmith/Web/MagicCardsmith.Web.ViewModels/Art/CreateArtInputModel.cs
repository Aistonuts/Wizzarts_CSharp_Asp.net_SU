namespace MagicCardsmith.Web.ViewModels.Art
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateArtInputModel : BaseCreateArtInputModel
    {
        [Required(ErrorMessage = "Art image is required!")]
        public IFormFile Image { get; set; }
    }
}
