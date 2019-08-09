namespace NetFlow.Services.HtmlSanitizer
{
    public interface IHtmlSanitizerService
    {
        string Sanitize(string htmlContents);
    }
}
