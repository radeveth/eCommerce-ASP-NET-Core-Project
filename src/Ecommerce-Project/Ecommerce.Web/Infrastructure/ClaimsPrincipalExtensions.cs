namespace Ecommerce.Web.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.Claims != null ? user.FindFirst(ClaimTypes.NameIdentifier).Value : null;
        }
    }
}
