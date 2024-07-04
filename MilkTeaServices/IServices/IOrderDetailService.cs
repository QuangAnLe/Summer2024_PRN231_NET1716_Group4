using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> getList(int oid);
        public OrderDetail get(int id);
        public void delete(int id);
        public void update(OrderDetail order);
        public void add(OrderDetail order);
    }
}
