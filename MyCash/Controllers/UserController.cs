using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUser()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id:int}")]
        public async Task<UserResponse> GetUserById([FromRoute] int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }

        [HttpPut("{userId:int}/")]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromRoute] int userId, [FromBody] UserRequest user)
        {
            return new OkObjectResult(await _userService.UpdateUser(userId, user));
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }

        //[HttpPost("{clientId:int}/order/{orderId:int}")]
        //public async Task<IActionResult> CreateClient([FromRoute] int clientId, [FromRoute] int orderId)
        //{
        //    await _clientService.AddOrder(clientId, orderId);
        //    return Ok();
        //}

        //[HttpGet("{clientId:int}/totalOrderPrice")]
        //public async Task<ClientDto> GetTotalOrderPrice(int clientId)
        //{
        //    return await _clientService.GetTotalOrderPrice(clientId);
        //}
    }
}
