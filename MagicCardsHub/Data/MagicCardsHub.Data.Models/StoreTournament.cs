namespace MagicCardsHub.Data.Models
{
    public class StoreTournament
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
