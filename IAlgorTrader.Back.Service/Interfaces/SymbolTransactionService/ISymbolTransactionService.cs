using IAlgoTrader.Back.Message.DTOs.SymbolDTOs;
using IAlgoTrader.Back.Message.DTOs.TransactionDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.SymbolTransactionService
{
    public interface ISymbolTransactionService
    {
        Task<ICollection<TransactionDto>> GetLastTransacitons(string? search = "");
        Task<ICollection<SymbolDto>> GetSymbols();
        Task GenerateTransactions();
        Task<IEnumerable<TransactionDto>> GetSymbolTransactions(Guid id);
    }
}
