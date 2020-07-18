using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Profile
{
    public class ProduuctProfile:AutoMapper.Profile
    {
        public ProduuctProfile()
        {
            CreateMap<db.Product, Models.Product>();
        }
    }
}
