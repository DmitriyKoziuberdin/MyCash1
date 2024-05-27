using Microsoft.AspNetCore.Identity;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.InterfacesLoginRegistration;
using MyCash.Domain.Entity;
using static MyCash.ApplicationService.DTO.Response.ServiceResponse;

namespace MyCash.ApplicationService.ServicesLoginRegistration
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<GeneralResponse> Register(UserDTO userDTO)
        {
            var response = await _applicationUserRepository.CreateUserAccount(userDTO);
            return response;
        }

        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            var response = await _applicationUserRepository.LoginUserAccount(loginDTO);
            return response;
        }

        public async Task<List<ApplicationUser>> GetAllUserAccount()
        {
            return await _applicationUserRepository.GetAllUserAccount();
        }

        //public async Task<GeneralResponse> UpdateUserAccount(UserDTO updatedUserDTO, string email)
        //{
        //    return await _applicationUserRepository.UpdateUserAccount(updatedUserDTO, email);
        //}

        //public async Task<ApplicationUser> FindUserByEmail(string email)
        //{
        //    return await _applicationUserRepository.FindUserByEmail(email);
        //}
    }
}
