using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface ICommentServices
    {
        User CheckLogin(string email, string password);
        List<Comment> GetAllComment();
        void AddNewComment(Comment comment);
        Comment GetCommentByID(int id);
        void UpdateComment(Comment comment);
    }
}
