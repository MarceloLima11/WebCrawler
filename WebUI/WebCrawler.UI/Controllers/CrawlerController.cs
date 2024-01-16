using Microsoft.AspNetCore.Mvc;
using WebCrawler.Application.Services;
using WebCrawler.UI.Models;

namespace WebCrawler.UI.Controllers
{
    public class CrawlerController : Controller
    {
        private readonly HttpClientService _httpClientService;
        public CrawlerController(HttpClientService httpClientService)
        { 
            _httpClientService = httpClientService ?? 
                throw new ArgumentNullException(nameof(HttpClientService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TrackRadix(RadixViewModel radix)
        {
            try
            {
                await _httpClientService.ProcessUrl(radix.Path);

                return NotFound();
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
        }
    }
}
