﻿namespace WebCrawler.Core.Entities
{
    public sealed class Details
    {
        public Guid CrawlingId { get; private set; }
        public bool CrawlingSucceded { get; set; }
        public int LinksFound { get; set; }
        public TimeSpan Duration { get; private set; }
        public List<string> Errors { get; private set; }

        public Details() {
            CrawlingId = Guid.NewGuid();
        }


        public void PostError(string error)
        {
            if (String.IsNullOrEmpty(error))
                Errors.Add(error);
        }
    }
}