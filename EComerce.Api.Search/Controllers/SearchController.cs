using EComerce.Api.Search.Inerfaces;
using EComerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
       
        [HttpPost]
       public async  Task<IActionResult>SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if(result.IsSuccess)
            {
                return Ok(result.searchResult);

            }
            return NotFound();
        }
    }
}
