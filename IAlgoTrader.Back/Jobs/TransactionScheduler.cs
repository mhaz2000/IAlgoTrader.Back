using IAlgorTrader.Back.Service;

namespace IAlgoTrader.Back.Jobs
{
    public class TransactionScheduler : ITransactionScheduler
    {
        private readonly IServiceHolder _serviceHolder;

        public TransactionScheduler(IServiceHolder serviceHolder)
        {
            _serviceHolder = serviceHolder;
        }

        public async Task GenerateTransaction()
        {
            await _serviceHolder.SymbolTransactionService.GenerateTransactions();
        }
    }
}
