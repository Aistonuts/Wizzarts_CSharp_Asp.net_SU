namespace Wizzarts.Web.ViewModels.TagHelper
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;

    public class SingleTagHelpControllerViewModel : IMapFrom<TagHelpController>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
