namespace MagicCardsmith.Data.Models
{
    using MagicCardsmith.Data.Common.Models;

    public class GameRulesComponents : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int GameRulesId { get; set; }

        public MagicCardsmithGameRules GameRules { get; set; }
    }
}
