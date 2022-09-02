namespace IAlgoTrader.Back.Jobs
{
    public interface ITransactionScheduler
    {
        public Task GenerateTransaction();
    }
}
