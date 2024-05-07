using System.Runtime.Serialization.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EM.Domain;
using System.IO;
using System.Text.Json;

namespace EM.Domain.Utilidades
{
	public class Relatorio
    {
        public byte[] GerarPDF(List<Aluno> alunos)
        {

            using (MemoryStream m = new MemoryStream())
            {


                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
               PdfWriter writer= PdfWriter.GetInstance(document, m);
                document.Open();
               
                string backgroundPath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\fundo2.png";
                PdfGState gs = new PdfGState();
                gs.FillOpacity = 0.3f;
                writer.DirectContentUnder.SetGState(gs);
                Image backgroundImage = Image.GetInstance(backgroundPath);
                backgroundImage.SetAbsolutePosition(0, 0);
                backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

                // Adiciona a imagem de fundo
                PdfContentByte canvas = writer.DirectContentUnder;
                canvas.AddImage(backgroundImage);

                PdfPTable layoutTable = new PdfPTable(new float[] { 3, 6 });
                layoutTable.WidthPercentage = 100;
                // Logo
                string logopath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\unnamed.png";
                Image logo= Image.GetInstance(logopath);
                logo.ScaleToFit(100, 100);
                PdfPCell logoCell=new PdfPCell(logo);
                logoCell.Border=Rectangle.NO_BORDER;
                layoutTable.AddCell(logoCell);
                 
                //Titulo
                Font titleFont= FontFactory.GetFont("Arial",24,Font.BOLD);
                Paragraph title= new Paragraph("Relatorio de Alunos",titleFont);
                title.Alignment = Element.ALIGN_LEFT;
                PdfPCell titleCell=new PdfPCell();
                titleCell.AddElement(title);
                titleCell.Border=Rectangle.NO_BORDER;
                layoutTable.AddCell(titleCell);

                document.Add(layoutTable);
                document.Add(new Paragraph());
                Chunk linebreak = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 112f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));

                document.Add(linebreak);
                // PARÂMETROS DA TABELA
                PdfPTable table = new PdfPTable(new float[] { 7, 10, 4, 5, 5, 6, 2 });
                table.WidthPercentage = 110;

                //cabeçalho da tabela
                PdfPCell cell = new PdfPCell(new Phrase("Matrícula"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.HorizontalAlignment= Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nome"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sexo"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("CPF"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Idade"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Cidade"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("UF"));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);






                foreach (Aluno aluno in alunos)
                {
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(aluno.Matricula?.ToString() ?? "");
                    
                    table.AddCell(aluno.Nome ?? "");                    
                    table.AddCell(aluno.Sexo.ToString()); // Convertendo enum para string
                    table.AddCell(aluno.CPF);
					table.AddCell(CalcularIdade(aluno.Nascimento).ToString() + " anos");
					table.AddCell(aluno.Cidade.NomeCidade);
					table.AddCell(aluno.Cidade.UF);
				}

                document.Add(table);
                document.Close();

				return m.ToArray();
			}
        }
       
        private string CalcularIdade(DateTime? dataNascimento)
        {
            if (!dataNascimento.HasValue)
            {
                return "0"; 
            }

            DateTime hoje = DateTime.Now;
            int anos=hoje.Year - dataNascimento.Value.Year;
            int meses= hoje.Month - dataNascimento.Value.Month;
            int dias = hoje.Day - dataNascimento.Value.Day;

            if(dias<0)
            {
                meses--;
                dias += DateTime.DaysInMonth(hoje.Year, hoje.Month);
            }
            if (meses < 0)
            {
                anos--;
                meses += 12;
            }

            return $"{anos}a {meses}m {dias}d";

        }
    }
}
