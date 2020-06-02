using System.Collections.Generic;
using System.Threading.Tasks;

using BankApplication.Data.DTOs;
using BankApplication.Service.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace BankApplication.WebApi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _service;

        public AccountsController(IAccountsRepository service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<AccountDTO> GetAccounts()
        {
            return _service.GetAccounts();
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> GetAccount([FromRoute]int id)
        {
            return Ok(await _service.GetAccount(id));
        }

        [HttpPost]
        [Route("NewAccount")]
        public IActionResult NewAccount([FromBody]AccountDTO account)
        {
            if (ModelState.IsValid)
            {
                var response = _service.SaveAccount(account);
                return Created("Account successfully created", response);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("UpdateAccount/{id:int}")]
        public IActionResult UpdateClient([FromRoute]int id, [FromBody]AccountDTO client)
        {
            if (ModelState.IsValid)
            {
                var response = _service.PutAccount(id, client);
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("RemoveAccount/{id:int}")]
        public IActionResult RemoveAccount([FromRoute]int id)
        {
            return Ok(_service.DeleteAccount(id));
        }

    }
}