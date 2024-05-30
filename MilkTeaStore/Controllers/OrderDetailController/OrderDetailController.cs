using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.OrderDetailController
{
    public class OrderDetailController : ODataController
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }
        [HttpGet("odata/OrderDetailByOid/{oid}")]
        [EnableQuery]
        public ActionResult<IQueryable<OrderDetailDTO>> Get([FromRoute] int oid)
        {
            try
            {
                var orders = _orderDetailService.getList(oid);
                var orderDTOs = _mapper.Map<IEnumerable<OrderDetailVM>>(orders);
                return Ok(orderDTOs.AsQueryable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("odata/OrderDetail/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var order = _orderDetailService.get(id);
                if (order != null)
                {
                    var orderDTO = _mapper.Map<OrderDetailVM>(order);
                    return Ok(orderDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [EnableQuery]
        [HttpPost("odata/OrderDetail")]
        public IActionResult PostOrder([FromBody] OrderDetailDTO orderDTO)
        {
            try
            {
                var order = _mapper.Map<OrderDetail>(orderDTO);
                _orderDetailService.add(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/OrderDetail/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] OrderDetailDTO orderDTO, [FromRoute] int id)
        {
            try
            {
                if (orderDTO.OrderID != id)
                {
                    return NotFound();
                }
                var order = _mapper.Map<OrderDetail>(orderDTO);
                _orderDetailService.update(order);
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/OrderDetail/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _orderDetailService.get(id);
                if (order == null)
                {
                    return NotFound();
                }
                _orderDetailService.delete(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
