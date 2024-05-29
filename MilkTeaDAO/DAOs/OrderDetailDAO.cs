using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilkTeaDAO.DAOs
{
    public class OrderDetailDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;

        public OrderDetailDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<OrderDetail> GetAllOrderDetails(int oid)
        {
            try
            {
                return _context.OrderDetails.Include(od => od.Order)
                                            .Include(od => od.Tea).Where(o=>o.OrderID == oid)
                                            .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderDetail GetOrderDetailByID(int id)
        {
            try
            {
                var orderDetail = _context.OrderDetails.Include(od => od.Order)
                                                       .Include(od => od.Tea)
                                                       .SingleOrDefault(od => od.OrderDetailID == id);
                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                orderDetail.OrderDetailID = 0;
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var existingOrderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderDetailID == orderDetail.OrderDetailID);
                if (existingOrderDetail != null)
                {
                    _context.Entry(existingOrderDetail).CurrentValues.SetValues(orderDetail);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order detail not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(int id)
        {
            try
            {
                var orderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderDetailID == id);
                if (orderDetail != null)
                {
                    _context.OrderDetails.Remove(orderDetail);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order detail not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
