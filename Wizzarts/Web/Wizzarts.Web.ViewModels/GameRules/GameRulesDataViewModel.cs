namespace Wizzarts.Web.ViewModels.GameRules
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class GameRulesDataViewModel : IMapFrom<WizzartsGameRulesData>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
