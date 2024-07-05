using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.DTO;
using MilkTeaStore.ViewModels;
using System;
using System.Collections.Generic;

namespace MilkTeaStore.Controllers.TaskUserController
{
    [ApiController]
    [Route("odata/[controller]")]
    public class TaskUserController : ODataController
    {
        private readonly ITaskUserServices _taskUserServices;
        private readonly IOrderService _orderServices;
        private readonly IMapper _mapper;

        public TaskUserController(ITaskUserServices taskUserServices, IOrderService orderServices, IMapper mapper)
        {
            _taskUserServices = taskUserServices;
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IEnumerable<TaskUserVM>> GetAll()
        {
            var taskUsers = _taskUserServices.getList();
            var taskUserViewModels = _mapper.Map<IEnumerable<TaskUserVM>>(taskUsers);
            return Ok(taskUserViewModels);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var taskUser = _taskUserServices.get(id);
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
        public IActionResult Create([FromBody] TaskUserCreateDTO taskUser)
        {
            try
            {
                var _taskUser = _mapper.Map<TaskUser>(taskUser);
                _taskUserServices.add(_taskUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public IActionResult Update([FromRoute] int id, [FromBody] TaskUserUpdateDTO taskUser)
        {
            try
            {
                if (taskUser.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                var _taskUser = _mapper.Map<TaskUser>(taskUser);
                _taskUserServices.update(_taskUser);
                return Ok("Update Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var taskUser = _taskUserServices.get(id);
                if (taskUser == null)
                {
                    return NotFound();
                }
                _taskUserServices.delete(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [EnableQuery]
        public IActionResult UpdateStatus([FromRoute] int id, [FromBody] TaskUserUpdateStatusDTO taskUserUpdateStatusDTO)
        {
            try
            {
                if (taskUserUpdateStatusDTO.TaskId != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                var taskUser = _taskUserServices.get(id);
                if (taskUser == null)
                {
                    return NotFound();
                }

                // Update TaskUser status
                taskUser.Status = taskUserUpdateStatusDTO.Status;
                _taskUserServices.update(taskUser);

                // Check if there is an associated Order and update its status
                if (taskUser.OrderID > 0)
                {
                    var order = _orderServices.get(taskUser.OrderID); // Assuming _orderServices provides access to Order CRUD operations
                    if (order != null)
                    {
                        order.Status = taskUserUpdateStatusDTO.Status; // Update Order status based on TaskUser status
                        _orderServices.update(order);
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
