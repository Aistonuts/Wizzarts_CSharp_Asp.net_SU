namespace Wizzarts.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Wizzarts.Data.Common.Models;

    public class DeckStatus : BaseDeletableModel<int>
    {
        public DeckStatus()
        {
            this.Decks = new HashSet<CardDeck>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<CardDeck> Decks { get; set; }
    }
}
