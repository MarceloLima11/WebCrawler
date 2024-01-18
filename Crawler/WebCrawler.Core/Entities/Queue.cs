namespace WebCrawler.Core.Entities
{
    public class Queue
    {
        public HashSet<string> Links { get; private set; } = [];
        HashSet<string> crawled = [];

        public string Top()
        {
            return Links.First();
        }

        public bool HasBeenCrawled(string link)
        { 
            return crawled.Contains(link);
        }

        public void Post(string link)
        {
            if (Links.Contains(link))
                return;

            Links.Add(link);
        }

        public void Remove(string link)
        {
            crawled.Add(link);
            Links.Remove(link);
        }
    }
}
