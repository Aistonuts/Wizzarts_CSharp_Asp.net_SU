namespace Wizzarts.Web.ViewModels.GameRules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class GameRulesViewModel : IMapFrom<WizzartsGameRules>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string GameRulesIntroUrl { get; set; }

        public string CardReading { get; set; }

        public string CardReadingIntroUrl { get; set; }

        public string CardNameReading { get; set; }

        public string CardNameUrl { get; set; }

        public string ManaCostReading { get; set; }

        public string ManaCostUrl { get; set; }

        public string CardTypeReading { get; set; }

        public string CardTypeUrl { get; set; }

        public string SetSymbolReading { get; set; }

        public string SetSymbolUrl { get; set; }

        public string CardTextBoxReading { get; set; }

        public string CardTextBoxUrl { get; set; }

        public string CardPowerToughnessReading { get; set; }

        public string CardPowToughUrl { get; set; }

        public string BattleFieldSetUp { get; set; }

        public string BattleFieldIntroUrl { get; set; }

        public string CreaturesInBattle { get; set; }

        public string LibraryInBattle { get; set; }

        public string LandsInBattle { get; set; }

        public string GraveyardInBattle { get; set; }

        public string HandInBattle { get; set; }

        public string GameActions { get; set; }

        public string TappingUntapping { get; set; }

        public string CastingSpells { get; set; }

        public string AttackingAndBlocking { get; set; }

        public string PartsOfTheTurn { get; set; }

        public string BeginningPhase { get; set; }

        public string FirstMainPhase { get; set; }

        public string CombatPhase { get; set; }

        public string SecondMainPhase { get; set; }

        public string EndingPhase { get; set; }

        public string Outro { get; set; }

        public string OutroUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
