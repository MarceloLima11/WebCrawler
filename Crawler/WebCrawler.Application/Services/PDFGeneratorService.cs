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

        public MemoryStream GenerateDocument(HashSet<string> links, List<string> errors)
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

                            var linksList = links.ToList();
                            document.Add(new Paragraph("Links").SetFont(PdfFontFactory.CreateFont()).SetFontSize(18f));
                            for (int index = 0; index < linksList.Count; index++)
                            {
                                document.Add(new Paragraph($"{index + 1}. {linksList[index]}"));
                            }

                            document.Add(new Paragraph("Invalid Links").SetFont(PdfFontFactory.CreateFont()).SetFontSize(18f));
                            for (int index = 0; index < errors.Count; index++)
                            {
                                document.Add(new Paragraph($"{index + 1}. {errors[index]}"));
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
