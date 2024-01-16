namespace MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class FrameColorViewModel : IMapFrom<CardFrameColor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
