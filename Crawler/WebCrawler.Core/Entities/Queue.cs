namespace WebCrawler.Core.Entities
{
    public class Queue
    {
        public HashSet<string> Links { get; private set; } = [];

        public string Top()
        {
            return Links.First();
        }

        public void Post(string link)
        {
            if (Links.Contains(link))
                return;

            Links.Add(link);
        }

        public void Remove(string link)
        {
            Links.Remove(link);
        }
    }
}
