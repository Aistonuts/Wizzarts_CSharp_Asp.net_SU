namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class GameRulesComponents : BaseDeletableModel<int>
    {
        [Comment("Game rule component title. Seeded")]
        public string Title { get; set; } = string.Empty;

        [Comment("Game rule component desciption. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Comment("Game rule component image url. Seeded")]
        public string ImageUrl { get; set; } = string.Empty;

        public int GameRulesId { get; set; }

        public MagicCardsmithGameRules GameRules { get; set; }
    }
}
