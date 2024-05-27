using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaServices.Services;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.UserController
{
    public class UserController : ODataController
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<User>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<UserVM>>(_userServices.GetAllUser()));
        }

        [HttpGet("odata/User/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var user = _userServices.GetUserByID(id);
                if (user != null)
                {
                    var responese = _mapper.Map<UserVM>(user);

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
        public IActionResult PostUser([FromBody] UserCreateDTO user)
        {
            try
            {
                var _user = _mapper.Map<User>(user);
                _userServices.AddNewUser(_user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/User/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] UserUpdateDTO user, [FromRoute] int id)
        {
            try
            {
                if (user.UserID != id)
                {
                    return NotFound();
                }
                var _user = _mapper.Map<User>(user);
                _userServices.UpdateUser(_user);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/User/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_userServices.GetAllUser() == null)
            {
                return NotFound();
            }
            var user = _userServices.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }

            _userServices.ChangeStatusUser(user);


            return Ok("Delete Successfully");
        }

    }
}
