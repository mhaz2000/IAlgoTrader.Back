using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.ContactUs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        public ContactUsController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contactUs = await _serviceHolder.ContactUsService.GetContactUs();
                return OkResult("اطلاعات تماس با ما با موفقیت یافت شد.", contactUs, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactUsCommand command)
        {
            try
            {
                await _serviceHolder.ContactUsService.UpdateContactUs(command);
                return OkResult("اطلاعات تماس با ما با موفقیت ویرایش گردید.");
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }
    }
}
