using IAlgoTrader.Back.Message.Commands.UserCommands;
using IAlgoTrader.Back.Message.DTOs.UserDTOs;
using IAlgoTrader.Back.Service.SeedWorks.Models;

namespace IAlgorTrader.Back.Service.Interfaces.UserService
{
    public interface IUserService
    {
        Task CreateUser(CreateUserCommand command);
        Task UpdateUser(UpdateUserCommand command, string userId);
        Task<ICollection<UserDto>> GetUsers(string? search = "");
        Task<UserLoginDto> LoginUser(LoginCommand command, JwtIssuerOptions jwtIssuerOptions);
        Task<UserDto> GetUser(string id);

    }
}
