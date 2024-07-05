using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;

namespace MilkTeaStore.Controllers.OrderController
{

    public class OrderController : ODataController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpGet("odata/Order")]
        [EnableQuery]
        public ActionResult<IQueryable<Order>> Get()
        {
            try
            {
                var orders = _orderService.getList();
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
                var orders = _orderService.getList();
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
        [HttpPost("odata/Order")]
        [EnableQuery]
        public IActionResult PostOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                Order newo = _orderService.add(order);
                return Ok(newo);
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
    }
}
