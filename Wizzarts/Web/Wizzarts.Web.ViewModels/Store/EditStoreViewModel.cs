namespace Wizzarts.Web.ViewModels.Store
{
    public class EditStoreViewModel : BaseStoreViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; } = string.Empty;

        public string StoreOwner { get; set; } = string.Empty;
    }
}
