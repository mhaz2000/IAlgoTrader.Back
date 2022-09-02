using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.TradeCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface ITradeRepository : IRepository<Trade>
    {
        Task CreateTrade(CreateTradeCommand command, Guid userId);
    }
}
