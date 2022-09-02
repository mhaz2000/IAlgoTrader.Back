using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Algorithms
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITAController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;

        public ITAController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order(ItaOrderCommand command)
        {
            try
            {
                await _serviceHolder.OrderService.ItaOrder(command, UserId);
                return OkResult("سفارش جدید با موفقیت ثبت گردید.");
            }
            catch (ManagedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException("متاستفانه خطای سیستمی رخ داده است.");
            }
        }
    }
}
