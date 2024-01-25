namespace WebCrawler.UI.Models
{
    public class DetailsViewModel
    {
        public Guid CrawlingId { get; set; }
        public bool Succeded { get; set; }
        public int LinksFound { get; set; }
        public HashSet<string> Links { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string>? Errors { get; set; }

        public string FormattedDuration => Duration.ToString(@"mm\:ss");
    }
}
