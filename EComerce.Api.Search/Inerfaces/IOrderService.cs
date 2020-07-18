using EComerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Interfices
{
   public interface IOrderService
    {
        Task<(bool IsSuccess,IEnumerable<Order> orders, string ErrorMessage)>GetOrdersAsync(int CustomerId);
    }
}
