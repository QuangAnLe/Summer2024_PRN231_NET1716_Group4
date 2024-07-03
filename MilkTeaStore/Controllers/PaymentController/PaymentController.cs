using Microsoft.AspNetCore.Mvc;
using MilkTeaServices.IServices;

namespace MilkTeaStore.Controllers.PaymentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _service;
        public PaymentController(IPaymentServices service)
        {
            _service = service;
        }


        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            return Ok(await _service.CreateOrder());
        }


    }


}
