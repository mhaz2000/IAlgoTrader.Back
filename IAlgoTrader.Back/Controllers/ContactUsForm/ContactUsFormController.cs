using IAlgorTrader.Back.SeedWorks;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using IAlgoTrader.Back.SeedWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.ContactUsForm
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsFormController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        public ContactUsFormController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageQuery pageQuery, string? search = "")
        {
            try
            {
                var contactUsForm = await _serviceHolder.ContactUsFormService.GetForms(search);
                return OkResult("اطلاعات تماس با ما یافت شد.", contactUsForm.ToPagingAndSorting(pageQuery), contactUsForm.Count());
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
        [HttpPost]
        public async Task<IActionResult> Create(AddCotactUsFormCommand command)
        {
            try
            {
                command.Validate();

                await _serviceHolder.ContactUsFormService.Create(command);
                return OkResult("پیغام شما با موفقیت ارسال گردید..");
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
    }
}
