using Microsoft.AspNetCore.Mvc;

namespace ControlDePagos.Controllers
{
    public class Login : Controller
    {
        public IActionResult LoginApp()
        {
            return View();
        }
    }
}
