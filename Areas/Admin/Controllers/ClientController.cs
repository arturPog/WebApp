using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
