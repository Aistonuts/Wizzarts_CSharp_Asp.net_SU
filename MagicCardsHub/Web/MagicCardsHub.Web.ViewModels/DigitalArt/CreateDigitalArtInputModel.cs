namespace MagicCardsHub.Web.ViewModels.DigitalArt
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateDigitalArtInputModel
    {
        [MinLength(10)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<IFormFile> Image { get; set; }
    }
}
