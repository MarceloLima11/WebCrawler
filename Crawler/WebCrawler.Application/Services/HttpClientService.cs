using System;

namespace WebCrawler.Application.Services
{
    public class HttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        { _httpClientFactory = httpClientFactory; }

        public async void Test(string path)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            try
            {
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
