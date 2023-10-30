namespace MagicCardsHub.Web.ViewModels.Art
{
    using Microsoft.AspNetCore.Http;

    public class CreateArtInputModel : BaseCreateArtInputModel
    {
        public IFormFile Image { get; set; }
    }
}
