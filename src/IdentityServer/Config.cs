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
                new Client
                {
                    ClientId = "client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("abrakadabra".Sha256())
                    },

                    AllowedScopes = { "Exadel.Forecast.Api" }
                },

                new Client
                {
                    ClientId = "postman",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes =  { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials },

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
