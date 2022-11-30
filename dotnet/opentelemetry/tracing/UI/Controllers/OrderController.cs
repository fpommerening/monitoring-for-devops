using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP.Monitoring.Trace.UI.Business;
using FP.Monitoring.Trace.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.Monitoring.Trace.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> Create([FromQuery] Guid productId)
        {
            var products = await _orderRepository.GetProducts();
            var product = products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return NotFound(productId);
            }

            var vm = new OrderViewModel
            {
                ProductName = product.Name,
                ProductId = product.Id
            };

            return View(vm);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(OrderViewModel vm)
        {

            await _orderRepository.OrderProduct(vm.ProductId, vm.Quantity, vm.Customer, vm.CardType, vm.CardNumber);
            return RedirectToAction("Index", "Product");
        }
    }
}
