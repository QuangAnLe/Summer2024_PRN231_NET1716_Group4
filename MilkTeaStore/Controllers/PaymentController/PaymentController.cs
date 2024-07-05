using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaDAO.ZaloPayHelper.Crypto;
using MilkTeaServices.IServices;
using Newtonsoft.Json;

namespace MilkTeaStore.Controllers.PaymentController
{
    public class PaymentController : ODataController
    {
        private readonly IPaymentServices _service;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentServices service, IOrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }


        [HttpPost("/odata/CreateOrder/{id}")]
        [EnableQuery]
        public async Task<IActionResult> CreateOrder([FromRoute] int id)
        {
            return Ok(await _service.CreateOrder(id));
        }


        private string key2 = "eG4r0GcoNtRGbO8";

        [HttpPost("/odata/Callback/{id}")]
        public IActionResult Post([FromBody] dynamic cbdata,[FromRoute] int id)
        {
            var result = new Dictionary<string, object>();

            try
            {
                var dataStr = Convert.ToString(cbdata["data"]);
                var reqMac = Convert.ToString(cbdata["mac"]);
                
                var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, dataStr);

                Console.WriteLine("mac = {0}", mac);

                // kiểm tra callback hợp lệ (đến từ ZaloPay server)
                
                    // thanh toán thành công
                    // merchant cập nhật trạng thái cho đơn hàng
                    var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    Console.WriteLine("update order's status = success where app_trans_id = {0}", dataJson["app_trans_id"]);
                    _orderService.UpdatePaymentSuccess(id);

                    result["return_code"] = 1;
                    result["return_message"] = "success";
                
            }
            catch (Exception ex)
            {
                result["return_code"] = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
                result["return_message"] = ex.Message;
            }

            // thông báo kết quả cho ZaloPay server
            return Ok(result);
        }

    }

}
