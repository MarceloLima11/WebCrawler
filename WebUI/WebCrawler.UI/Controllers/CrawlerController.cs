using Microsoft.AspNetCore.Mvc;
using WebCrawler.Application.Services;
using WebCrawler.UI.Models;

namespace WebCrawler.UI.Controllers
{
    public class CrawlerController : Controller
    {
        private readonly CrawlService _httpClientService;
        public CrawlerController(CrawlService httpClientService)
        { 
            _httpClientService = httpClientService ?? 
                throw new ArgumentNullException(nameof(CrawlService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessUrl(RadixViewModel radix)
        {
            try
            {
                var details = await _httpClientService.ProcessUrl(radix.Path);

                return View("Result", new DetailsViewModel
                {
                    Errors = details.Errors,
                    Duration = details.Duration,
                    LinksFound = details.LinksFound,
                    CrawlingId = details.CrawlingId,
                    CrawlingSucceded = details.CrawlingSucceded,
                });
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
        }
    }
}
