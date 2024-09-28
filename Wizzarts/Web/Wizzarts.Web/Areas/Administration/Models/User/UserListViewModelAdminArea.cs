using System.Collections.Generic;
using Wizzarts.Web.ViewModels;

namespace Wizzarts.Web.Areas.Administration.Models.User
{
    public class UserListViewModelAdminArea : PagingViewModel
    {
        public IEnumerable<UserInListViewModelAdminArea> Users { get; set; }

        public IEnumerable<UserInListViewModelAdminArea> Artists { get; set; }

        public IEnumerable<UserInListViewModelAdminArea> StoreOwners { get; set; }

        public IEnumerable<UserInListViewModelAdminArea> Admins { get; set; }

        public IEnumerable<UserInListViewModelAdminArea> Premium { get; set; }
    }
}
