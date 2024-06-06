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
    public class CommentRepo : ICommentRepo
    {
        CommentDAO dao = new CommentDAO();
        public void add(Comment comment)
        {
            dao.Add(comment);
        }

        public void delete(int id)
        {
            dao.Delete(id);
        }

        public Comment get(int id)
        {
            return dao.Get(id);
        }

        public List<Comment> getList()
        {
            return dao.GetList();
        }

        public void update(Comment comment)
        {
            dao.Update(comment);
        }
    }
}

