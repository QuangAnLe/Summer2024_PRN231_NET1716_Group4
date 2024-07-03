using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class CommentDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;

        public CommentDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<Comment> GetList()
        {
            try
            {
                return _context.Comments.Include(c => c.Tea)
                    .Include(c => c.User)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Comment Get(int id)
        {
            try
            {
                var comment = _context.Comments.Include(c => c.Tea).Include(c => c.User)
                                           .SingleOrDefault(c => c.CommentID == id);
                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Comment comment)
        {
            try
            {
                comment.CommentID = 0;
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Comment comment)
        {
            try
            {
                var existingComment = _context.Comments.SingleOrDefault(c => c.CommentID == comment.CommentID);
                if (existingComment != null)
                {
                    _context.Entry(existingComment).CurrentValues.SetValues(comment);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Comment not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var comment = _context.Comments.SingleOrDefault(c => c.CommentID == id);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Comment not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
