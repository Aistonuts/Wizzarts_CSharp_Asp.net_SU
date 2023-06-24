namespace CardsAssembler.Domain
{
    public class Art
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Description { get; set; }

        public string ImageUrl { get;set; }

        public string ArtIstId { get; set; }

        public CardCreator Artist{ get; set; }

        public string GameCardId { get; set; }

        public Card GameCard { get; set; }

    }
}
