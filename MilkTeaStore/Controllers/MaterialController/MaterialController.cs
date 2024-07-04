using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.MaterialController
{
    public class MaterialController : ODataController
    {
        private readonly IMaterialServices _materialServices;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialServices materialServices, IMapper mapper)
        {
            _materialServices = materialServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<Material>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<MaterialVM>>(_materialServices.GetAllMaterial()));
        }

        [HttpGet("odata/Material/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var material = _materialServices.GetMaterialByID(id);
                if (material != null)
                {
                    var responese = _mapper.Map<MaterialVM>(material);

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
        public IActionResult PostMaterial([FromBody] MaterialCreateDTO material)
        {
            try
            {
                var _material = _mapper.Map<Material>(material);
                _materialServices.AddNewMaterial(_material);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/Material/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] MaterialUpdateDTO material, [FromRoute] int id)
        {
            try
            {
                if (material.MaterialID != id)
                {
                    return NotFound();
                }
                var _material = _mapper.Map<Material>(material);
                _materialServices.UpdateMaterial(_material);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/Material/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_materialServices.GetAllMaterial() == null)
            {
                return NotFound();
            }
            var material = _materialServices.GetMaterialByID(id);
            if (material == null)
            {
                return NotFound();
            }

            _materialServices.ChangeStatusMaterial(material);


            return Ok("Delete Successfully");
        }

    }
}
