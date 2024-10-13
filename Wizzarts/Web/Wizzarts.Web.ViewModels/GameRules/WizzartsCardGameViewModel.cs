namespace Wizzarts.Web.ViewModels.GameRules
{
    using System.Collections.Generic;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public class WizzartsCardGameViewModel : IndexAuthenticationViewModel, IMapFrom<WizzartsCardGame>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public GameRulesViewModel CardGameRules { get; set; }

    }
}
