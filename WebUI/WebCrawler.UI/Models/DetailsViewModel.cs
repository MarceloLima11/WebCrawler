namespace WebCrawler.UI.Models
{
    public class DetailsViewModel
    {
        public Guid CrawlingId { get; set; }
        public bool Succeded { get; set; }
        public int LinksFound { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string>? Errors { get; set; }
    }
}
