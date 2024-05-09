using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class CidadeController : Controller
	{
		private readonly RepositorioCidade _repositorioCidade = new();
		public IActionResult EditeCidade() 
        {

        return View();
       
        }

		
        public IActionResult CadastreCidade(int? id )
        {
			{
				if (id != null)
				{
					Cidade? cidade = _repositorioCidade.Get(c => c.ID_Cidade == id).FirstOrDefault();
					if (cidade == null)
					{
						return NotFound();
					}
					ViewBag.IsEdicao = true;
					return View(cidade);
				}
				ViewBag.IsEdicao = false;
				return View(new Cidade());
			}
		}
		[HttpPost]
		public IActionResult CadastreCidade(Cidade cidade)//esse é o metodo 
        {
			if (ModelState.IsValid)
			{
				if (cidade.ID_Cidade > 0)
				{
					_repositorioCidade.Update(cidade);
				}
				else
				{

				_repositorioCidade.Add(cidade);
				}
				return RedirectToAction("TabelaCidade", "Cidade");//sucesso no adicionar e ai ele vai para a pagina da tabela 
			}
			return View("CadastreCidade", cidade);//view que ira ocorrer a açao,parametro passado, caso tenha um erro, ele permanece na view de cadastrar

		}

		public IActionResult TabelaCidade(Cidade cidade)
        {
            IEnumerable<Cidade> listacidade = _repositorioCidade.GetAll();
            return View(listacidade);
        }

    }
}
