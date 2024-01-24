using iText.Kernel.Exceptions;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using WebCrawler.Application.Interfaces;

namespace WebCrawler.Application.Services
{
    public class PDFGeneratorService : IDocumentGenerator
    {
        public PDFGeneratorService()
        {}

        public MemoryStream GenerateDocument(List<string> links)
        {
            try
            {
                using (MemoryStream stream = new())
                {
                    using (PdfWriter writer = new(stream))
                    {
                        using (PdfDocument pdf = new(writer))
                        {
                            Document document = new(pdf);

                            document.Add(new Paragraph("Links").SetFont(PdfFontFactory.CreateFont()).SetFontSize(16f));
                            for (int index = 0; index < links.Count; index++)
                            {
                                document.Add(new Paragraph($"{index + 1}. {links[index]}"));
                            }
                        }
                    }

                    MemoryStream copyStream = new MemoryStream(stream.ToArray());
                    return copyStream;
                }
            }
            catch (PdfException pdfEx)
            {
                Console.WriteLine($"PdfException Message: {pdfEx.Message}");
                Console.WriteLine($"PdfException Stack Trace: {pdfEx.StackTrace}");
                throw;
            }
        }
    }
}
