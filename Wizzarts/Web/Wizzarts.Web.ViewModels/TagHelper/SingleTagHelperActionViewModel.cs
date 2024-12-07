namespace Wizzarts.Web.ViewModels.TagHelper
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class SingleTagHelperActionViewModel : IMapFrom<TagHelpAction>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
