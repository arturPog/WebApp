using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    public class OrterController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrterController(IStatusService statusService, IOrderService orderService, IOrderDetailService orderDetailService) 
        {
            _statusService = statusService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = _orderService.GetAllOrderWithDetailsAsync();
            
            return View(orders);
        }
        public async Task<IActionResult> Create()
        {
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit()
        {

            return RedirectToAction("Index");
        }
    }
}
