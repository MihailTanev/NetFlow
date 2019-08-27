namespace NetFlow.Web.Extensions.TruncateHelper
{
    using System;

    public static class TruncateExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            string suffix = "...";

            if (string.IsNullOrEmpty(value)) { return value; }

            return value.Substring(0, Math.Min(value.Length, maxLength)).TrimEnd(new char[0]) + suffix;
        }
    }
}
