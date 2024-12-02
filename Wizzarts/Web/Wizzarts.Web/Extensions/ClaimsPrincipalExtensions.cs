namespace Wizzarts.Web.Extensions
{
    using System.Security.Claims;

    using static Wizzarts.Common.GlobalConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdministratorRoleName);
        }

        public static bool IsArtist(this ClaimsPrincipal user)
        {
            return user.IsInRole(ArtistRoleName);
        }

        public static bool IsPremiumUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(PremiumRoleName);
        }
    }
}
