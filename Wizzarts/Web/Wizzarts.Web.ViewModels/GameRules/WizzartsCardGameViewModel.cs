namespace Wizzarts.Web.ViewModels.GameRules
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class WizzartsCardGameViewModel : IndexAuthenticationViewModel, IMapFrom<WizzartsCardGame>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public GameRulesViewModel CardGameRules { get; set; }
    }
}
