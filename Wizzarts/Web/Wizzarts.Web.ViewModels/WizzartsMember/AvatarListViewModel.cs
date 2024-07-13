using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Web.ViewModels.Home;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class AvatarListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }
    }
}
