using Microsoft.AspNetCore.Mvc;
using WebCrawler.UI.Models;

namespace WebCrawler.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error(int code, string message)
        {
            var errorViewModel = new ErrorViewModel { Code = 500, Message = message };
            return View("Error", errorViewModel);
        }
    }
}
