namespace Wizzarts.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Models;

    public class WizzartsGameRulesData : BaseDeletableModel<int>
    {
        [Comment("Game rule component title. Seeded")]
        public string Title { get; set; } = string.Empty;

        [Comment("Game rule component description. Seeded")]
        public string Description { get; set; } = string.Empty;

        [Comment("Game rule component image url. Seeded")]
        public string ImageUrl { get; set; } = string.Empty;

        public string WizzartsCardGameId { get; set; }

        [ForeignKey(nameof(WizzartsCardGameId))]
        public WizzartsCardGame WizzartsCardGame { get; set; }
    }
}
