using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }
 [HttpGet ]
 public async Task<IActionResult> GetCustomersAsync()
        {
            var result =await customerProvider.GetCustomersAsync();
            if (result.IsScuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if (result.IsScuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }

    }
}
