

using Microsoft.AspNetCore.Identity;

namespace MagicCardsHub.Domain
{
    
    public class ProjectCreator : IdentityUser
    {
        public ProjectCreator()
        {
            this.GameSeasons = new List<GameSeason>();
            this.ArtPieces = new List<Art>();
        }

        public ICollection<GameSeason> GameSeasons { get; set; }
        public ICollection<Art> ArtPieces { get; set; }
    }
}
