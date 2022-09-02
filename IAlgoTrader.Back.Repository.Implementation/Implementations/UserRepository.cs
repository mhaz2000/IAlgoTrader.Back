using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.UserCommands;
using IAlgoTrader.Back.Repository.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public async Task CreateAsync(CreateUserCommand command)
        {
            var _passwordHasher = new PasswordHasher<User>();

            var user = new User()
            {
                IsAdmin = false,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.Phone,
                SecurityStamp = Guid.NewGuid().ToString(),
                LogoId = command.LogoId
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);
            await Context.Users.AddAsync(user);

            var role = Context.Roles.FirstOrDefault(c => c.Name == "User");
            await Context.UserRoles.AddAsync(new IdentityUserRole<string>() { RoleId = role.Id, UserId = user.Id });
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await Context.Users.FindAsync(id);

            Context.Users.Remove(user);
        }

        public async Task EditAsync(UpdateUserCommand command, string userId)
        {
            try
            {
                var _passwordHasher = new PasswordHasher<User>();

                var user = await Context.Users.FindAsync(userId);
                user.PhoneNumber = command.Phone;
                user.FirstName = command.FirstName ?? "";
                user.LastName = command.LastName ?? "";
                user.Email = command.Email;
                user.LogoId = command.LogoId;

                if (!string.IsNullOrEmpty(command.Password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User?> GetUserAsync(string id)
        {
            return await Context.Users.FindAsync(id) ?? null;
        }
    }
}
