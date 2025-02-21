using Microsoft.AspNetCore.Mvc;

namespace Navio_Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(string email, string senha)
        {
            // Credenciais fixas
            string emailFixo = "Gui@gmail";
            string senhaFixa = "g";

            if (email == emailFixo && senha == senhaFixa)
            {
                return RedirectToAction("Index", "Dashboard");//Aqui me manda para a tela de dashboard
            }
            else
            {
                ViewData["Mensagem"] = "Email ou senha incorretos!";
                return View("Index");
            }
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
