using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaServices.Services;
using MilkTeaStore.DTO.Create;

namespace MilkTeaStore.Controllers.OrderController
{

    public class OrderController : ODataController
    {
        private readonly IOrderService _orderService;
        private readonly ITaskUserServices _taskUserServices;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, ITaskUserServices taskUserServices, IUserServices userServices, IMapper mapper)
        {
            _orderService = orderService;
            _taskUserServices = taskUserServices;
            _userServices = userServices;
            _mapper = mapper;
        }
        [HttpGet("odata/Order")]
        [EnableQuery]
        public ActionResult<IQueryable<Order>> Get()
        {
            try
            {
                var orders = _orderService.getList().OrderByDescending(o => o.StartDate);
                return Ok(orders.AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("odata/OrderByUser/{id}")]
        [EnableQuery]
        public ActionResult<IQueryable<Order>> GetAllOrdersByUserID([FromRoute] int id)
        {
            try
            {
                var orders = _orderService.getList().Where(o => o.UserID == id).OrderByDescending(o => o.StartDate);
                return Ok(orders.AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("odata/Order/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var order = _orderService.get(id);
                if (order != null)
                {
                    var orderDTO = _mapper.Map<OrderDTO>(order);
                    return Ok(orderDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("odata/OrderWithShipper")]
        [EnableQuery]
        public IActionResult PostOrderWithShipper([FromBody] OrderWithShipperDTO orderWithShipperDTO)
        {
            try
            {
                // Create the order
                var order = _mapper.Map<Order>(orderWithShipperDTO.Order);
                order.Status = true;
                Order newOrder = _orderService.add(order);

                // Get the shipper
                var shipper = _userServices.GetUserByID(orderWithShipperDTO.ShipperID);
                if (shipper == null)
                {
                    return BadRequest("Invalid shipper ID");
                }

                // Create the TaskUser
                var taskUser = new TaskUser
                {
                    OrderID = newOrder.OrderID,
                    UserID = shipper.UserID,
                    WorkName = shipper.FullName,
                    WorkDescription = "Shipping",
                    Status = true,
                };

                _taskUserServices.Add(taskUser);

                return Ok(new { OrderID = newOrder.OrderID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/Order/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] OrderDTO orderDTO, [FromRoute] int id)
        {
            try
            {
                if (orderDTO.OrderID != id)
                {
                    return NotFound();
                }
                var order = _mapper.Map<Order>(orderDTO);
                _orderService.update(order);
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/Order/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _orderService.get(id);
                if (order == null)
                {
                    return NotFound();
                }
                _orderService.delete(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("odata/OrderSuccess/{id}")]
        [EnableQuery]
        public IActionResult UpdatePaymentSuccess([FromRoute] int id)
        {
            try
            {
               
                _orderService.UpdatePaymentSuccess(id);
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class OrderWithShipperDTO
    {
        public OrderDTO Order { get; set; }
        public int ShipperID { get; set; }
    }
}
