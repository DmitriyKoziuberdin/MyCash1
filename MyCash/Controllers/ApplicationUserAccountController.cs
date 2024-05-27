using Microsoft.AspNetCore.Mvc;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.InterfacesLoginRegistration;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;
using static MyCash.ApplicationService.DTO.Response.ServiceResponse;

namespace MyCash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserAccountController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;

        public ApplicationUserAccountController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await _applicationUserService.Register(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _applicationUserService.Login(loginDTO);
            return Ok(response);
        }

        [HttpGet]
        public async Task<List<ApplicationUser>> GetAllApllicationUser()
        {
            return await _applicationUserService.GetAllUserAccount();
        }


        //[HttpPut]
        //public async Task<GeneralResponse> UpdateUserAccount(UserDTO updatedUserDTO, string email)
        //{
        //    return await _applicationUserService.UpdateUserAccount(updatedUserDTO, email);
        //}

        //[HttpGet("email")]
        //public async Task<ApplicationUser> FindUserByEmail(string email)
        //{
        //    return await _applicationUserService.FindUserByEmail(email);
        //}
    }
}
