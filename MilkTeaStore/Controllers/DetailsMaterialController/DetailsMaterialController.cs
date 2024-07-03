using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.DetailsMaterialController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsMaterialController : ControllerBase
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

        [HttpDelete("{id}")]
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


    }
}
