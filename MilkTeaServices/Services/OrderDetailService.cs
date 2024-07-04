using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepo _OrderDetailRepo;
        public OrderDetailService(IOrderDetailRepo OrderDetailRepo)
        {
            _OrderDetailRepo = OrderDetailRepo;
        }
        public List<OrderDetail> getList(int oid) => _OrderDetailRepo.getList(oid);
        public OrderDetail get(int id) => _OrderDetailRepo.get(id);
        public void delete(int id) => _OrderDetailRepo.delete(id);
        public void update(OrderDetail order) => _OrderDetailRepo.update(order);
        public void add(OrderDetail order) => _OrderDetailRepo.add(order);
    }
}
