namespace IAlgoTrader.Back.Jobs
{
    public interface IOrderScheduler
    {
        Task ApplyOrders();
    }
}
