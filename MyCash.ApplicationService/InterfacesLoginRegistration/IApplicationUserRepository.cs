using MyCash.ApplicationService.DTO.Request;
using MyCash.Domain.Entity;
using static MyCash.ApplicationService.DTO.Response.ServiceResponse;

namespace MyCash.ApplicationService.InterfacesLoginRegistration
{
    public interface IApplicationUserRepository
    {
        Task<List<ApplicationUser>> GetAllUserAccount();
        Task<GeneralResponse> CreateUserAccount(UserDTO userDTO);
        Task<LoginResponse> LoginUserAccount(LoginDTO loginDTO);

        //Task<GeneralResponse> UpdateUserAccount(UserDTO updatedUserDTO, string email);

        //Task<ApplicationUser> FindUserByEmail(string email);
    }
}
