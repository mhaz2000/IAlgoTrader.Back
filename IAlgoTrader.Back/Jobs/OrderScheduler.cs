using IAlgorTrader.Back.Service;

namespace IAlgoTrader.Back.Jobs
{
    public class OrderScheduler : IOrderScheduler
    {
        private readonly IServiceHolder _serviceHolder;

        public OrderScheduler(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        public async Task ApplyOrders()
        {
            await _serviceHolder.TradeService.RegisterTrades();
        }
    }
}
