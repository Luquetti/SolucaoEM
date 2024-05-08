using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EM.Domain.Utilidades
{
    public class Relatorio
    {
        public byte[] GerarPDF(List<Aluno> alunos,int? Id_Cidade,int?Sexo)
        {

            using (MemoryStream m = new())
            {


                Document document = new(PageSize.A4, 25, 25, 30, 30);
               PdfWriter writer= PdfWriter.GetInstance(document, m);
                document.Open();
               
                string backgroundPath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\fundo2.png";
                PdfGState gs = new()
                {
                    FillOpacity = 0.3f
                };
                writer.DirectContentUnder.SetGState(gs);
                Image backgroundImage = Image.GetInstance(backgroundPath);
                backgroundImage.SetAbsolutePosition(0, 0);
                backgroundImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

                // Adiciona a imagem de fundo
                PdfContentByte canvas = writer.DirectContentUnder;
                canvas.AddImage(backgroundImage);

                PdfPTable layoutTable = new([3, 6])
                {
                    WidthPercentage = 100
                };
                // Logo
                string logopath = "C:\\Work.Luquetti\\POO\\SolucaoEm\\SolucaoEm\\wwwroot\\Imagens\\unnamed.png";
                Image logo= Image.GetInstance(logopath);
                logo.ScaleToFit(100, 100);
                PdfPCell logoCell = new(logo)
                {
                    Border = Rectangle.NO_BORDER
                };
                layoutTable.AddCell(logoCell);
                 
                //Titulo
                Font titleFont= FontFactory.GetFont("Arial",24,Font.BOLD);
                Paragraph title = new("Relatório de Alunos", titleFont)
                {
                    Alignment = Element.ALIGN_LEFT
                };
                PdfPCell titleCell=new();
                titleCell.AddElement(title);
                titleCell.Border=Rectangle.NO_BORDER;
                layoutTable.AddCell(titleCell);

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
                // PARÂMETROS DA TABELA
                PdfPTable table = new([4, 10, 3, 7, 5, 8, 2])
                {
                    WidthPercentage = 100
                };

                //cabeçalho da tabela
                PdfPCell cell = new PdfPCell(new Phrase("Matrícula"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                cell.HorizontalAlignment= Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nome"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sexo"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("CPF"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Idade"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Cidade"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("UF"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                table.AddCell(cell);






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
        }
       
        private static string CalcularIdade(DateTime? dataNascimento)
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
