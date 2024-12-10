using BusinessLogic.RepoInterfaces;
using System.Text;
using Microsoft.Extensions.Options;
using BusinessLogic.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccessLogic.Api
{
    public class HttpClientContext : IHttpClientContext
    {
        private readonly HttpClient _httpClient;
        private string _urlBase;
        private IServiceToken _token;

        public HttpClientContext(
            HttpClient httpClient, 
            IOptions<ApiConf> urlBase, 
            IServiceToken token)
        {
            _httpClient = httpClient;
            _token = token;
            _urlBase = urlBase.Value.BaseUrl;
            _httpClient.BaseAddress = new Uri(_urlBase);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token.GetToken());
        }

        public HttpResponseMessage Delete(string uri)
        {
            return _httpClient.DeleteAsync(uri).Result;
        }

        public HttpResponseMessage Get(string uri)
        {
            return _httpClient.GetAsync(uri).Result;
        }

        public HttpResponseMessage Post<T>(string uri, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(uri, content).Result;
        }

        public HttpResponseMessage Put<T>(string uri, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return _httpClient.PutAsync(uri, content).Result;
        }
    }
}
