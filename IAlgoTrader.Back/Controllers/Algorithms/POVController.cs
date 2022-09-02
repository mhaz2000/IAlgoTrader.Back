using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class POVController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;

        public POVController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTradeNumbers(Guid id)
        {
            try
            {
                var tradeNumbers = await _serviceHolder.AlgorithmService.GetTradeNumbers(id);
                return OkResult("اطلاعات حجم معاملات با موفقیت یافت شد.", tradeNumbers, 1);
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException(e.Message);
            }
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order(PovOrderCommand command)
        {

            try
            {
                await _serviceHolder.OrderService.PovOrder(command, UserId);
                return OkResult("سفارش جدید با موفقیت ثبت گردید.");
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException(e.Message);
            }
        }
    }
}
