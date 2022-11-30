using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP.Monitoring.Trace.Common.Models;
using FP.Monitoring.Trace.StockService.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FP.Monitoring.Trace.StockService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _repository;
        
        public ProductController(ILogger<ProductController> logger, ProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            var products = await _repository.GetProducts();
            return products.ToList();
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> OrderProduct(Guid productId, [FromBody] int quantity)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            try
            {
                await _repository.UpdateProducts(productId, quantity);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return Accepted();
        }
    }
}
