namespace Wizzarts.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class WizzartsGameRulesData : BaseDeletableModel<int>
    {
        [Comment("Game rule component title. Seeded")]
        public string Title { get; set; } = string.Empty;

        [Comment("Game rule component desciption. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Comment("Game rule component image url. Seeded")]
        public string ImageUrl { get; set; } = string.Empty;

        public int GameRulesId { get; set; }

        public WizzartsGameRules GameRules { get; set; }
    }
}
