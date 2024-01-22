namespace WebCrawler.Application.Interfaces
{
    public interface IDocumentGenerator
    {
        byte[] GenerateDocument(List<string> content);
    }
}
