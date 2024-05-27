using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.Repo
{
    public class CommentRepo :ICommentRepo
    {

        CommentDAO dao = new CommentDAO();

        public void AddNewComment(Comment comment) => dao.AddNewComment(comment);

        public User CheckLogin(string email, string password) => dao.CheckLogin(email, password);

        public List<Comment> GetAllComment() => dao.GetAllComment();

        public Comment GetCommentByyID(int id) => dao.GetCommentByID(id);

        public void UpdateComment(Comment comment) => dao.UdateCoomment(comment);
    }
}
