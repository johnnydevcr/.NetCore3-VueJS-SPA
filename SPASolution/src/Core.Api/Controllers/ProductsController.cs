using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.DTOs;
using Service;

namespace Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO model)
        {
            await _productService.Create(model);
            return Ok();
        }
    }
}
