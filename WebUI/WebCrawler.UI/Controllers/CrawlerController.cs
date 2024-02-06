using Microsoft.AspNetCore.Mvc;
using System.Net;
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
                if (!Utils.Url.IsValidUrl(radix.Path))
                    return RedirectToAction("Error", "Error", new { statusCode = HttpStatusCode.BadRequest, message = "Invalid url!" });

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
            { return RedirectToAction("Error", "Error", new { statusCode = HttpStatusCode.BadRequest, message = ex.Message }); }
        }

        [HttpPost]
        public IActionResult GenerateDocument(HashSet<string> links, List<string> errors)
        {
            try
            {
                var stream = _documentGenerator.GenerateDocument(links, errors);

                stream.Seek(0, SeekOrigin.Begin);
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "document.pdf"
                };

            }
            catch (Exception ex)
            { return RedirectToAction("Error", "Error", new { statusCode = HttpStatusCode.BadRequest, message = ex.Message }); }
        }
    }
}
