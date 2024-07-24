using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkTeaStore.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class TaskUserController : ODataController
    {
        private readonly ITaskUserServices _taskUserServices;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public TaskUserController(ITaskUserServices taskUserServices, IOrderService orderService, IMapper mapper)
        {
            _taskUserServices = taskUserServices;
            _orderService = orderService;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskUserVM>>> GetAll()
        {
            try
            {
                var taskUsers = await _taskUserServices.GetList();
                var taskUserViewModels = _mapper.Map<IEnumerable<TaskUserVM>>(taskUsers);
                return Ok(taskUserViewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            try
            {
                var taskUser = await _taskUserServices.Get(id);
                if (taskUser != null)
                {
                    var response = _mapper.Map<TaskUserVM>(taskUser);
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Create([FromBody] TaskUserCreateDTO taskUser)
        {
            try
            {
                var _taskUser = _mapper.Map<TaskUser>(taskUser);
                await _taskUserServices.Add(_taskUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TaskUserUpdateDTO taskUser)
        {
            try
            {
                if (taskUser.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                var _taskUser = _mapper.Map<TaskUser>(taskUser);
                await _taskUserServices.Update(_taskUser);
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var taskUser = await _taskUserServices.Get(id);
                if (taskUser == null)
                {
                    return NotFound();
                }
                await _taskUserServices.Delete(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/updatestatus")]
        [EnableQuery]
        public async Task<IActionResult> UpdateTaskStatus([FromRoute] int id, [FromBody] TaskUpdateStatus taskUpdateStatus)
        {
            try
            {
                if (taskUpdateStatus.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                var taskUser = await _taskUserServices.Get(id);
                if (taskUser == null)
                {
                    return NotFound();
                }

                taskUser.Status = taskUpdateStatus.Status ?? false; // Assign false if status is null
                await _taskUserServices.Update(taskUser);

                if (taskUser.OrderID > 0)
                {
                    var order =  _orderService.get(taskUser.OrderID);
                    if (order != null)
                    {
                        order.Status = taskUpdateStatus.Status ?? false;
                        _orderService.update(order);
                    }
                    else
                    {
                        return NotFound("Associated Order not found.");
                    }
                }

                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/updatestatus/success")]
        [EnableQuery]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] TaskUserUpdateSuccessDTO taskUserUpdateSuccessDTO)
        {
            try
            {
                if (taskUserUpdateSuccessDTO.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                if (taskUserUpdateSuccessDTO.Status != true)
                {
                    return BadRequest("This endpoint only handles successful status updates.");
                }

                var taskUser = await _taskUserServices.Get(taskUserUpdateSuccessDTO.TaskId);
                if (taskUser == null)
                {
                    return NotFound();
                }

                // Update TaskUser status to true
                taskUser.Status = true;
                await _taskUserServices.Update(taskUser);

                // Check if there is an associated Order and update its status to true
                if (taskUser.OrderID > 0)
                {
                    var order = _orderService.get(taskUser.OrderID);
                    if (order != null)
                    {
                        order.Status = true;
                        _orderService.update(order);
                    }
                    else
                    {
                        return NotFound("Associated Order not found.");
                    }
                }

                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPatch("{id}/updatestatus/failure")]
        [EnableQuery]
        public async Task<IActionResult> UpdateFailureReason([FromRoute] int id, [FromBody] TaskUserUpdateReasonDTO taskUserUpdateStatusDTO)
        {
            try
            {
                if (taskUserUpdateStatusDTO.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                if (taskUserUpdateStatusDTO.Status == false && string.IsNullOrEmpty(taskUserUpdateStatusDTO.FailureReason))
                {
                    return BadRequest(new
                    {
                        errors = new
                        {
                            FailureReason = new[] { "The FailureReason field is required." }
                        },
                        type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        title = "One or more validation errors occurred.",
                        status = 400
                    });
                }

                var taskUser = await _taskUserServices.Get(taskUserUpdateStatusDTO.TaskId);
                if (taskUser == null)
                {
                    return NotFound();
                }

                taskUser.Status = taskUserUpdateStatusDTO.Status ?? false;
                if (taskUserUpdateStatusDTO.Status.HasValue && !taskUserUpdateStatusDTO.Status.Value)
                {
                    taskUser.WorkDescription = taskUserUpdateStatusDTO.FailureReason;
                }
                else
                {
                    taskUser.WorkDescription = null;
                }
                await _taskUserServices.Update(taskUser);

                if (taskUser.OrderID > 0)
                {
                    var order = _orderService.get(taskUser.OrderID);
                    if (order != null)
                    {
                        order.Status = taskUserUpdateStatusDTO.Status ?? false; // Handle nullable bool
                        _orderService.update(order); // Ensure this method is asynchronous
                    }
                    else
                    {
                        return NotFound("Associated Order not found.");
                    }
                }

                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
