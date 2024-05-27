using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.Services
{
    public class CommentServices :ICommentServices
    {
        private readonly ICommentRepo _commentRepo;
        public CommentServices(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public void AddNewComment(Comment comment) => _commentRepo.AddNewComment(comment);

        public User CheckLogin(string email, string password) => _commentRepo.CheckLogin(email, password);

        public List<Comment> GetAllComment() => _commentRepo.GetAllComment();

        public Comment GetCommentByID(int id) => _commentRepo.GetCommentByID(id);

        public void UpdateComment(Comment comment) => _commentRepo.UpdateComment(comment);

    }
}
    }
}
