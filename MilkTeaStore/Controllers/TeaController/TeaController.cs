using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;
using System;

namespace MilkTeaStore.Controllers.TeaController
{
    public class TeaController : ODataController
    {

        private readonly ITeaServices _teaServices;
        private readonly IMapper _mapper;

        public TeaController(ITeaServices teaServices, IMapper mapper)
        {
            _teaServices = teaServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<Tea>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<TeaVM>>(_teaServices.GetAllTea()));
        }

        [HttpGet("odata/Tea/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var tea = _teaServices.GetTeaByID(id);
                if (tea != null)
                {
                    var responese = _mapper.Map<TeaVM>(tea);

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
        public IActionResult PostTea([FromBody] TeaCreateDTO tea)
        {
            try
            {
                var _tea = _mapper.Map<Tea>(tea);
                _teaServices.AddNewTea(_tea);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/Tea/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] TeaUpdateDTO tea, [FromRoute] int id)
        {
            try
            {
                if (tea.TeaID != id)
                {
                    return NotFound();
                }
                var _tea = _mapper.Map<Tea>(tea);
                _teaServices.UpdateTea(_tea);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/Tea/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_teaServices.GetAllTea() == null)
            {
                return NotFound();
            }
            var tea = _teaServices.GetTeaByID(id);
            if (tea == null)
            {
                return NotFound();
            }

            _teaServices.ChangeStatusTea(tea);


            return Ok("Delete Successfully");
        }


    }
}
