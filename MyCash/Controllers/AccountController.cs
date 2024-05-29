using Microsoft.AspNetCore.Mvc;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.ApplicationService.Services;
using MyCash.Domain.Entity;

namespace MyCash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<List<AccountGetAllResponse>> GetAllAccount()
        {
            return await _accountService.GetAllAccount();
        }

        [HttpGet("{id:int}")]
        public async Task<AccountResponse> GetAccountById([FromRoute] int id)
        {
            return await _accountService.GetAccountById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest account)
        {
            await _accountService.CreateAccount(account);
            return Ok();
        }

        [HttpPut("{accountId:int}/")]
        public async Task<ActionResult<AccountResponse>> UpdateAccount([FromRoute] int accountId, [FromBody] AccountRequest account)
        {
            return new OkObjectResult(await _accountService.UpdateAccount(accountId, account));
        }

        [HttpDelete("{accountId:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int accountId)
        {
            await _accountService.DeleteAccount(accountId);
            return Ok();
        }

        [HttpPost("{accountId:int}/transaction/{transactionId:int}")]
        public async Task<IActionResult> AddTransaction([FromRoute] int accountId, [FromRoute] int transactionId)
        {
            await _accountService.AddTransaction(accountId, transactionId);
            return Ok();
        }

        
    }
}
