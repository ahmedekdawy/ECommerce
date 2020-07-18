using AutoMapper;
using ECommerce.Api.Customers.db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Provides
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbcontext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbcontext,ILogger<CustomerProvider> logger ,IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbcontext.Customers.Any())
            {
                dbcontext.Customers.Add(new db.Customer { Id = 1, Name = "Ahmed", Address = "cairo" });
                dbcontext.Customers.Add(new db.Customer { Id = 2, Name = "Esraa", Address = "Sharkia" });
                dbcontext.Customers.Add(new db.Customer { Id = 3, Name = "Mohammed", Address = "Giza" });
                dbcontext.Customers.Add(new db.Customer { Id = 4, Name = "Seleem", Address = "Assuet" });
                dbcontext.SaveChanges();
            }
        }

        public async Task<(bool IsScuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {

          
            var customers =await  dbcontext.Customers.ToListAsync();
            if(customers!=null && customers.Any())
            {
                var result = mapper.Map<IEnumerable<db.Customer>, IEnumerable<Models.Customer>>(customers).ToList();
                return (true, result, null);
            }
            return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message );
            }
        }

        public async Task<(bool IsScuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {


                var customer = await dbcontext.Customers.FirstOrDefaultAsync(w=>w.Id==id);
                if (customer != null)
                {
                    var result = mapper.Map<db.Customer,Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
