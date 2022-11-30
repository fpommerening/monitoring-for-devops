using Microsoft.AspNetCore.Mvc;

namespace FP.Monitoring.Trace.PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Trace-Paymentservice");
        }
    }
}
