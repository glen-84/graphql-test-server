using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GraphQLTestServer.Types;

[MutationType]
public static class Mutations
{
    public static async Task<bool> SignInAsync([Service] IHttpContextAccessor httpContextAccessor)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "admin@example.com"),
            new("FullName", "Admin"),
            new(ClaimTypes.Role, "Administrator"),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return true;
    }
}
