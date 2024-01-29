using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebCrawler.Application.Interfaces;
using WebCrawler.Application.Services;
using WebCrawler.UI.Models;

namespace WebCrawler.UI.Controllers
{
    public class CrawlerController : Controller
    {
        private readonly CrawlService _httpClientService;
        private readonly IDocumentGenerator _documentGenerator;

        public CrawlerController(CrawlService httpClientService
            , IDocumentGenerator documentGenerator)
        { 
            _httpClientService = httpClientService 
                ?? throw new ArgumentNullException(nameof(CrawlService));
            _documentGenerator = documentGenerator;
        }

        public IActionResult Index()
        {
            return View();
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
                    Links = details.CrawledLinks,
                    CrawlingId = details.CrawlingId,
                    Succeded = details.CrawlingSucceded,
                });
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
        }

        [HttpPost]
        public IActionResult GenerateDocument(HashSet<string> links)
        {
            try
            {
                var stream = _documentGenerator.GenerateDocument(links);

                stream.Seek(0, SeekOrigin.Begin);
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "document.pdf"
                };

            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
    }
}
