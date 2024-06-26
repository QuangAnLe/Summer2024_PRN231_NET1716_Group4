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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _OrderRepo;
        public OrderService(IOrderRepo OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public List<Order> getList() => _OrderRepo.getList();
        public Order get(int id) => _OrderRepo.get(id);
        public void delete(int id) => _OrderRepo.delete(id);
        public void update(Order order) => _OrderRepo.update(order);
        public Order add(Order order) => _OrderRepo.add(order);
    }
}
