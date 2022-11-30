using System.Threading.Tasks;
using FP.Monitoring.Trace.UI.Business;
using FP.Monitoring.Trace.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.Monitoring.Trace.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public ProductController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();
            var products = await _orderRepository.GetProducts();
            vm.Products.AddRange(products);
            return View(vm);
        }
    }
}
