namespace Wizzarts.Web.ViewModels.GameRules
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class GameRulesDataViewModel : IMapFrom<WizzartsGameRulesData>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
