using Microsoft.AspNetCore.Mvc;
using WebCrawler.UI.Models;

namespace WebCrawler.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error(ErrorViewModel error)
        {
            return View("Error", error);
        }
    }
}
