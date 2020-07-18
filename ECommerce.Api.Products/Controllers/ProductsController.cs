using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
  
    public class ProductsController : ControllerBase
    {
        public IProductsProvider ProductsProvider { get; }
        public ProductsController(IProductsProvider productsProvider)
        {
            ProductsProvider = productsProvider;
        }

      

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result= await ProductsProvider.GetProductsAsync();
            if(result.IsSuccess)
            { return Ok(result.products); }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsAsync(int id)
        {
            var result = await ProductsProvider.GetProductsAsync(id);
            if (result.IsSuccess)
            { return Ok(result.product); }
            return NotFound();
        }
    }
}
