using AutoMapper;
using ECommerce.Api.Products.db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext,ILogger<ProductsProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new db.Product() {ID=1,Name="Keyboard",Price=20,Inventory=100 });
                dbContext.Products.Add(new db.Product() { ID = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                dbContext.Products.Add(new db.Product() { ID = 3, Name = "Monitor", Price = 150, Inventory = 300 });
                dbContext.Products.Add(new db.Product() { ID = 4, Name = "CPU", Price = 200, Inventory = 400 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {

            try
            {
                var products =await dbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                  var result=  mapper.Map<IEnumerable<db.Product>, IEnumerable<Models.Product>>(products).ToList();
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductsAsync(int id)
        {

            try
            {
                var product = await dbContext.Products.Where(w=>w.ID==id).FirstOrDefaultAsync();
                if (product != null)
                {
                    var result = mapper.Map<db.Product, Models.Product>(product);
                    return (true, result, null);
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
