using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepo _repo;
        public PaymentServices(IPaymentRepo repo)
        {
            _repo = repo;
        }
        public async Task<Dictionary<string, object>> CreateOrder(int id)
        {
            return await _repo.CreateOrder(id);
        }
    }
}
