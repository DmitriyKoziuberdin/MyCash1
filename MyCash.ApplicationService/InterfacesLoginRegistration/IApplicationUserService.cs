using MyCash.ApplicationService.DTO.Request;
using MyCash.Domain.Entity;
using static MyCash.ApplicationService.DTO.Response.ServiceResponse;

namespace MyCash.ApplicationService.InterfacesLoginRegistration
{
    public interface IApplicationUserService
    {
        Task<GeneralResponse> Register(UserDTO userDTO);
        Task<LoginResponse> Login(LoginDTO loginDTO);

        Task<List<ApplicationUser>> GetAllUserAccount();

        //Task<GeneralResponse> UpdateUserAccount(UserDTO updatedUserDTO, string email);


        //Task<ApplicationUser> FindUserByEmail(string email);
    }
}
