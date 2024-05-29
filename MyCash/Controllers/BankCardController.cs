using Microsoft.AspNetCore.Mvc;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankCardController : ControllerBase
    {
        private readonly IBankCardService _bankCardService;

        public BankCardController(IBankCardService bankCardService)
        {
            _bankCardService = bankCardService;
        }

        [HttpGet]
        public async Task<List<BankCard>> GetAllAccount()
        {
            return await _bankCardService.GetAllBankCards();
        }

        [HttpGet("{id:int}")]
        public async Task<CardResponse> GetCardById([FromRoute] int id)
        {
            return await _bankCardService.GetCardById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankCard([FromBody] CardRequest card)
        {
            await _bankCardService.CreateCard(card);
            return Ok();
        }

        [HttpPut("{cardId:int}/")]
        public async Task<ActionResult<CardResponse>> UpdateCard([FromRoute] int cardId, [FromBody] CardRequest card)
        {
            return new OkObjectResult(await _bankCardService.UpdateCard(cardId, card));
        }

        [HttpDelete("{cardId:int}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int cardId)
        {
            await _bankCardService.DeleteCard(cardId);
            return Ok();
        }

        [HttpPost("{accountId:int}/card/{cardId:int}")]
        public async Task<IActionResult> AddCardForAccount([FromRoute] int accountId, [FromRoute] int cardId)
        {
            await _bankCardService.AddCardForAccount(accountId, cardId);
            return Ok();
        }
    }
}
