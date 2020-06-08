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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id) {
            return await _orderService.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(OrderCreateDTO orderCreate) {
            return await _orderService.Create(orderCreate);
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<OrderDTO>>> GetAll(int page, int take = 20)
        {
            return await _orderService.GetAll(page, take);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id) {
            await _orderService.Remove(id);
            return NoContent();
        }
    }
}
