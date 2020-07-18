using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Inerfaces
{
   public interface ISearchService
    {
       Task<(bool IsSuccess, dynamic searchResult)> SearchAsync(int CustomerId);
    }
}
