using EComerce.Api.Search.Interfices;
using EComerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory,ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order>orders, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersServices");
                var response = await client.GetAsync($"api/orders/{CustomerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content =await  response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true  };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, option);
                    if(result!=null && result.Any())
                    {
                        return (true, result, null);
                    }
                    return (false, null, "Not Found");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
