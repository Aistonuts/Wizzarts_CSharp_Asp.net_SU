namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Wizzarts.Data.Common.Models;

    public class WizzartsGameRules : BaseDeletableModel<int>
    {
        public WizzartsGameRules()
        {
            this.GameRulesData = new HashSet<WizzartsGameRulesData>();
        }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string GameRulesIntroUrl { get; set; } = string.Empty;

        public string CardReading { get; set; } = string.Empty;

        public string CardReadingIntroUrl { get; set; } = string.Empty;

        public string CardNameReading { get; set; } = string.Empty;

        public string CardNameUrl { get; set; } = string.Empty;

        public string ManaCostReading { get; set; } = string.Empty;

        public string ManaCostUrl { get; set; } = string.Empty;

        public string CardTypeReading { get; set; } = string.Empty;

        public string CardTypeUrl { get; set; } = string.Empty;

        public string SetSymbolReading { get; set; } = string.Empty;

        public string SetSymbolUrl { get; set; } = string.Empty;

        public string CardTextBoxReading { get; set; } = string.Empty;

        public string CardTextBoxUrl { get; set; } = string.Empty;

        public string CardPowerToughnessReading { get; set; } = string.Empty;

        public string CardPowToughUrl { get; set; } = string.Empty;

        public string BattleFieldSetUp { get; set; } = string.Empty;

        public string BattleFieldIntroUrl { get; set; } = string.Empty;

        public string CreaturesInBattle { get; set; } = string.Empty;

        public string LibraryInBattle { get; set; } = string.Empty;

        public string LandsInBattle { get; set; } = string.Empty;

        public string GraveyardInBattle { get; set; } = string.Empty;

        public string HandInBattle { get; set; } = string.Empty;

        public string GameActions { get; set; } = string.Empty;

        public string TappingUntapping { get; set; } = string.Empty;

        public string CastingSpells { get; set; } = string.Empty;

        public string AttackingAndBlocking { get; set; } = string.Empty;

        public string PartsOfTheTurn { get; set; } = string.Empty;

        public string BeginningPhase { get; set; } = string.Empty;

        public string FirstMainPhase { get; set; } = string.Empty;

        public string CombatPhase { get; set; } = string.Empty;

        public string SecondMainPhase { get; set; } = string.Empty;

        public string EndingPhase { get; set; } = string.Empty;

        public string Outro { get; set; } = string.Empty;

        public string OutroUrl { get; set; } = string.Empty;

        public string PublishedById { get; set; } = string.Empty;

        [ForeignKey(nameof(PublishedById))]
        public WizzartsTeam PublishedBy { get; set; }

        public virtual ICollection<WizzartsGameRulesData> GameRulesData { get; set; }
    }
}
