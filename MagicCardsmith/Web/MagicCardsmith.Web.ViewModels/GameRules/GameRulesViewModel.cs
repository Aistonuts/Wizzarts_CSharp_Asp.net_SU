namespace MagicCardsmith.Web.ViewModels.GameRules
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class GameRulesViewModel : IMapFrom<MagicCardsmithGameRules>, IHaveCustomMappings
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

        public string Publisher { get; set; }

        public string PublisherAvatarUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<RulesComponentsListViewModel> GameRulesComponents { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MagicCardsmithGameRules, GameRulesViewModel>()
                .ForMember(x => x.Publisher, opt =>
                opt.MapFrom(x =>
                x.PublishedBy.UserName));
            configuration.CreateMap<MagicCardsmithGameRules, GameRulesViewModel>()
               .ForMember(x => x.PublisherAvatarUrl, opt =>
               opt.MapFrom(x =>
               x.PublishedBy.AvatarUrl));
        }
    }
}
