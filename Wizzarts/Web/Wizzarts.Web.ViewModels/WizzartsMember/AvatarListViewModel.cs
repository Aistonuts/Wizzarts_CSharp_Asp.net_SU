namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using System.Collections.Generic;

    using Wizzarts.Web.ViewModels.Home;

    public class AvatarListViewModel : IndexAuthenticationViewModel
    {
        public IEnumerable<AvatarInListViewModel> Avatars { get; set; }
    }
}
