using AutoMapper;
using EComerce.Api.Orders.db;
using EComerce.Api.Orders.Interfices;
using EComerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Orders.Providers
{
    public class OrdersProvider : IordersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
           if(!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new db.Order { Id = 1,CustomerId=1, OrderDate = new DateTime(2020, 1, 1), Total = 1000 });
                dbContext.Orders.Add(new db.Order { Id =2, CustomerId = 2, OrderDate = new DateTime(2020, 2, 1), Total = 1500 });
                dbContext.Orders.Add(new db.Order { Id =3, CustomerId =3, OrderDate = new DateTime(2020, 3, 1), Total = 2000 });
                dbContext.OrderItems.Add(new db.OrderItem { Id = 1,OrderId=1, ProductId = 1 });
                dbContext.OrderItems.Add(new db.OrderItem { Id = 2, OrderId = 1, ProductId = 2});
                dbContext.OrderItems.Add(new db.OrderItem { Id =3, OrderId = 1, ProductId = 3 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {

                var orders = await dbContext.Orders.Where(w => w.CustomerId == customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}