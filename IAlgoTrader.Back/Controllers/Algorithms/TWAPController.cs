using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class TWAPController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;

        public TWAPController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(TWAPOrderCommand command)
        {
            try
            {
                var twap = await _serviceHolder.AlgorithmService.TWAPCalculation(command);
                return OkResult("عملیات با موفقیت انجام شد.", twap, 1);
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده است. با پشتیبانی تماس حاصل فرمایید.");
            }
        }

        [HttpPost("Order")]
        public async Task<IActionResult> RegisterOrder(TwapOrderCommand command)
        {
            try
            {
                await _serviceHolder.OrderService.TwapOrder(command, UserId);
                return OkResult("سفارش جدید با موفقیت ثبت گردید.");
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده است. با پشتیبانی تماس حاصل فرمایید.");
            }
        }
    }
}
