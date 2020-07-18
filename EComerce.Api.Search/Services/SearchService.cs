using EComerce.Api.Search.Inerfaces;
using EComerce.Api.Search.Interfices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService,ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic searchResult)> SearchAsync(int CustomerId)
        {
            var Orderresult = await orderService.GetOrdersAsync(CustomerId);
            var productResult = await productService.GetProductsAsync();
            var CustomerResult = await customerService.GetCustomerAsync();
            if (Orderresult.IsSuccess)
            {
                foreach (var order in Orderresult.orders)
                {
                    order.CustomerName = CustomerResult.IsSuccess ? CustomerResult.customers.FirstOrDefault(w => w.Id == CustomerId)?.Name : "Customer info not available";
                    foreach (var item in order.Items )
                    {
                        item.ProductName = productResult.IsSuccess? productResult.Products.FirstOrDefault(w => w.ID == item.ProductId)?.Name:"Product Info not available";
                    }
                }
                var result = new
                {
                    Orders = Orderresult.orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
