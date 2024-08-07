﻿using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _OrderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            _OrderRepo = orderRepo;
        }
        public List<Order> getList() => _OrderRepo.getList();
        public List<Order> GetAllOrdersByUserID(int userID) => _OrderRepo.GetAllOrdersByUserID(userID);
        public Order get(int id) => _OrderRepo.get(id);
        public void delete(int id) => _OrderRepo.delete(id);
        public void update(Order order) => _OrderRepo.update(order);
        public Order add(Order order) => _OrderRepo.add(order);
        public void UpdatePaymentSuccess(int id) => _OrderRepo.UpdatePaymentSuccess(id);
    }
}
