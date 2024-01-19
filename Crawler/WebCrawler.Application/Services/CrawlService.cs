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
        private Stopwatch _stopwatch;

        public CrawlService()
        {
            _queue = new Queue();
            _details = new Details();
            _stopwatch = new Stopwatch();
        }

        public async Task<Details> ProcessUrl(string pathRequest)
        {
            string path = pathRequest.Clean();
            Radix radix = new(path);

            try
            {
                _stopwatch.Start();

                _htmlWeb = new HtmlWeb();

                var document = await _htmlWeb.LoadFromWebAsync(radix.Path);

                var links = document.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        string childUrl = link.GetAttributeValue("href", "");
                        if (childUrl.IsSafeUrl() && !_queue.HasBeenCrawled(childUrl))
                        {
                            _queue.Post(childUrl);
                            _details.LinksFound++;
                        }
                    }

                    _queue.Remove(radix.Path);
                    _stopwatch.Stop();
                    _details.Duration.Add(_stopwatch.Elapsed);
                    if (_queue.Links.Count != 0)
                        await ProcessUrl(_queue.Top());
                }
            }
            catch (WebException err)
            {
                _details.Errors.Add($"Error processing URL {radix.Path}: {err.Message}");
                _details.CrawlingSucceded = false;
                return _details;
            }

            _details.CrawlingSucceded = true;
            return _details;
        }
    }
}
