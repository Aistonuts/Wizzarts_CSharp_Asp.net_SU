namespace CardsAssembler.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    public class CardCreator : IdentityUser
    {
        public CardCreator()
        {             
            this.Cards = new List<CardAssembly>();
            this.CardsArt = new List<Art>();
        }       

        public ICollection<CardAssembly> Cards{ get; set; }  
        public ICollection<Art> CardsArt { get; set; }

        
    }
}
