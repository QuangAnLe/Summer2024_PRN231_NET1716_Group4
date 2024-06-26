using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.IRepo
{
    public interface ICommentRepo
    {
        public List<Comment> getList();
        public Comment get(int id);
        public void delete(int id);
        public void update(Comment comment);
        public void add(Comment comment);
    }
}
