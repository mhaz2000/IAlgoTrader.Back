using IAlgorTrader.Back.SeedWorks;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.SeedWorks;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Trade
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        public TradeController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrades([FromQuery] PageQuery pageQuery)
        {
            try
            {
                var trades = await _serviceHolder.TradeService.GetTrades(UserId);
                return OkResult("اطلاعات سفارشات با موفقیت یافت شد.", trades.ToPagingAndSorting(pageQuery), trades.Count());
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SystemException(e.Message);
            }
        }
    }
}
