using ControleDeContatosMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatosMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
