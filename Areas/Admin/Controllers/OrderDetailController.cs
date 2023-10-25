using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
