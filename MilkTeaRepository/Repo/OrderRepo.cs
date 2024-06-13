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
    public class OrderRepo : IOrderRepo
    {

        OrderDAO dao = new OrderDAO();
        public void add(Order order)
        {
            dao.Add(order);
        }

        public void delete(int id)
        {
            dao.Delete(id);
        }

        public Order get(int id)
        {
           return dao.Get(id);  
        }

        public List<Order> getList()
        {
            return dao.GetList();
        }

        public void update(Order order)
        {
          dao.Update(order);
        }
    }
}
