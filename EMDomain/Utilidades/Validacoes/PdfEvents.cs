using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EM.Domain.Utilidades.Validacoes
{
    public class PdfEvents : PdfPageEventHelper
    {
		private readonly string backgroundImagePath = ".\\wwwroot\\Imagens\\fundo2.png";
		private readonly string logopath = ".\\wwwroot\\Imagens\\unnamed.png";

        // Construtor para inicializar os caminhos das imagens


        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            PdfPTable header = new(1)
            {
                TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin,
                LockedWidth = true
            };

            Image logo = Image.GetInstance(logopath);
            logo.ScaleToFit(200, 100);  // Make sure the height here is controlled

            PdfPCell logoCell = new(logo)
            {
                Border = Rectangle.NO_BORDER,

                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingTop = -20,  // Reduce or remove padding to minimize space
                PaddingBottom = -20,  // Reduce or remove padding to minimize space
            };
            header.AddCell(logoCell);

            // Optionally, reduce the height of this cell to force content upwards
            PdfPCell emptyCell = new()
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
			Image backgroundImage = Image.GetInstance(backgroundImagePath);
			// Dimensiona a imagem para caber na página
			backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
			backgroundImage.ScalePercent(80);
			// Calcula a posição relativa para centralizar a imagem
			float xPosition = (document.PageSize.Width - backgroundImage.ScaledWidth) / 2;
			float yPosition = (document.PageSize.Height - backgroundImage.ScaledHeight) / 2 - 50;
			backgroundImage.SetAbsolutePosition(xPosition, yPosition);
			// Adiciona a imagem de fundo
			PdfContentByte canvas = writer.DirectContentUnder;
			canvas.AddImage(backgroundImage);

			document.Add(header);
        }
    }
}

