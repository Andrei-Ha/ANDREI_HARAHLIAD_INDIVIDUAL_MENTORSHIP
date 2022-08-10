using Exadel.Forecast.DAL.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Services
{
    public class RequestSender<T>
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _webUrl;
        private readonly Dictionary<string, string> _headers;

        public RequestSender(string webUrl, Dictionary<string, string> headers = default)
        {
            _webUrl = webUrl;
            _headers = headers;
        }

        private async Task<T> Get(CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _webUrl);

            if (_headers != null)
            {
                foreach (var header in _headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request, token);
            response.EnsureSuccessStatusCode();
            string strModel = await response.Content.ReadAsStringAsync(token);
            return JsonConvert.DeserializeObject<T>(strModel);
        }

        public void SetBearerAccessToken(string bearerToken)
        {
            _httpClient.SetBearerToken(bearerToken);
        }

        public async Task<TokenResponse> GetClientCredentialsTokenAsync(ClientCredentialsTokenRequest clientCredentialsTokenRequest)
        {
            return await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
        }

        public async Task<T> GetModelAsync(CancellationToken token = default)
        {
            try
            {
                return await Get(token);
            }
            catch (Exception ex)
            {
                // Logger(ex.Message)
                Console.WriteLine(ex.Message);
            }

            return default;
        }

        public async Task<DebugModel<T>> GetDebugModelAsync(CancellationToken token = default)
        {
            var debugModel = new DebugModel<T>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                T model = await Get(token);
                if (model != null)
                {
                    debugModel.Model = model;
                }
            }
            catch (Exception e)
            {
                debugModel.TextException = e.Message;
            }

            stopwatch.Stop();
            debugModel.RequestDuration = stopwatch.ElapsedMilliseconds;
            return debugModel;
        }
    }
}
