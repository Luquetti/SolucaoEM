using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class CidadeController : Controller
	{
		private readonly RepositorioCidade _repositorioCidade = new RepositorioCidade();
		public IActionResult EditarCidade() 
        {

        return View();
       
        }

		
        public IActionResult CadastrarCidade(int? id )
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
				return RedirectToAction("TabelaCidade", "Cidade");
			}
			return View("CadastrarCidade", cidade);//view que ira ocorrer a açao,parametro passado

		}

		public IActionResult TabelaCidade(Cidade cidade)
        {
            IEnumerable<Cidade> listacidade = _repositorioCidade.GetAll();
            return View(listacidade);
        }

    }
}
