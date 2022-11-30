using System.Threading.Tasks;
using FP.Monitoring.Trace.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FP.Monitoring.Trace.PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly CardProvider _provider;

        public PaymentController(CardProvider provider)
        {
            _provider = provider;
        }

        [HttpPut]
        public async Task<IActionResult> Execute([FromBody]Payment payment)
        {
            if (string.IsNullOrEmpty(payment.Name))
            {
                return BadRequest("Missing Name");
            }
            await _provider.Validate(payment.Type, payment.Number);
            return Accepted();
        }

    }
}
