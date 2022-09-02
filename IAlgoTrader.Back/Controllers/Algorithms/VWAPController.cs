using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class VWAPController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        public VWAPController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> VWAPCalculator(Guid id)
        {
            try
            {
                var result = await _serviceHolder.AlgorithmService.VWAPCalculation(id);
                return OkResult("مقدار میانگین موزون حجم قیمت با موفقیت محاسبه گردید.", result);
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده، با پشتیبانی تماس حاصل فرمایید.");
            }
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order(VwapOrderCommand command) 
        {
            try
            {
                await _serviceHolder.OrderService.VwapOrder(command, UserId);
                return OkResult("سفارش جدید با موفقیت ثبت گردید.");
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده، با پشتیبانی تماس حاصل فرمایید.");
            }
        }

    }
}
