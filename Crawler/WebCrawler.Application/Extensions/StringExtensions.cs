namespace WebCrawler.Application.Extensions
{
    internal static class StringExtensions
    {
        public static string Clean(this string str)
        {
            if (!str.EndsWith('/'))
                str += '/';

            return str.Trim().ToLower();
        }

        public static bool IsSafeUrl(this string url) => !string.IsNullOrWhiteSpace(url) && !url.StartsWith('#') && !url.StartsWith("mailto:") && !url.StartsWith("tel:");
    }
}
