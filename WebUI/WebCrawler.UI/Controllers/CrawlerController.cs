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
            var test = new DetailsViewModel
            {
                Errors = [],
                Duration = TimeSpan.MinValue,
                LinksFound = 20,
                CrawlingId = Guid.NewGuid(),
                Succeded = false,
            };

            return View("Result", test);
        }

        [HttpGet]
        public async Task<IActionResult> ProcessUrl(RadixViewModel radix)
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
                    Succeded = details.CrawlingSucceded,
                });
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
        }
    }
}
