using System;

namespace WebCrawler.Core.Entities
{
    public sealed class Radix
    {
        public string Path { get; }

        public Radix(string path)
        {
            this.Path = IsValidUrl(path) ? path: throw new ArgumentException("URL is invalid or not an HTTP/HTTPS URL.");
        }

        static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var resultUri)
                && (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
