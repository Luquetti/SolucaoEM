using EM.Domain.Utilidades.Validacoes;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EM.Domain.Utilidades
{



    public class Relatorio
    { 
        public static byte[] GerarPDF(List<Aluno> alunos, int? Id_Cidade, int? Sexo)
        {

            using MemoryStream m = new();

            Document document = new(PageSize.A4, 25, 25, 25, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, m);


            PdfEvents events = new();
            writer.PageEvent = events;


            string backgroundPath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\fundo2.png";

            document.Open();

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


            if (Id_Cidade.HasValue || Sexo.HasValue)
            {
                Font filterFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
                document.Add(new Paragraph($"Filtros utilizados:"));
                _ = new Paragraph($"Filtros utilizados:");
                if (Id_Cidade.HasValue)
                {
                    Paragraph filterCidade = new($"• Cidade: ID-{Id_Cidade}")
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
            }

            document.Add(linebreak);

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

            foreach (Aluno aluno in alunos)
            {

                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(aluno.Matricula?.ToString() ?? "");



                table.AddCell(aluno.Nome ?? "");
                string sexoAbreviado = (aluno.Sexo == Enum.Sexo.masculino ? "M" : "F");
                table.AddCell(sexoAbreviado.ToString());
                table.AddCell(aluno.CPF);
                table.AddCell(CalcularIdade(aluno.Nascimento).ToString());
                table.AddCell(aluno.Cidade.NomeCidade);
                table.AddCell(aluno.Cidade.UF);

            }

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
