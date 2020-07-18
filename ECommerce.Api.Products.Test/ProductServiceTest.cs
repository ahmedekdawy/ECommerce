using AutoMapper;
using ECommerce.Api.Products.db;
using ECommerce.Api.Products.Profile;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Frameworks;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace ECommerce.Api.Products.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public async  Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof (GetProductsReturnAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var producProfile = new ProduuctProfile();
            var configurations = new MapperConfiguration(cfg=>cfg.AddProfile (producProfile));
            var mapper = new Mapper(configurations);
            var ProductsProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await ProductsProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product { ID = i, Name = "Product" + i, Price = i * 10 ,Inventory=i*3});
            }
            dbContext.SaveChanges();
            
        }
    }
}
