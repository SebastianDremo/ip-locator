using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using locator.Core.Entities;
using locator.Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace locator.Web.Services
{
    public class IpStackService : IIpStackService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _ipStackApiKey;

        public IpStackService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _ipStackApiKey = _configuration.GetSection("IpStackKey").Value;
        }

        public Task<Localization> GetLocalizationByDnsAsync(string dns)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Localization> GetLocalizationByIpAsync(string ip)
        {
            var url = $"http://api.ipstack.com/{ip}?access_key={_ipStackApiKey}&fields=ip,country_name,city,latitude,longitude";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Localization>(responseStream);
        }
    }
}