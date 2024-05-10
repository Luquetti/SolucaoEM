using EM.Domain.Utilidades.Validacoes;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EM.Domain.Utilidades
{



    public class Relatorio
    { 
        public static byte[] GerarPDF(List<Aluno> alunos, string? nameCidade , int? Sexo,bool linhaAlternada)
        {

            using MemoryStream m = new();


            Document document = new(PageSize.A4, 25, 25, 25, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, m);


            PdfEvents events = new();
            writer.PageEvent = events;


            string backgroundPath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\fundo2.png";

            document.Open();
            //TENATDNO COLOCAR LINHA ZEBRADA:
            BaseColor colorEven = new BaseColor(233, 233, 233); // Cor clara para linhas pares
            BaseColor colorOdd = new BaseColor(255, 255, 255);

            // Title table, set this up with reduced or negative padding
            PdfPTable layoutTable = new(1)
            {
                WidthPercentage = 100  // Full width
            };

            Font titleFont = FontFactory.GetFont("Arial", 24, Font.BOLD);
            Paragraph title = new("Relatório de Alunos", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };

            PdfPCell titleCell = new PdfPCell(new Phrase(title))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP,
                PaddingTop = -40,  // Negative padding to move upwards, adjust as needed
                PaddingBottom = 0
            };
            layoutTable.AddCell(titleCell);

            // Add the title table to the document; it should appear over the logo
            document.Add(layoutTable);



			document.Add(new Paragraph());
            Chunk linebreak = new(new iTextSharp.text.pdf.draw.LineSeparator(1f, 112f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
            document.Add(linebreak);

			if (!string.IsNullOrEmpty(nameCidade) || Sexo.HasValue)
            {
                Font filterFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
                document.Add(new Paragraph($"Filtros utilizados:"));
                _ = new Paragraph($"Filtros utilizados:");
                if (!string.IsNullOrEmpty(nameCidade))
                {
                    Paragraph filterCidade = new($"• Cidade: {nameCidade}")
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    document.Add(filterCidade);
                }
                if (Sexo.HasValue)
                {
                    Paragraph filterSexo = new($"• Sexo: {(Sexo.Value == 0 ? "Masculino" : "Feminino")}", filterFont)
                    {
                        Alignment = Element.ALIGN_LEFT
                    };
                    document.Add(filterSexo);
                }

				document.Add(new Paragraph("\n"));
			}

            

            PdfPTable table = new([5, 10, 4, 7, 6, 8, 2])
            {
                WidthPercentage = 103
            };


            table.DefaultCell.FixedHeight = 30;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;


            PdfPCell CreateHeaderCell(string text) => new(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)))
            {
                BackgroundColor = new BaseColor(0, 100, 0)
            };


            table.AddCell(CreateHeaderCell("Matrícula"));
            table.AddCell(CreateHeaderCell("Nome"));
            table.AddCell(CreateHeaderCell("Sexo"));
            table.AddCell(CreateHeaderCell("CPF"));
            table.AddCell(CreateHeaderCell("Idade"));
            table.AddCell(CreateHeaderCell("Cidade"));
            table.AddCell(CreateHeaderCell("UF"));
            table.HeaderRows = 1;







           
            //pROXIMA LINHA CONTEM MUDANÇA PARA ZEBRAR 
            int rowIndex = 0;
            foreach (Aluno aluno in alunos)
			{
				if (linhaAlternada)
				{
					// Aplica cores alternadas se linhaAlternada for true
					BaseColor rowColor = (rowIndex % 2 == 0) ? colorEven : colorOdd;
					table.AddCell(new PdfPCell(new Phrase(aluno.Matricula.ToString(), FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.Nome, FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.Sexo.ToString(), FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.CPF, FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.Nascimento.ToString(), FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.Cidade.NomeCidade, FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
					table.AddCell(new PdfPCell(new Phrase(aluno.Cidade.UF, FontFactory.GetFont("Arial", 12))) { BackgroundColor = rowColor });
				} else

				{
					// Não aplica cores, relatório normal
					table.AddCell(new PdfPCell(new Phrase(aluno.Matricula.ToString(), FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.Nome, FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.Sexo.ToString(), FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.CPF, FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.Nascimento.ToString(), FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.Cidade.NomeCidade, FontFactory.GetFont("Arial", 12))));
					table.AddCell(new PdfPCell(new Phrase(aluno.Cidade.UF, FontFactory.GetFont("Arial", 12))));
				}
				rowIndex++; // Incrementa o índice da linha para alternar cores
			}

            table.SpacingAfter = 100;
			document.Add(table);
			document.Close();

			return m.ToArray();
		}

		private static string CalcularIdade(DateTime? dataNascimento)
        {
            if (!dataNascimento.HasValue)
            {
                return "0";
            }

            DateTime hoje = DateTime.Now;
            int anos = hoje.Year - dataNascimento.Value.Year;
            int meses = hoje.Month - dataNascimento.Value.Month;
            int dias = hoje.Day - dataNascimento.Value.Day;

            if (dias < 0)
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
