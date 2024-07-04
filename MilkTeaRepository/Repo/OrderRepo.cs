using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class OrderRepo : IOrderRepo
    {

        OrderDAO dao = new OrderDAO();
        public Order add(Order order)
        {
            return dao.Add(order);
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
