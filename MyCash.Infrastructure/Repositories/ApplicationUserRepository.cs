using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;
using static MyCash.ApplicationService.DTO.Response.ServiceResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyCash.Infrastructure.Repositories
{
    public class ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IApplicationUserRepository
    {
        

        public async Task<GeneralResponse> CreateUserAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<List<ApplicationUser>> GetAllUserAccount()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<LoginResponse> LoginUserAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<GeneralResponse> UpdateUserAccount(UserDTO updatedUserDTO, string email)
        //{
        //    if (updatedUserDTO is null) return new GeneralResponse(false, "Model is empty");

        //    // Найти пользователя по его электронной почте
        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user is null) return new GeneralResponse(false, "User not found");

        //    // Проверить, соответствует ли пароль
        //    var passwordValid = await userManager.CheckPasswordAsync(user, updatedUserDTO.Password);
        //    if (!passwordValid) return new GeneralResponse(false, "Invalid password");

        //    // Обновить данные пользователя
        //    user.Name = updatedUserDTO.Name;
        //    user.Email = updatedUserDTO.Email;

        //    // Выполнить обновление пользователя
        //    var updateUser = await userManager.UpdateAsync(user);

        //    if (!updateUser.Succeeded) return new GeneralResponse(false, "Error occurred.. please try again");

        //    return new GeneralResponse(true, "Account Updated");
        //}

        //public async Task<ApplicationUser> FindUserByEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");

        //    var user = await userManager.FindByEmailAsync(email);
        //    return user;
        //}
    }
}
