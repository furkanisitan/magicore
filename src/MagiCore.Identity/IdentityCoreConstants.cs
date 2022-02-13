using IdentityModel;

namespace MagiCore.Identity;

public static class IdentityCoreConstants
{
    public static class Scopes
    {
        /// <summary>OPTIONAL. This scope value requests access to the <c>role</c> Claims.</summary>
        public const string Roles = "roles";
    }

    public static readonly Dictionary<string, IEnumerable<string>> ScopeToClaimsMapping = new()
    {
        {
            Scopes.Roles,
            new[]
            {
                JwtClaimTypes.Role,
            }
        }
    };

}