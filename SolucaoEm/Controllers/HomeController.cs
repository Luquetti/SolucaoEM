using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SolucaoEm.Models;

namespace SolucaoEm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()//pagina principal
        {
            return RedirectToAction("TabelaAluno","Aluno");

        }

    }
}
