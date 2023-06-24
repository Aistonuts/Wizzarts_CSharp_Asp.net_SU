namespace CardsAssembler.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    public class CardCreator : IdentityUser
    {
        public CardCreator()
        {             
            this.CardProjects = new List<CardAssembly>();
            this.GameCardsArt = new List<Art>();
        }
        public CardCreator UserRole { get; set; }

        public ICollection<CardAssembly> CardProjects { get; set; }  
        public ICollection<Art> GameCardsArt { get; set; }

        
    }
}
