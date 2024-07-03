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
    public class OrderDetailRepo : IOrderDetailRepo
    {
        OrderDetailDAO dao = new OrderDetailDAO();
        public void add(OrderDetail OrderDetail)
        {
            dao.AddOrderDetail(OrderDetail);
        }

        public void delete(int id)
        {
            dao.DeleteOrderDetail(id);
        }

        public OrderDetail get(int id)
        {
            return dao.GetOrderDetailByID(id);
        }

        public List<OrderDetail> getList(int oid)
        {
            return dao.GetAllOrderDetails(oid);
        }

        public void update(OrderDetail OrderDetail)
        {
            dao.UpdateOrderDetail(OrderDetail);
        }
    }
}
