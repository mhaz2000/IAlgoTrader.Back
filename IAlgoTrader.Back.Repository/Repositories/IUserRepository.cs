using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.UserCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserAsync(string id);
        Task CreateAsync(CreateUserCommand command);
        Task EditAsync(UpdateUserCommand command, string userId);
        Task DeleteAsync(Guid Id);
    }
}
