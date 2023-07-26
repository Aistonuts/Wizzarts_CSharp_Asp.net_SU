namespace MagicCardsHub.Web.ViewModels.DigitalArt
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MagicCardsHub.Web.ViewModels.BaseCreateModel;
    using Microsoft.AspNetCore.Http;

    public class CreateDigitalArtInputModel : BaseCreateImageInputModel
    {
        [MinLength(10)]
        public string Description { get; set; }
    }
}
