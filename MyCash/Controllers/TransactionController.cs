using Microsoft.AspNetCore.Mvc;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<List<TransactionGetAllResponse>> GetAllTransactions()
        {
            return await _transactionService.GetAllTransactions();
        }

        [HttpGet("{transactionId:int}")]
        public async Task<TransactionResponse> GetTransactionById([FromRoute] int transactionId)
        {
            return await _transactionService.GetTransactionById(transactionId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest transaction)
        {
            await _transactionService.CreateTransaction(transaction);
            return Ok();
        }

        [HttpPut("{transactionId:int}/")]
        public async Task<ActionResult<TransactionResponse>> UpdateTransaction([FromRoute] int transactionId, [FromBody] TransactionRequest transaction)
        {
            return new OkObjectResult(await _transactionService.UpdateTransaction(transactionId, transaction));
        }

        [HttpDelete("{transactionID:int}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int transactionID)
        {
            await _transactionService.DeleteTransaction(transactionID);
            return Ok();
        }


        //[HttpGet("summary/{orderName}")]
        //public async Task<ActionResult<OrderCountDto>> GetOrderSummary(string orderName)
        //{
        //    var orderSummary = await _orderService.GetOrderSummary(orderName);

        //    return Ok(orderSummary);
        //}
    }
}
