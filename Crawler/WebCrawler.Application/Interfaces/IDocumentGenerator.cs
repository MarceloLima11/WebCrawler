namespace WebCrawler.Application.Interfaces
{
    public interface IDocumentGenerator
    {
        MemoryStream GenerateDocument(List<string> content);
    }
}
