namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Wizzarts.Data.Common.Models;

    public class WizzartsCardGame : BaseDeletableModel<string>
    {
        public WizzartsCardGame()
        {
            this.WizzartsTeamMembers = new HashSet<WizzartsTeam>();
            this.GameRulesData = new HashSet<WizzartsGameRulesData>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CardGameRulesId { get; set; }

        public WizzartsGameRules CardGameRules { get; set; }

        public virtual ICollection<WizzartsTeam> WizzartsTeamMembers { get; set; }

        public virtual ICollection<WizzartsGameRulesData> GameRulesData { get; set; }
    }
}
