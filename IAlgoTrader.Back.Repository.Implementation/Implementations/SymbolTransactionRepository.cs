using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.TransactionCommands;
using IAlgoTrader.Back.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class SymbolTransactionRepository : Repository<Transaction>, ISymbolTransactionRepository
    {
        public SymbolTransactionRepository(DataContext context) : base(context)
        {
        }

        public async Task GenerateTransaction(IEnumerable<CreateTransactionCommand> command)
        {
            await Context.Transactions.AddRangeAsync(command.Select(s => new Transaction()
            {
                ClosePrice = s.ClosePrice,
                Date = s.Date,
                LastPrice = s.LastPrice,
                NumberTrade = s.NumberTrade,
                PriceFirst = s.PriceFirst,
                PriceMax = s.PriceMax,
                PriceMin = s.PriceMin,
                SymbolId = s.SymbolId,
                Symbol = Context.Symbols.Find(s.SymbolId)
            }));
        }

        public IQueryable<Transaction> GetAllTransactionsAsync()
        {
            return Context.Transactions.Include(c => c.Symbol);
        }

        public async Task<ICollection<Transaction>> GetIncludedTransactionBySymbolId(Guid id)
        {
            return await Context.Transactions.Include(c => c.Symbol).Where(c => c.SymbolId == id).ToListAsync();
        }
    }
}
