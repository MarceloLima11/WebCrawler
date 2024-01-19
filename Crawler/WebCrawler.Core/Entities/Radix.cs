using System;

namespace WebCrawler.Core.Entities
{
    public sealed class Radix(string path)
    {
        public string Path { get; private set; } = path;
    }
}
