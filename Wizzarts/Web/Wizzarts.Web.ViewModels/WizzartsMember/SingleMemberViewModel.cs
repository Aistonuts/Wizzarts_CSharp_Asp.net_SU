namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Home;

    public class SingleMemberViewModel : IndexAuthenticationViewModel, IMapFrom<ApplicationUser>
    {
    }
}
