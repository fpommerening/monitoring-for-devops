using Microsoft.AspNetCore.Mvc;

namespace FP.Monitoring.Trace.StockService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Trace-Stockservice");
        }
    }
}
