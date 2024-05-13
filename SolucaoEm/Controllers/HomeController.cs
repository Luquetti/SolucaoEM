using Microsoft.AspNetCore.Mvc;

namespace SolucaoEm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public override bool Equals(object? obj)
        {
            return obj is HomeController controller &&
                   EqualityComparer<ILogger<HomeController>>.Default.Equals(_logger, controller._logger);
        }

        public IActionResult Index()//pagina principal
        {
            return RedirectToAction("TabelaAluno","Aluno");

        }

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}
}
