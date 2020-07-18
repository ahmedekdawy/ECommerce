using ECommerce.Api.Products.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
 public   interface IProductsProvider
    {
         Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductsAsync(int id);

    }
}
