using EComerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Interfices
{
   public interface IProductService
    {
        Task<(bool IsSuccess,IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
