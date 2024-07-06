using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface IOrderRepo
    {
        public List<Order> getList();
        public List<Order> GetAllOrdersByUserID(int userID);
        public Order get(int id);
        public void delete(int id);
        public void update(Order order);
        public Order add(Order order);
        public void UpdatePaymentSuccess(int id);
    }
}
