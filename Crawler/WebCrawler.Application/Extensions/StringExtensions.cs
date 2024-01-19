namespace WebCrawler.Application.Extensions
{
    internal static class StringExtensions
    {
        public static string Clean(this string str)
        {
            return str.Trim().ToLower();
        }
    }
}
