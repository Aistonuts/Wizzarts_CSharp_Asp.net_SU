using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAspNetCoreApp.Services
{
    public interface IYearsService
    {
        IEnumerable<int> GetLastYears(int count);
    }
}
