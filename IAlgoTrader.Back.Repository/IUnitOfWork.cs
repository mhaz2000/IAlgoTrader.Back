using IAlgoTrader.Back.Repository.Repositories;

namespace IAlgoTrader.Back.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        IContactUsRepository ContactUsRepository { get; }
        IContactUsFormRepository ContactUsFormRepository { get; }
        ISymbolTransactionRepository SymbolTransactionRepository { get; }
        ISymbolRepository SymbolRepository { get; }
        ITradeRepository TradeRepository { get; }
        IUserRepository UserRepository { get; }


        Task<int> CommitAsync();
        int Commit();
    }
}
