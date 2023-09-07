namespace MagicCardsHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MagicCardsHub.Data;
    using MagicCardsHub.Data.Common.Repositories;
    using MagicCardsHub.Data.Models;
    using MagicCardsHub.Services.Mapping;

    public class PlayCardService : IPlayCardService
    {
        private readonly IDeletableEntityRepository<PlayCard> playCardRepository;
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly ApplicationDbContext db;

        public PlayCardService(IDeletableEntityRepository<PlayCard> playCardRepository,IDeletableEntityRepository<Article> articleRepository, ApplicationDbContext db)
        {
            this.playCardRepository = playCardRepository;
            this.articleRepository = articleRepository;
            this.db = db;
        }

   //   public async Task CreateAsync(CreatePlayCardInputModel input, string userId, string imagePath)
   //   {
   //       var playCard = new PlayCard()
   //       {
   //           Name = input.Name,
   //           Description = input.Description,
   //           CardType = input.CardType,
   //           Damage = input.Damage,
   //           Defence = input.Defence,
   //           ArtId = input.ArtId,
   //       };
   //
   //       await this.playCardRepository.AddAsync(playCard);
   //       await this.playCardRepository.SaveChangesAsync();
   //   }

        public T GetById<T>(int id)
        {
            var art = this.articleRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return art;
        }
    }
}
