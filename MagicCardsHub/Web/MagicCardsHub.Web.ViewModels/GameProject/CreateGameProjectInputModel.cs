namespace MagicCardsHub.Web.ViewModels.GameProject
{
    using System.ComponentModel.DataAnnotations;

    using MagicCardsHub.Web.ViewModels.BaseCreateModel;

    public class CreateGameProjectInputModel : BaseCreateImageInputModel
    {
        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        [Range(1, 20)]
        public int NumberOfCards { get; set; }
    }
}
