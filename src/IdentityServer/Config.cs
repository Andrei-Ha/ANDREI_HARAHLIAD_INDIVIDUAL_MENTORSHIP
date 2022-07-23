using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Exadel.Forecast.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("Exadel.Forecast.Api", "My Forecast API", new [] { JwtClaimTypes.Email }),
                new ApiScope("postman", "Postman", new [] { JwtClaimTypes.Role })
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("abrakadabra".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "Exadel.Forecast.Api" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "postman",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    //AllowedGrantTypes = GrantTypes.Code,
                    AllowedGrantTypes =  { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials },

                    // where to redirect to after login
                    //RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    //PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        "postman",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
    }
}
