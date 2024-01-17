using HtmlAgilityPack;
using System;
using System.Diagnostics;
using WebCrawler.Application.Extensions;
using WebCrawler.Core.Entities;

namespace WebCrawler.Application.Services
{
    public class CrawlService
    {
        private HtmlWeb _htmlWeb;
        private Queue queue;

        public CrawlService()
        { 
            _htmlWeb = new HtmlWeb();
            queue = new Queue();
        }

        public async Task ProcessUrl(string path)
        {
            path.Clean();
            Radix radix = new(path);

            try
            {
                var document = await _htmlWeb.LoadFromWebAsync(radix.Path);

                var links = document.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        string childUrl = link.GetAttributeValue("href", "");
                        if (childUrl.IsSafeUrl())
                            queue.Post(childUrl);
                    }

                    queue.Remove(radix.Path);
                    if(queue.Links.Count > 0)
                        await ProcessUrl(queue.Top());
                }
            }
            catch 
            { throw; }

            Debug.Write("STOP HERE -- CHECK LIST");
        }
    }
}
