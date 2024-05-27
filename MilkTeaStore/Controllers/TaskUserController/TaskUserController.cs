using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.TaskUserController
{
    public class TaskUserController :ODataController
    {
        private readonly ITaskUserServices _taskUserServices;
        private readonly IMapper _mapper;

        public TaskUserController(ITaskUserServices taskUserServices, IMapper mapper)
        {
            _taskUserServices = taskUserServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<TaskUser>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<TaskUserVM>>(_taskUserServices.GetAllTaskUser()));
        }

        [HttpGet("odata/TaskUser/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var task = _taskUserServices.GetTaskUserByID(id);
                if (task != null)
                {
                    var responese = _mapper.Map<TaskUserVM>(task);

                    return Ok(responese);
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
        public IActionResult PostTask([FromBody] TaskUserCreateDTO task)
        {
            try
            {
                var _task = _mapper.Map<TaskUser>(task);
                _taskUserServices.AddNewTaskUser(_task);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/TaskUser/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] TaskUserUpdateDTO task, [FromRoute] int id)
        {
            try
            {
                if (task.TaskId != id)
                {
                    return NotFound();
                }
                var _task = _mapper.Map<TaskUser>(task);
               _taskUserServices.UpdateTaskUser(_task);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/TaskUser/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_taskUserServices.GetAllTaskUser() == null)
            {
                return NotFound();
            }
            var task = _taskUserServices.GetTaskUserByID(id);
            if (task == null)
            {
                return NotFound();
            }

            _taskUserServices.ChangeStatusTaskUser(task);


            return Ok("Delete Successfully");
        }


    }
}
