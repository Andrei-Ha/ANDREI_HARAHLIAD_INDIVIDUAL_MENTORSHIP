using IdentityServer4.Models;

namespace Exadel.Forecast.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("Exadel.Forecast.Api", "My Forecast API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
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
                }
            };
    }
}
