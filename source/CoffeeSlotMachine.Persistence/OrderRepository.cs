using System;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSlotMachine.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> GetAllWithProduct()
        {
            return _dbContext.Orders.Include(order => order.Product).ToArray();
        }

        public Order AddOrder(Product product)
        {
            Order order = new Order();
            order.Product = product;
            _dbContext.Orders.Add(order);
            return order;
        }
    }
}
