using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepo _commentRepo;
        public CommentServices(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }
        public List<Comment> getList() => _commentRepo.getList();
        public Comment get(int id) => _commentRepo.get(id);
        public void delete(int id) => _commentRepo.delete(id);
        public void update(Comment comment) => _commentRepo.update(comment);
        public void add(Comment comment) => _commentRepo.add(comment);
    }
}
