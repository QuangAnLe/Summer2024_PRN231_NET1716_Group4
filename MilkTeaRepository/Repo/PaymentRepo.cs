using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class PaymentRepo : IPaymentRepo
    {
        PaymentDAO dao = new PaymentDAO();
        public async Task<Dictionary<string, object>> CreateOrder(int id)
        {
            return await dao.CreateOrder(id);
        }
    }
}
