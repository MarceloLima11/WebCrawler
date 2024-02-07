using System.Net;

namespace WebCrawler.UI.Models
{
    public class ErrorViewModel
    {
        public int? Code { get; set; }
        public string Message { get; set; }

        public ErrorViewModel()
        { }
        public ErrorViewModel(HttpStatusCode code, string message)
        {
            Code = (int)code;
            Message = message;
        }
    }
}
