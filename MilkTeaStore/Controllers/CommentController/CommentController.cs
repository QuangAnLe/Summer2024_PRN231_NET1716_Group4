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
    public class CommentController :ODataController
    {
        private readonly ICommentServices _commentServices;
        private readonly IMapper _mapper;

        public CommentController(ICommentServices commentServices, IMapper mapper)
        {
            _commentServices = commentServices;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<Comment>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<CommentVM>>(_commentServices.GetAllComment()));
        }

        [HttpGet("odata/Comment/{id}")]
        [EnableQuery]
        public IActionResult GetByID([FromRoute] int id)
        {
            try
            {
                var comment = _commentServices.GetCommentByID(id);
                if (comment != null)
                {
                    var responese = _mapper.Map<CommentVM>(comment);

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
        public IActionResult PostComment([FromBody] CommentCreateDTO comment)
        {
            try
            {
                var _comment = _mapper.Map<Comment>(comment);
                _commentServices.AddNewComment(_comment);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("odata/Comment/{id}")]
        [EnableQuery]
        public IActionResult Put([FromBody] CommentUpdateDTO comment, [FromRoute] int id)
        {
            try
            {
                if (comment.CommentID != id)
                {
                    return NotFound();
                }
                var _comment = _mapper.Map<Comment>(comment);
                _commentServices.UpdateComment(_comment);

                return Ok("Update Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("odata/Comment/{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            if (_commentServices.GetAllComment() == null)
            {
                return NotFound();
            }
            var comment = _commentServices.GetCommentByID(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok("Delete Successfully");
        }


    }
}
