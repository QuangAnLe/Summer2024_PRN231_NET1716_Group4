using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface IOrderService
    {
        public List<Order> getList();
        public Order get(int id);
        public void delete(int id);
        public void update(Order order);
        public Order add(Order order);
    }
}
