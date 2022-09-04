using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.TransactionCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface ISymbolTransactionRepository : IRepository<Transaction>
    {
        IQueryable<Transaction> GetAllTransactionsAsync();
        Task<ICollection<Transaction>> GetIncludedTransactionBySymbolId(Guid id);
        Task GenerateTransaction(IEnumerable<CreateTransactionCommand> command);
    }
}
