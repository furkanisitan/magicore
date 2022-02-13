using Duende.IdentityServer.Models;

namespace MagiCore.Identity;

/// <summary>
/// Convenience class that defines standard identity resources.
/// </summary>
public class IdentityCoreResources
{
    /// <summary>
    /// Models the standard role scope
    /// </summary>
    /// <seealso cref="IdentityResource" />
    public class Role : IdentityResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResources.Email"/> class.
        /// </summary>
        public Role()
        {
            Name = IdentityCoreConstants.Scopes.Roles;
            DisplayName = "Your roles";
            UserClaims = (IdentityCoreConstants.ScopeToClaimsMapping[IdentityCoreConstants.Scopes.Roles].ToList());
        }
    }
}