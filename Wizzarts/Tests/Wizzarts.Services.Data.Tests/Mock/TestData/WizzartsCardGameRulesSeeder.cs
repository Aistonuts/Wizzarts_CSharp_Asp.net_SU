namespace Wizzarts.Services.Data.Tests.Mock
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Wizzarts.Data;
    using Wizzarts.Data.Models;

    public class WizzartsCardGameRulesSeeder : ITestDbSeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.WizzartsGameRules.Any())
            {
                return;
            }

            await dbContext.WizzartsGameRules.AddAsync(new WizzartsGameRules
            {
                Title = "Wizzarts card game rules",
                PublishedById = "2b346dc6-5bd7-4e64-8396-15a064aa27a7",
                Description = "Welcome to Wizzarts!" +
                "Play with your friends in a game that lets you explore rich worlds, discover unique strategies, and develop your skills. " +
                "Each player starts the game with 20 life. Get your opponent's 20 life points down to 0 and you win the game.\r\n\r\n" +
                "Here you'll find everything you need to learn how to play Wizzarts card game.",
                GameRulesIntroUrl = "/images/navigation/rulesNav/MCSGameRules.png",
                CardReading = "Card reading is a very important part of the game. A Wizzarts card game has a name, mana cost, type, symbol, textbox with abilities and flavor and power and toughness.",
                CardReadingIntroUrl = "/images/navigation/rulesNav/MGCCardComponents.jpg",
                CardNameReading = "Card name : You can only have up to four cards of the same name in a single deck, except for Basic Lands, which are unlimited",
                CardNameUrl = "/images/navigation/rulesNav/MGCName.jpg",
                ManaCostReading = "Mana cost : Mana is used to cast spells. To cast a spell you will have to play certain amount of black, red, green, white, blue or a colorless mana.",
                ManaCostUrl = "/images/navigation/rulesNav/MGCMana.jpg",
                CardTypeReading = "Card type : Every card in Wizzarts card game has a type, such as Land, Creature, Artifact, Enchantment, Instant and Sorcery",
                CardTypeUrl = "/images/navigation/rulesNav/MGCType.jpg",
                SetSymbolReading = "Set symbol : The color of the card symbol indicates rarity - black - and - white is common, silver is uncommon, gold is rare",
                SetSymbolUrl = "/images/navigation/rulesNav/MGCExpansion.jpg",
                CardTextBoxReading = "Card text : Rules text explains special effect a card has during gameplay. Flavor text does not affect gameplay, but exists to provide the backstory of the card.",
                CardTextBoxUrl = "/images/navigation/rulesNav/MGCTextBox.jpg",
                CardPowerToughnessReading = "Card power and toughness : The left number shows a creature's power (damage it deals) and the right number shows toughness (damage it takes to destroy it).",
                CardPowToughUrl = "/images/navigation/rulesNav/MGCPowerToughness.jpg",
                BattleFieldSetUp = "In Wizzarts card game, a spell is any type of card cast by a player. Spells are usually cast from your hand, but in special cases can be cast from other areas of the battlefield like your library or your graveyard. Land cards are the only type of card that is not considered a spell.",
                BattleFieldIntroUrl = "/images/navigation/rulesNav/GameRulesBattlefield.png",
                CreaturesInBattle = "Creatures in battle : Creatures cards stay on the battlefield until they're defeated by spells or combat.",
                LibraryInBattle = "Library : This is where you put your deck of cards, face-down. At the beginning of the game, you will shuffle your library, then draw seven cards.",
                LandsInBattle = "Lands : You will play up to one land card each turn (face-up). These cards create mana which you can use to cast spells!",
                GraveyardInBattle = "Graveyard : When cards are destroyed or discarded they will be placed here face-up and any play can look at them.",
                HandInBattle = "Hand : Do not show these cards to anyone. At the beginning of your turn, draw one card (more depending on spells). If you have more than seven cards at the end of your turn, discard down to seven.",
                GameActions = "GAME ACTIONS\r\nThis section describes the actions you'll take during a game, including tapping your cards, casting spells, and attacking/blocking with creatures in combat.",
                TappingUntapping = "TAPPING AND UNTAPPING\r\nTo tap a card is to turn it sideways to show that it has been used for the turn. You do this when you use a land to make mana, when you attack with a creature, or when you activate an ability that has the  symbol as part of its cost ( means \"tap this permanent\"). When a permanent is tapped, you can’t tap it again until it’s been untapped (turned back upright). As your turn begins, untap your tapped cards so you can use them again.",
                CastingSpells = "CASTING SPELLS\r\nTo cast a spell, you must pay its mana cost (located in the upper right corner of the card) by tapping lands (or other permanents) to make the amount and type of mana which that spell requires. For example, if you were casting Serra Angel, which costs , you could tap three basic lands of any type to pay  plus two Plains to pay .\r\n\r\nOnce a spell has been cast, one of two things happens. If the spell is an instant or a sorcery, you follow the instructions on the card, and then you put the card into your graveyard. If the spell is a creature, artifact, or enchantment, you put the card on the table in front of you. The card is now on the battlefield.\r\n\r\nCards on the battlefield are called permanents to differentiate them from instants and sorceries, which are never on the battlefield.",
                AttackingAndBlocking = "ATTACKING AND BLOCKING\r\nThe most common way to win the game is to attack with your creatures. If a creature that is attacking an opponent isn’t blocked, it deals damage equal to its power to that opponent.\r\n\r\nThe middle phase of each turn is the combat phase. In your combat phase, you choose which of your creatures will attack, and you choose which opponents they will attack. Tap your creatures to show that they are attacking. Your opponents then choose which of their creatures will block, if any. Tapped creatures can’t be declared as blockers.\r\n\r\nOnce all blockers have been chosen, each creature—both attackers and blockers—simultaneously deals damage equal to its power (the number on the left side of the slash in the lower right corner of the card).\r\n\r\nAn attacking creature that isn’t blocked deals damage to the player it’s attacking.\r\nAn attacking creature that is blocked deals damage to the creature or creatures that are blocking it, and vice versa.\r\nIf damage is dealt to your opponent, they lose that much life.\r\n\r\nIf one of your attacking creatures is blocked by multiple creatures, you decide how to divide its combat damage among them. You must assign at least enough damage to the first blocking creature to destroy it before you can assign damage to the second one, and so on.\r\n\r\nIf a creature is dealt damage equal to or greater than its toughness over the course of a single turn (whether it be combat damage, damage from spells or abilities, or a combination of both), that creature is destroyed, and it goes to its owner’s graveyard (or \"dies\"). If a creature takes damage that isn’t enough to destroy it in a single turn, that creature stays on the battlefield, and the damage wears off at the end of the turn.",
                PartsOfTheTurn = "PARTS OF THE TURN\r\nEach turn proceeds in the same sequence. Whenever you enter a new step or phase, any triggered abilities that happen during that step or phase trigger and are put on the stack. The active player (the player whose turn it is) gets to start casting spells and activating abilities, then each other player in turn order will too. When all players decline to do anything more and nothing is on the stack waiting to resolve, the game will move to the next step.",
                BeginningPhase = "BEGINNING PHASE\r\nUntap Step\r\nYou untap all your tapped permanents. On the first turn of the game, you don’t have any permanents, so you just skip this step. No one can cast spells or activate abilities during this step.\r\nUpkeep Step\r\nPlayers can cast instants and activate abilities. This part of the turn is mentioned on a number of cards. If something is supposed to happen just once per turn, right at the beginning, an ability will trigger \"at the beginning of your upkeep.\"\r\nDraw Step\r\nYou must draw a card from your library (even if you don’t want to). The player who goes first in a two-player game skips the draw step on their first turn to make up for the advantage of going first. Players can then cast instants and activate abilities.",
                FirstMainPhase = "FIRST MAIN PHASE\r\nYou can cast any number of sorceries, instants, creatures, artifacts, enchantments, and planeswalkers, and you can activate abilities. You can play a land during this phase, but remember that you can play only one land during your turn. Your opponent can cast instants and activate abilities.",
                CombatPhase = "COMBAT PHASE\r\nBeginning of Combat Step\r\nPlayers can cast instants and activate abilities.\r\nDeclare Attackers Step\r\nYou decide which, if any, of your untapped creatures will attack, and which player or planeswalker they will attack. This taps the attacking creatures. Players can then cast instants and activate abilities.\r\nDeclare Blockers Step\r\nYour opponent decides which, if any, of their untapped creatures will block your attacking creatures. If multiple creatures block a single attacker, you order the blockers to show which will be first to receive damage, which will be second, and so on. Players can then cast instants and activate abilities.\r\nCombat Damage Step\r\nEach attacking or blocking creature that’s still on the battlefield assigns its combat damage to the defending player (if it’s attacking that player and wasn’t blocked), to a planeswalker (if it’s attacking that planeswalker and wasn’t blocked), to the creature or creatures blocking it, or to the creature it’s blocking. If an attacking creature is blocked by multiple creatures, you divide its combat damage among them by assigning at least enough damage to the first blocking creature to destroy it, then by assigning damage to the second one, and so on. Once players decide how the creatures they control will deal their combat damage, the damage is all dealt at the same time. Players can then cast instants and activate abilities.\r\nEnd of Combat Step\r\nPlayers can cast instants and activate abilities.",
                SecondMainPhase = "SECOND MAIN PHASE\r\nYour second main phase is just like your first main phase. You can cast any type of spell and activate abilities, but your opponent can only cast instants and activate abilities. You can play a land during this phase if you didn’t play one during your first main phase.",
                EndingPhase = "ENDING PHASE\r\nEnd Step\r\nAbilities that trigger “at the beginning of your end step” go on the stack. Players can cast instants and activate abilities.\r\nCleanup Step\r\nIf you have more than seven cards in your hand, choose and discard cards until you have only seven. Next, all damage on creatures is removed and all “until end of turn” effects end. No one can cast instants or activate abilities unless an ability triggers during this step.",
                Outro = "Magic Cardsmith is a collectible trading card game of fun-filled, strategic games to play with friends old and new. Welcoming worldbuilders, narrative lovers, and gameplay enthusiasts alike, Magic has something for everyone and countless ways to play.",
                OutroUrl = "/images/navigation/rulesNav/cloudsAlt.png",
                WizzartsCardGameId = "60e5f43e-cfa7-497d-80b3-d266a934dafa",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
