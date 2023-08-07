using Diploma_Thesis.Models;
using Diploma_Thesis.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma_Thesis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientsService _clientService;
        public ClientController(IClientsService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailed(Guid id)
        {
            var client = await _clientService.GetClientDetailedAsync(id);
            return Ok(client);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ClientDetailedModel model)
        {
            int count = await _clientService.AddClientAsync(model);
            if (count < 1)
                return BadRequest();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClientDetailedModel model)
        {
            int count = await _clientService.UpdateAsync(model);
            if (count < 1)
                return BadRequest();
            return Ok();
        }
    }
}
