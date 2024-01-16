namespace MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class CardTypeViewModel : IMapFrom<CardType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
