namespace MagicCardsmith.Web.ViewModels.GameRules
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class RulesComponentsListViewModel : IMapFrom<GameRulesComponents>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
