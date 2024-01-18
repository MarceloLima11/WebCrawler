using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Net;
using WebCrawler.Application.Extensions;
using WebCrawler.Core.Entities;

namespace WebCrawler.Application.Services
{
    public class CrawlService
    {
        private HtmlWeb _htmlWeb;
        private Queue _queue;
        private Details _details;

        public CrawlService() => _queue = new Queue();

        public async Task ProcessUrl(string pathRequest)
        {


            string path = pathRequest.Clean();
            Radix radix = new(path);

            try
            {
                _htmlWeb = new HtmlWeb();

                var document = await _htmlWeb.LoadFromWebAsync(radix.Path);

                var links = document.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        string childUrl = link.GetAttributeValue("href", "");
                        if (childUrl.IsSafeUrl() && !_queue.HasBeenCrawled(childUrl))
                            _queue.Post(childUrl);
                    }

                    _queue.Remove(radix.Path);
                    if (_queue.Links.Any())
                        await ProcessUrl(_queue.Top());
                }
            }
            catch (WebException err)
            { Debug.WriteLine($"Error to processing URL {radix.Path}: {err.Message}"); }
        }
    }
}
