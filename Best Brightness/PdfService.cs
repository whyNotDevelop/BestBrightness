using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace BestBrightness
{
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService()
        {
            var context = new CustomAssemblyLoadContext();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "wkhtmltox.dll");
            context.LoadUnmanagedLibrary(path);

            _converter = new SynchronizedConverter(new PdfTools());
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                    }
                }
            };

            return _converter.Convert(doc);
        }
    }
}
