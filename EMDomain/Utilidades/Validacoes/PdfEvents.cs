using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EM.Domain.Utilidades.Validacoes
{
    public class PdfEvents : PdfPageEventHelper
    {
        private string backgroundImagePath;
        private  string logopath;

        // Construtor para inicializar os caminhos das imagens


        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            PdfPTable header = new PdfPTable(1);
            header.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            header.LockedWidth = true;

            Image logo = Image.GetInstance("C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\unnamed.png");
            logo.ScaleToFit(200, 100);  // Make sure the height here is controlled

            PdfPCell logoCell = new PdfPCell(logo)
            {
                Border = Rectangle.NO_BORDER,

                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingTop = -20,  // Reduce or remove padding to minimize space
                PaddingBottom = -20,  // Reduce or remove padding to minimize space
            };
            header.AddCell(logoCell);

            // Optionally, reduce the height of this cell to force content upwards
            PdfPCell emptyCell = new PdfPCell()
            {
                FixedHeight = 5,  // Minimal height to reduce space
                Border = Rectangle.NO_BORDER
            };
            header.AddCell(emptyCell);

            PdfGState gs = new()
            {
                FillOpacity = 0.3f
            };
            writer.DirectContentUnder.SetGState(gs);
            Image backgroundImage = Image.GetInstance("C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\fundo2.png");
            backgroundImage.SetAbsolutePosition(0, 120);
            backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

            // Adiciona a imagem de fundo
            PdfContentByte canvas = writer.DirectContentUnder;
            canvas.AddImage(backgroundImage);

            document.Add(header);
        }

        private PdfPCell CreateHeaderCell(string text)
        {
            return new PdfPCell(new Phrase(text))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
        }
    }
}

