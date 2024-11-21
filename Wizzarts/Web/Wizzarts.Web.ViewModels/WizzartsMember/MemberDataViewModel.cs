namespace Wizzarts.Web.ViewModels.WizzartsMember
{
    using Wizzarts.Web.ViewModels.Home;

    public class MemberDataViewModel : IndexAuthenticationViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Nickname { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;
    }
}