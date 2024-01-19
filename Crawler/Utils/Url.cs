using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Url
    {
        public static string RemoveUrlBar(string str)
        {
            if (str.EndsWith('/'))
                return str.Remove(str.Length - 1, 1);

            return str;
        }

        public static bool IsSafeUrl(string url) => !string.IsNullOrWhiteSpace(url) && !url.StartsWith('#') && !url.StartsWith("mailto:") && !url.StartsWith("tel:");

        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var resultUri)
                && (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
