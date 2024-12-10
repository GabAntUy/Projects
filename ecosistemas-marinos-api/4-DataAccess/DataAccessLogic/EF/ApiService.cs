using BusinessLogic.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DataAccessLogic.EF
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public List<Pais> ObtenerPaisesDeApi()
        {
            string apiUrl = "https://restcountries.com/v3.1/all?fields=name,cca2";
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _httpClient.Send(request);
            response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().Result;
            var paises = JsonConvert.DeserializeObject<List<PaisTemp>>(responseBody);

            return ConvertApiDataToPaises(paises);
        }

        private List<Pais> ConvertApiDataToPaises(List<PaisTemp> apiData)
        {
            return apiData.Select(p => new Pais
            {
                CodigoIso = p.Cca2,
                Nombre = p.Name.Common
            }).ToList();
        }
    }
    public class PaisTemp
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("cca2")]
        public string Cca2 { get; set; }
    }

    public class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }
    }
    
}


