namespace MagicCardsHub.Domain
{
    public class Art
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ArtIstId { get; set; }

        public ProjectCreator Artist { get; set; }

        public string CardId { get; set; }

        public Card Card { get; set; }
    }
}
