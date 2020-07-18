using EComerce.Api.Search.Inerfaces;
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
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;

        public CustomerService(IHttpClientFactory httpClientFactory,ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async  Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {

         
            var client = httpClientFactory.CreateClient("CustomersServices");
            var response = await  client.GetAsync($"api/customers");
            if (response.IsSuccessStatusCode)
            {
                var content =await  response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<Customer>>(content, options);
                return (true, result, null);
            }
            return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null,ex.Message );
            }
        }
    }
}
