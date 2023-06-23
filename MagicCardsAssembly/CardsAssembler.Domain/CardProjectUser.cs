namespace CardsAssembler.Domain
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    public class CardProjectUser : IdentityUser
    {
        public CardProjectUser()
        {             
            this.Projects = new List<GameProjectCard>();
            this.CardsArt = new List<Art>();
        }   

        public ICollection<GameProjectCard> Projects { get; set; }  
        public ICollection<Art> CardsArt { get; set; }

        
    }
}
