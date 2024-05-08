using System.Diagnostics;
using EM.Domain;
using EM.Domain.Interface;
using EM.Domain.Utilidades;
using Microsoft.AspNetCore.Mvc;
using SolucaoEm.Models;


namespace EM.Web.Controllers
{
    public class AlunoController(IRepositorioAluno<Aluno> repositorioAluno, IRepositorioCidade<Cidade> repositorioCidade, Relatorio geradorRelatorio) : Controller
    {
        private readonly IRepositorioAluno<Aluno> repositorioAluno = repositorioAluno;
        private readonly IRepositorioCidade<Cidade> repositorioCidade = repositorioCidade;
        private readonly Relatorio geradorRelatorio = geradorRelatorio;

        public IActionResult TabelaAluno()
        {
            IEnumerable<Aluno> listaAluno = repositorioAluno.GetAll();
            return View(listaAluno);
        }
        public IActionResult CadastrarAluno(int? matricula)
        {
            ViewBag.Cidades = repositorioCidade.GetAll().ToList();
            if (matricula != null)
            {
                var aluno = repositorioAluno.Get(a => a.Matricula == matricula).FirstOrDefault();
                if (aluno == null)
                {
                    return NotFound();
                }
                ViewBag.IsEdicao = true;
                return View(aluno);
            }
            ViewBag.IsEdicao = false;
            return View(new Aluno());
        }

        [HttpPost]//processa os dados enviados pelo usuario
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
               
                if (aluno.Matricula > 0)
                {
                    repositorioAluno.Update(aluno);
                }
                else
                {
                    repositorioAluno.Add(aluno);
                }
                return RedirectToAction("TabelaAluno");
            }

            ViewBag.IsEdicao = aluno.Matricula > 0;
            ViewBag.Cidades = repositorioCidade.GetAll().ToList();
            return View(aluno);


        }
        public IActionResult EditarAluno()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(Aluno aluno)
        {
            repositorioAluno.Remover(aluno);
            return RedirectToAction("TabelaAluno");
        }
        [HttpPost]
        public ActionResult Search(string searchTerm, string searchType)
        {
            Console.WriteLine("Search Type: " + searchType); // Adicione esta linha para depuração

            IEnumerable<Aluno> alunos;
            if (searchType == "matricula" && int.TryParse(searchTerm, out int matricula))
            {
                alunos = new List<Aluno>(repositorioAluno.GetByMatricula(matricula));
            }
            else if (searchType == "nome")
            {
                alunos = repositorioAluno.GetContendoNoNome(searchTerm);
            }
            else
            {
                alunos = [];
            }

            return View("TabelaAluno", alunos);


        }
            public IActionResult Error()
           {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

         [HttpPost]
           public IActionResult RelatorioAluno(int? sexo=null, int? ID_Cidade=null)
            
           {
			try
			{
				var alunos = repositorioAluno.GetAll().ToList();

				// Filtrar por sexo, se fornecido
				if (sexo.HasValue)
				{
					alunos = alunos.Where(a => (int)a.Sexo.Value == sexo.Value).ToList();
				}

				// Filtrar por cidade, se fornecido
				if (ID_Cidade.HasValue)
				{
					alunos = alunos.Where(a => a.Cidade.ID_Cidade == ID_Cidade).ToList();
				}

                // Ger// Gere o relatório e obtenha o caminho do arquivo PDF com os filtros de Id_cidade e Sexo.
                byte[] pdfPath = geradorRelatorio.GerarPDF(alunos,ID_Cidade,sexo);
                // Retorna o FileStream como um FileStreamResult.
                return File(pdfPath, "application/pdf", "RelatorioAlunos.pdf");
			}
			catch (Exception ex)
			{
				
				return BadRequest("Erro ao gerar relatório: " + ex.Message);
			}


		}

		public IActionResult RelatorioAluno()
            {
			List<Aluno> alunos = repositorioAluno.GetAll().ToList();

			// Gere o relatório e obtenha o caminho do arquivo PDF com os filtros de Id_cidade e Sexo.
			byte [] pdfPath = geradorRelatorio.GerarPDF(alunos,null,null);



            // Retorne o FileStream como um FileStreamResult.
            return File(pdfPath, "application/pdf", "RelatorioAlunos.pdf");
        }

		public IActionResult RelatorioDeAluno()
        {
			ViewBag.Cidades = repositorioCidade.GetAll().ToList();
			return View();  
        }


	}
    
}
