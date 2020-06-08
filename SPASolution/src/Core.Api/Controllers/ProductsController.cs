using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.DTOs;
using Service;
using Service.Commons;

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
        public async Task<ActionResult> Create(ProductCreateDTO model)
        {
            var result = await _productService.Create(model);
            return CreatedAtAction("Get", new {id =result.ProductId},result);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,ProductUpdateDTO model)
        {
            await _productService.Update(id,model);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _productService.Remove(id);
            return NoContent();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id) {
            return await _productService.Get(id);
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductDTO>>> GetAll(int page, int take)
        {
            return await _productService.GetAll(page,take);
        }
    }
}
