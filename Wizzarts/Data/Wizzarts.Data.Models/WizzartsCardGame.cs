using Wizzarts.Data.Common.Models;

namespace Wizzarts.Data.Models
{
    public class WizzartsCardGame : BaseDeletableModel<string>
    {
        public WizzartsCardGame()
        {
            
        }

        public string Title { get; set; }


    }
}
