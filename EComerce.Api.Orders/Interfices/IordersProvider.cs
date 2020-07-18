using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Orders.Interfices
{
    public interface IordersProvider
    {
        Task<(bool IsSuccess,IEnumerable<Models.Order> Orders,string ErrorMessage)>GetOrdersAsync(int customerId);
    }
}
