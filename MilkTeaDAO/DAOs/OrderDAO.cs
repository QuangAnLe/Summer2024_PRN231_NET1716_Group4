using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class OrderDAO
    {

        private readonly MilkTeaDeliveryDBContext _context;

        public OrderDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<Order> GetList()
        {
            try
            {
                return _context.Orders.Include(o => o.OrderDetails)
                                      .ThenInclude(od => od.Tea)
                                      .ToList();
            }
            catch (Exception ex)
            {
                try
                {
                    return _context.Orders.Include(o => o.OrderDetails)
                                          .ThenInclude(od => od.Tea)
                                          .ToList();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

           
        }

        public Order Get(int id)
        {
            try
            {
                var order = _context.Orders.Include(o => o.OrderDetails)
                                           .ThenInclude(od => od.Tea)
                                           .Include(p => p.User)
                                           .SingleOrDefault(o => o.OrderID == id);
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order Add(Order order)
        {
            try
            {
                order.OrderID = 0;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return _context.Orders.Take(1).OrderByDescending(o => o.StartDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                var existingOrder = _context.Orders.SingleOrDefault(o => o.OrderID == order.OrderID);
                if (existingOrder != null)
                {
                    _context.Entry(existingOrder).CurrentValues.SetValues(order);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(o => o.OrderID == id);
                if (order != null)
                {
                    var orderDetails = _context.OrderDetails.Where(od => od.OrderID == id).ToList();
                    if (orderDetails.Any())
                    {
                        _context.OrderDetails.RemoveRange(orderDetails);
                    }

                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePaymentSuccess(int id)
        {
            try
            {
                var existingOrder = _context.Orders.SingleOrDefault(o => o.OrderID == id);
                if (existingOrder != null)
                {
                    existingOrder.Status = true;
                    _context.Update(existingOrder);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
