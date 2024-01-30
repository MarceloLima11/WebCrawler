namespace WebCrawler.Application.Interfaces
{
    public interface IDocumentGenerator
    {
        MemoryStream GenerateDocument(HashSet<string> links, List<string> errors);
    }
}
