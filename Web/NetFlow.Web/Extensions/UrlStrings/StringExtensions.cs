namespace NetFlow.Web.Extensions.UrlStrings
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
        {
            return Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();
        }

        public static string TitleToUpper(this string title)
        {           
            return title.First().ToString().ToUpper() + title.Substring(1);          
        }
    }
}
