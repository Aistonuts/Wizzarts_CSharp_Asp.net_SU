namespace MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class ColorlessManaCostViewModel : IMapFrom<ColorlessMana>
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        public string ColorName { get; set; }

        public string ImageUrl { get; set; }
    }
}
