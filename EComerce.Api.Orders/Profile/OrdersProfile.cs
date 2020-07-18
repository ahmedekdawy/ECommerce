using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EComerce.Api.Orders.Profile
{
    public class OrdersProfile:AutoMapper.Profile
    {
        public OrdersProfile()
        {
            CreateMap<db.Order, Models.Order>();
            CreateMap<db.OrderItem, Models.OrderItem>();
        }
    }
}
