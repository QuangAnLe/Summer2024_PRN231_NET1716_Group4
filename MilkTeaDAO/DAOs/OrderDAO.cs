using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    throw new Exception(ex.Message);
                }
            }

            public Order Get(int id)
            {
                try
                {
                    var order = _context.Orders.Include(o => o.OrderDetails)
                                               .ThenInclude(od => od.Tea)
                                               .SingleOrDefault(o => o.OrderID == id);
                    return order;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public void Add(Order order)
            {
                try
                {
                order.OrderID = 0;
                    _context.Orders.Add(order);
                    _context.SaveChanges();
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
        }
}
