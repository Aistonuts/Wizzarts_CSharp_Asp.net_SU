namespace MagicCardsmith.Web.Areas.Administration.Models.Users
{
    using System.Collections.Generic;

    using MagicCardsmith.Web.ViewModels;

    public class UserServiceListModel : PagingViewModel
    {
        public IEnumerable<UserServiceInListModel> Users { get; set; }

        public IEnumerable<UserServiceInListModel> Artists { get; set; }

        public IEnumerable<UserServiceInListModel> StoreOwners { get; set; }

        public IEnumerable<UserServiceInListModel> Admins { get; set; }

        public IEnumerable<UserServiceInListModel> Premium { get; set; }
    }
}
