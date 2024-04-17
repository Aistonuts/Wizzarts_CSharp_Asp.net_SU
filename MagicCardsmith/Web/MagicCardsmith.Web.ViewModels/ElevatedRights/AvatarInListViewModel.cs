namespace MagicCardsmith.Web.ViewModels.ElevatedRights
{
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Services.Mapping;

    public class AvatarInListViewModel : IMapFrom<Avatar>
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }
    }
}
