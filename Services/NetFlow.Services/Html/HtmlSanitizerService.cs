namespace NetFlow.Services.HtmlSanitizer
{
    using Ganss.XSS;

    public class HtmlSanitizerService : IHtmlSanitizerService
    {
        private readonly HtmlSanitizer sanitizer;

        public HtmlSanitizerService()
        {
            this.sanitizer = new HtmlSanitizer();
            this.sanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string htmlContents)
        {
            return this.sanitizer.Sanitize(htmlContents);
        }
    }
}
