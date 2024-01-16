using WebCrawler.Application.Extensions;
using WebCrawler.Core.Entities;

namespace WebCrawler.Application.Services
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        { _httpClientFactory = httpClientFactory; }

        public async Task ProcessUrl(string path)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            try
            {
                path.Clean();
                Radix radix = new(path);

                HttpResponseMessage response = await httpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }
            }
            catch
            { throw; }
        }
    }
}
