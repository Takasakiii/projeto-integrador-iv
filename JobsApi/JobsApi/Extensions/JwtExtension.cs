using System.Security.Claims;
using JobsApi.Exceptions;

namespace JobsApi.Extensions;

public static class JwtExtension
{
    private static string? GetClaimByName(this ClaimsPrincipal claims, string name)
    {
        var identity = claims.Identities.FirstOrDefault();

        var claim = identity?.Claims.FirstOrDefault(x => x.Type == name);

        return claim?.Value;
    }

    public static uint GetId(this ClaimsPrincipal claims)
    {
        var value = GetClaimByName(claims, ClaimTypes.NameIdentifier);

        if (value is null)
        {
            throw new JwtException("Id is not found");
        }

        return uint.Parse(value);
    }

    private static string? GetClaimByName(this IEnumerable<Claim> claims, string name)
    {
        var claim = claims.FirstOrDefault(x => x.Type == name);

        return claim?.Value;
    }

    public static uint GetId(this IEnumerable<Claim> claims)
    {
        var value = GetClaimByName(claims, "nameid");

        if (value is null)
        {
            throw new JwtException("Id is not found");
        }

        return uint.Parse(value);
    }
}