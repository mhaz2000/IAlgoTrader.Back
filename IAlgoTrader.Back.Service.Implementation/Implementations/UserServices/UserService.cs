using IAlgorTrader.Back.Service.Interfaces.UserService;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.UserCommands;
using IAlgoTrader.Back.Message.DTOs.UserDTOs;
using IAlgoTrader.Back.Repository;
using IAlgoTrader.Back.Service.SeedWorks.Interfaces;
using IAlgoTrader.Back.Service.SeedWorks.Models;
using Microsoft.AspNetCore.Identity;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task CreateUser(CreateUserCommand command)
        {
            var errors = new List<string>();
            if (await _unitOfWork.UserRepository.AnyAsync(c => c.Email.ToLower() == command.Email.ToLower()))
                errors.Add("ایمیل وارد شده، قبلا ثبت شده است.");
            if (await _unitOfWork.UserRepository.AnyAsync(c => c.PhoneNumber.ToLower() == command.Phone.ToLower()))
                errors.Add("شماره موبایل وارد شده، قبلا ثبت شده است.");

            if (errors.Any())
                throw new ManagedException(String.Join("\n", errors));

            await _unitOfWork.UserRepository.CreateAsync(command);
            await _unitOfWork.CommitAsync();

        }

        public async Task<UserDto> GetUser(string id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserAsync(id);
                if (user is null)
                    throw new NotFoundException("کاربر مورد نظر یافت نشد.");

                return new UserDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    LogoId = user.LogoId,
                    Phone = user.PhoneNumber
                };
            }
            catch (Exception)
            {
                throw new ManagedException("در هنگام دریافت اطلاعات کاربران خطایی رخ داده است.");
            }
        }

        public async Task<int> GetUserCount()
        {
            return (await _unitOfWork.UserRepository.GetAllAsync()).Count();
        }

        public async Task<ICollection<UserDto>> GetUsers(string? search = "")
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();
                users = users
                    .Where(c =>
                    c.FirstName.Contains(search ?? "") ||
                    c.LastName.Contains(search ?? "") ||
                    c.PhoneNumber.Contains(search ?? "") ||
                    c.Email.Contains(search ?? ""));

                return users.Select(s => new UserDto()
                {
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Id = s.Id,
                    LogoId = s.LogoId,
                    Phone = s.PhoneNumber
                }).ToList();
            }
            catch (Exception)
            {
                throw new ManagedException("در هنگام دریافت اطلاعات کاربران خطایی رخ داده است.");
            }
        }

        public async Task<UserLoginDto> LoginUser(LoginCommand command, JwtIssuerOptions jwtIssuerOptions)
        {
            try
            {
                var user = await _unitOfWork.UserRepository
                    .FirstOrDefaultAsync(c => c.PhoneNumber == command.Username || c.Email == command.Username);

                if (user is null)
                    throw new ManagedException("نام کاربری یا رمز عبور اشتباه است.");

                if (!await _userManager.CheckPasswordAsync(user, command.Password))
                    throw new ManagedException("نام کاربری یا رمز عبور اشتباه می‌باشد.");

                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _tokenGenerator.TokenGeneration(user, jwtIssuerOptions, _roleManager.Roles.Where(c => userRoles.Any(t => t == c.Name)).ToList());

                return new UserLoginDto()
                {
                    IsAdmin = user.IsAdmin,
                    TokenType = token.TokenType,
                    expires_in = token.expires_in,
                    AuthToken = token.AuthToken,
                    RefreshToken = token.RefreshToken
                };
            }
            catch (ManagedException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw new ManagedException("در هنگام ورود خطایی رخ داده است.");
            }
        }

        public async Task UpdateUser(UpdateUserCommand command, string userId)
        {
            try
            {
                await _unitOfWork.UserRepository.EditAsync(command, userId);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw new ManagedException("در هنگام ویرایش کاربر خطایی رخ داده است.");
            }
        }
    }
}
