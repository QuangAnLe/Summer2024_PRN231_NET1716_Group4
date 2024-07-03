using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaServices.Services;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.DetailsMaterialController
{

    public class DetailsMaterialController : ODataController
    {
        private readonly IDetailsMaterialServices _detailServices;
        private readonly IMapper _mapper;

        public DetailsMaterialController(IDetailsMaterialServices detailServices, IMapper mapper)
        {
            _detailServices = detailServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<DetailsMaterial>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<DetailsMaterialVM>>(_detailServices.GetAllDetailsMaterial()));
        }

        [HttpGet("odata/DetailsMaterial/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var detail = _detailServices.GetDetailsMaterialByID(id);
                if (detail != null)
                {
                    var responese = _mapper.Map<DetailsMaterialVM>(detail);

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
        public IActionResult PostDetailsMaterial([FromBody] DetailsMaterialCreateDTO detail)
        {
            try
            {
                var _detail = _mapper.Map<DetailsMaterial>(detail);
                _detailServices.AddNewDetailsMaterial(_detail);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/DetailsMaterial/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            try
            {
                _detailServices.DeleteDetailsMaterialById(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("odata/DetailsMaterial/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] DetailsMaterialUpdateDTO detail, [FromRoute] int id)
        {
            try
            {
               
                var _detail = _mapper.Map<DetailsMaterial>(detail);
                _detail.DetailsMaterialID = id;
                _detailServices.UpdateDetailsMaterial(_detail);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
