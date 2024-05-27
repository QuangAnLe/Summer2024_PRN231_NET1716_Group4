using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaDAO.DAOs
{
    public class CommentDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public CommentDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }
        public User CheckLogin(string email, string password)
        {
            return _context.Users.Where(u => u.Email!.Equals(email) && u.Password!.Equals(password)).FirstOrDefault();
        }

        public List<Comment> GetAllComment()
        {
            try
            {
                return _context.Comments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewComment(Comment comment)
        {
            try
            {
                _context.Add(comment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Comment GetCommentByID(int id)
        {
            try
            {
                var comment = _context.Comments!.SingleOrDefault(c => c.CommentID == id);
                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UdateCoomment(Comment comment)
        {
            try
            {
                var a = _context.Comments!.SingleOrDefault(c => c.CommentID == comment.CommentID);

                _context.Entry(a).CurrentValues.SetValues(comment);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}


