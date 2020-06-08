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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClientCreateDTO model)
        {
            var result = await _clientService.Create(model);
            return CreatedAtAction("Get",new { id = result.ClientId},result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,ClientUpdateDTO model)
        {
            await _clientService.Update(id,model);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _clientService.Remove(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> Get(int id) {
            return await _clientService.Get(id);
        }
        [HttpGet]
        public async Task<ActionResult<DataCollection<ClientDTO>>> GetAll(int page, int take = 20)
        {
            return await _clientService.GetAll(page, take);
        }
    }
}
