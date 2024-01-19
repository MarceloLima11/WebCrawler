namespace WebCrawler.UI.Models
{
    public class DetailsViewModel
    {
        public Guid CrawlingId { get; set; }
        public bool CrawlingSucceded { get; set; }
        public int LinksFound { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string>? Errors { get; set; }
    }
}
