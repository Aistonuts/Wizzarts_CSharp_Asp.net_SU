namespace MagicCardsHub.Services.Data
{
    using System.Threading.Tasks;

   

    public interface IPlayCardService
    {
       // Task CreateAsync(CreatePlayCardInputModel input, string userId, string imagePath);

        T GetById<T>(int id);
    }
}
