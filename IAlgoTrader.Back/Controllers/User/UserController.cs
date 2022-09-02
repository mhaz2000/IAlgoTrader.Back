using FluentValidation;
using IAlgorTrader.Back.SeedWorks;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.UserCommands;
using IAlgoTrader.Back.SeedWorks;
using IAlgoTrader.Back.SeedWorks.Models;
using IAlgoTrader.Back.Service.SeedWorks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        public UserController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            _jwtIssuerOptions = config.Get<AppSettingsModel>().JwtIssuerOptions;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            try
            {
                command.Validate();

                await _serviceHolder.UserService.CreateUser(command);
                return OkResult("ثبت نام شما با موفقیت انجام شد.");
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            try
            {
                await _serviceHolder.UserService.UpdateUser(command, UserId.ToString());
                return OkResult("اطلاعات شما با موفقیت ویرایش گردید.");
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            try
            {
                var user = await _serviceHolder.UserService.LoginUser(command, _jwtIssuerOptions);
                return OkResult("شما با موفقیت وارد شدید.", user);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [HttpGet("UserDetail")]
        public async Task<IActionResult> GetUserDetail()
        {

            try
            {
                var user = await _serviceHolder.UserService.GetUser(UserId.ToString());
                return OkResult("اطلاعات کاربر با موفقیت یافت شد.", user, 1);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PageQuery pageQuery, string? search = "")
        {
            try
            {
                var users = await _serviceHolder.UserService.GetUsers(search);
                return OkResult("اطلاعات کاربران با موفقیت یافت شد.", users.ToPagingAndSorting(pageQuery), users.Count());
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> CheckIfLogin()
        {
            return OkResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public async Task<IActionResult> CheckIfAdmin()
        {
            return OkResult();
        }
    }
}
