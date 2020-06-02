using System.Collections.Generic;
using System.Threading.Tasks;

using BankApplication.Data.DTOs;
using BankApplication.Service.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace BankApplication.WebApi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientsRepository _service;

        public ClientController(IClientsRepository service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<ClientDTO> GetClients()
        {
            return _service.GetClients();
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<ClientDTO> GetClient([FromRoute]int id)
        {
            return await _service.GetClient(id);
        }

        [HttpPost]
        [Route("NewClient")]
        public IActionResult NewClient([FromBody]ClientDTO client)
        {
            if (ModelState.IsValid)
            {
                var response = _service.SaveClient(client);
                return Created("Client successfully created", response);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("UpdateClient/{id:int}")]
        public IActionResult UpdateClient([FromRoute]int id, [FromBody]ClientDTO client)
        {
            if (ModelState.IsValid)
            {
                var response = _service.PutClient(id, client);
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("RemoveClient/{id:int}")]
        public IActionResult RemoveClient([FromRoute]int id)
        {
            return Ok(_service.DeleteClient(id));
        }
    }
}