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
        private readonly IDetailsMaterialService _detailsMaterialService;
        private readonly IMapper _mapper;

        public DetailsMaterialController(IDetailsMaterialService detailsMaterialService, IMapper mapper)
        {
            _detailsMaterialService = detailsMaterialService;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<DetailsMaterial>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<DetailsMaterialVM>>(_detailsMaterialService.GetAllDetailsMaterial()));
        }

       
        [HttpGet("odata/DetailsMaterial/{teaID}")]
        [EnableQuery]
        public ActionResult<IQueryable<DetailsMaterial>> GetByTeaID([FromRoute] int teaID)
        {
            return Ok(_mapper.Map<IEnumerable<DetailsMaterialVM>>(_detailsMaterialService.GetDetailsMaterialByTeaID(teaID)));
        }

        [HttpGet("odata/DetailsMaterial/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var tea = _detailsMaterialService.GetDetailsMaterialByID(id);
                if (tea != null)
                {
                    var responese = _mapper.Map<DetailsMaterialVM>(tea);

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
        public IActionResult PostDetailsMaterial([FromBody] DetailsMaterialCreateDTO detailsMaterial)
        {
            try
            {
                var _detailsMaterial = _mapper.Map<DetailsMaterial>(detailsMaterial);
                _detailsMaterialService.AddNewDetailDetailsMaterial(_detailsMaterial);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/DetailsMaterial/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] DetailsMaterialUpdateDTO detailsMaterial, [FromRoute] int id)
        {
            try
            {
                if (detailsMaterial.DetailsMaterialID != id)
                {
                    return NotFound();
                }
                var _detailsMaterial = _mapper.Map<DetailsMaterial>(detailsMaterial);
                _detailsMaterialService.UpdateDetailsMaterial(_detailsMaterial);

                return Ok("Update Successfully");

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
            if (_detailsMaterialService.GetAllDetailsMaterial() == null)
            {
                return NotFound();
            }
            var detailsMaterial = _detailsMaterialService.GetDetailsMaterialByID(id);
            if (detailsMaterial == null)
            {
                return NotFound();
            }

            _detailsMaterialService.DeleteDetailsMaterial(id);


            return Ok("Delete Successfully");
        }
    }
}
