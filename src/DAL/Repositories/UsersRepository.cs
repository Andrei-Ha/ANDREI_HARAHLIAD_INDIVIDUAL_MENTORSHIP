using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Services;
using IdentityModel.Client;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _url;
        private ClientCredentialsTokenRequest _clientCredentialsRequest;

        public UsersRepository(string url, ClientCredentialsTokenRequest clientCredentialsRequest)
        {
            _url = url;
            _clientCredentialsRequest = clientCredentialsRequest;
        }

        public async Task<List<IdentityUser>> GetUsersAsync(string id = "")
        {
            string webUrl = _url + $"?id={id}";
            var requestSender = new RequestSender<List<IdentityUser>>(webUrl);

            var tokenResponse = await requestSender.GetClientCredentialsTokenAsync(_clientCredentialsRequest);

            if (tokenResponse.IsError)
            {
                throw new HttpRequestException($"An error occurred while getting the client credential token from the identity server.", null, System.Net.HttpStatusCode.UnprocessableEntity);
            }
            requestSender.SetBearerAccessToken(tokenResponse.AccessToken);

            return await requestSender.GetModelAsync();
        }
    }
}
