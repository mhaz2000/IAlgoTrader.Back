using IAlgorTrader.Back.SeedWorks;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Base;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.SeedWorks;
using Microsoft.AspNetCore.Mvc;

namespace IAlgoTrader.Back.Controllers.SymbolTransaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolTransactionController : ApiControllerBase
    {
        private readonly IServiceHolder _serviceHolder;
        public SymbolTransactionController(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageQuery pageQuery, string? search = "")
        {
            try
            {
                var transactions = await _serviceHolder.SymbolTransactionService.GetLastTransacitons(search);
                return OkResult("اطلاعات آخرین معاملات نماد ها", transactions.ToPagingAndSorting(pageQuery), transactions.Count());
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }


        [HttpGet("GetSymbols")]
        public async Task<IActionResult> GetSymbols()
        {
            try
            {
                var symbols = await _serviceHolder.SymbolTransactionService.GetSymbols();
                return OkResult("اطلاعات نماد ها", symbols, symbols.Count());
            }
            catch (ManagedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new SystemException("متاسفانه خطای سیستمی رخ داده");
            }
        }
    }
}
