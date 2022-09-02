using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.TransactionCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface ISymbolTransactionRepository : IRepository<Transaction>
    {
        IQueryable<Transaction> GetAllTransactionsAsync();
        Task GenerateTransaction(IEnumerable<CreateTransactionCommand> command);
    }
}
