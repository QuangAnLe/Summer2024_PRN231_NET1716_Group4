using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaServices.IServices;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Controllers.CommentController
{
    [ApiController]
    [Route("odata/[controller]")]
    public class CommentController : ODataController
    {
        private readonly ICommentServices _commentServices;
        private readonly IMapper _mapper;

        public CommentController(ICommentServices commentServices, IMapper mapper)
        {
            _commentServices = commentServices;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<CommentVM>> GetAll()
        {
            var comments = _commentServices.getList();
            var commentViewModels = _mapper.Map<IEnumerable<CommentVM>>(comments);
            return Ok(commentViewModels);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var comment = _commentServices.get(id);
                if (comment != null)
                {
                    var response = _mapper.Map<CommentVM>(comment);
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
        public IActionResult Create([FromBody] CommentCreateDTO comment)
        {
            try
            {
                var _comment = _mapper.Map<Comment>(comment);
                _commentServices.add(_comment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public IActionResult Update([FromRoute] int id, [FromBody] CommentUpdateDTO comment)
        {
            try
            {
                if (comment.CommentID != id)
                {
                    return BadRequest("ID in the route does not match the ID in the request body.");
                }

                var _comment = _mapper.Map<Comment>(comment);
                _commentServices.update(_comment);
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
                var comment = _commentServices.get(id);
                if (comment == null)
                {
                    return NotFound();
                }
                _commentServices.delete(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
