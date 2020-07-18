using EComerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Inerfaces
{
  public   interface ICustomerService
    {
        Task<(bool IsSuccess,IEnumerable<Customer>customers,string ErrorMessage)>GetCustomerAsync();
    }
}
