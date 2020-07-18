using EComerce.Api.Orders.Interfices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController:ControllerBase
    {
        private readonly IordersProvider ordersProvider;

        public OrdersController(IordersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult>  GetOrders(int customerId)
        {
            var result =await  ordersProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
