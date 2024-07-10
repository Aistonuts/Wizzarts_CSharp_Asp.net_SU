using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    public class AvatarListViewModel
    {
        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }
    }
}
