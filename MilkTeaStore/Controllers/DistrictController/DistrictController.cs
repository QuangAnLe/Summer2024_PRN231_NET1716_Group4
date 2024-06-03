using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.DistrictController
{

    public class DistrictController : ODataController
    {
        private readonly IDistrictServices _districtServices;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictServices districtServices, IMapper mapper)
        {
            _districtServices = districtServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<District>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<DistrictVM>>(_districtServices.GetAllDistrict()));
        }

        [HttpGet("odata/District/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var district = _districtServices.GetDistrictByID(id);
                if (district != null)
                {
                    var responese = _mapper.Map<DistrictVM>(district);

                    return Ok(responese);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("odata/District")]
        [EnableQuery]
        public IActionResult PostDistrict([FromBody] DistrictCreateDTO district)
        {
            try
            {
                var _district = _mapper.Map<District>(district);
                _districtServices.AddNewDistrict(_district);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/District/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] DistrictUpdateDTO district, [FromRoute] int id)
        {
            try
            {
                if (district.DistrictID != id)
                {
                    return NotFound();
                }
                var _district = _mapper.Map<District>(district);
                _districtServices.UpdateDistrict(_district);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/District/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_districtServices.GetAllDistrict() == null)
            {
                return NotFound();
            }
            var district = _districtServices.GetDistrictByID(id);
            if (district == null)
            {
                return NotFound();
            }

            _districtServices.ChangeStatusDistrict(district);


            return Ok("Delete Successfully");
        }
    }
}
