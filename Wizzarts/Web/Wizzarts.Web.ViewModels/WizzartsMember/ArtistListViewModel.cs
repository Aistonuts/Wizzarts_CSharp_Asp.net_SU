using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Web.ViewModels.Home;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class ArtistListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<ArtistsInListViewModel> Artists { get; set; }
    }
}
