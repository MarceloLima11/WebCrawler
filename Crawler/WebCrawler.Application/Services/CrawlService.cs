using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;
using Utils;
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
            string path = Url.RemoveUrlBar(pathRequest.Clean());
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
                        string childUrl = link.GetAttributeValue("href", "").Clean();
                        if (Url.IsSafeUrl(childUrl))
                        {
                            if (!Url.IsValidUrl(childUrl))
                            {
                                _stopwatch.Stop();
                                _details.Errors.Add(childUrl);
                                _queue.Remove(childUrl);
                                continue;
                            }

                            _queue.Post(Url.RemoveUrlBar(childUrl));
                            _details.CrawledLinks.Add(childUrl);
                            _details.LinksFound++;
                        }
                    }

                    _queue.Remove(radix.Path);
                    _stopwatch.Stop();
                    if (_queue.Links.Count != 0)
                        await ProcessUrl(_queue.Top());
                }
            }
            catch (Exception err)
            {
                _details.Errors.Add($"Critical error processing URL {radix.Path}: {err.Message}");
                _details.CrawlingSucceded = false;
                _stopwatch.Stop();
                _details.AddUpDuration(_stopwatch.Elapsed);
                return _details;
            }

            _details.CrawlingSucceded = true;
            _stopwatch.Stop();
            _details.AddUpDuration(_stopwatch.Elapsed);
            return _details;
        }
    }
}
