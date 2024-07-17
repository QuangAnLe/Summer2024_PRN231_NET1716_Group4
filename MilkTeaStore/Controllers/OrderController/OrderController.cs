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
        public ActionResult<IQueryable<OrderWithTaskUserDTO>> Get()
        {
            try
            {
                var orders = _orderService.getList().OrderByDescending(o => o.StartDate);
                var ordersWithTaskUsers = orders.Select(o => new OrderWithTaskUserDTO
                {
                    Order = _mapper.Map<OrderDTO>(o),
                    TaskUser = _mapper.Map<TaskUserDTO>(_taskUserServices.GetByOrderID(o.OrderID))
                });
                return Ok(ordersWithTaskUsers.AsQueryable());
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
                    var taskUser = _taskUserServices.GetByOrderID(id);
                    var taskUserDTO = _mapper.Map<TaskUserDTO>(taskUser);
                    var orderWithTaskUser = new OrderWithTaskUserDTO
                    {
                        Order = orderDTO,
                        TaskUser = taskUserDTO
                    };
                    return Ok(orderWithTaskUser);
                }
                return NotFound();
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

       
        [HttpPost("odata/OrderWithShipper")]
        [EnableQuery]
        public IActionResult PostOrderWithShipper([FromBody] OrderWithShipperDTO orderWithShipperDTO)
        {
            try
            {
                // Create the order
                var order = _mapper.Map<Order>(orderWithShipperDTO.Order);
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

                _taskUserServices.add(taskUser);

                return Ok(new { OrderID = newOrder.OrderID });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpPut("odata/Order/{id}")]
		[EnableQuery]
		public IActionResult Put([FromBody] OrderWithTaskUserDTO orderWithTaskUserDTO, [FromRoute] int id)
		{
			try
			{
				if (orderWithTaskUserDTO.Order.OrderID != id)
				{
					return NotFound();
				}

				// Update the order
				var order = _mapper.Map<Order>(orderWithTaskUserDTO.Order);
				_orderService.update(order);

				// Update or create the TaskUser
				var existingTaskUser = _taskUserServices.GetByOrderID(id);
				if (existingTaskUser == null)
				{
					// Create new TaskUser
					var newTaskUser = _mapper.Map<TaskUser>(orderWithTaskUserDTO.TaskUser);
					_taskUserServices.add(newTaskUser);
				}
				else
				{
					var taskId = existingTaskUser.TaskId;
					_mapper.Map(orderWithTaskUserDTO.TaskUser, existingTaskUser);
					existingTaskUser.TaskId = taskId; // Gán lại TaskId sau khi map

					_taskUserServices.update(existingTaskUser);
				}

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

        [HttpPut("odata/Order/{id}/UpdateShipper")]
        [EnableQuery]
        public IActionResult UpdateShipper([FromRoute] int id, [FromQuery] int shipperId)
        {
            try
            {
                var order = _orderService.get(id);
                if (order == null)
                {
                    return NotFound("Order not found");
                }

                var shipper = _userServices.GetUserByID(shipperId);
                if (shipper == null)
                {
                    return BadRequest("Invalid shipper ID");
                }

                // Update or create the TaskUser entry
                var taskUser = _taskUserServices.GetByOrderID(id);
                if (taskUser == null)
                {
                    taskUser = new TaskUser
                    {
                        OrderID = id,
                        UserID = shipperId,
                        WorkName = shipper.FullName,
                        WorkDescription = "Shipping",
                        Status = true,
                    };
                    _taskUserServices.add(taskUser);
                }
                else
                {
                    taskUser.UserID = shipperId;
                    taskUser.WorkName = shipper.FullName;
                    _taskUserServices.update(taskUser);
                }

                return Ok("Shipper updated successfully");
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

    public class OrderWithTaskUserDTO
    {
        public OrderDTO Order { get; set; }
        public TaskUserDTO TaskUser { get; set; }
    }

    public class TaskUserDTO
    {
        public int TaskID { get; set; }
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public bool Status { get; set; }
    }
}
