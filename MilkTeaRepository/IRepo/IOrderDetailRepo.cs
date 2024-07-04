using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface IOrderDetailRepo
    {
        public List<OrderDetail> getList(int oid);
        public OrderDetail get(int id);
        public void delete(int id);
        public void update(OrderDetail order);
        public void add(OrderDetail order);
    }
}
