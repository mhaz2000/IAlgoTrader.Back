using IAlgorTrader.Back.SeedWorks;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.SeedWorks;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;

        public OrderController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageQuery pageQuery)
        {
            try
            {
                var orders = await _serviceHolder.OrderService.GetOrders(UserId);
                return OkResult("اطلاعات سفارشات با موفقیت یافت شد.", orders.ToPagingAndSorting(pageQuery), orders.Count());
            }
            catch(ManagedException ex)
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
