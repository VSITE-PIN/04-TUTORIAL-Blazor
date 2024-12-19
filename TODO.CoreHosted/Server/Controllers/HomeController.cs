using Microsoft.AspNetCore.Mvc;

namespace TODO.CoreHosted.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
