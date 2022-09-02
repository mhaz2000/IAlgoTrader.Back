using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Message.Commands.TradeCommands;
using IAlgoTrader.Back.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        public TradeRepository(DataContext context) : base(context)
        {

        }
        public async Task CreateTrade(CreateTradeCommand command, Guid userId)
        {
            await Context.Trades.AddAsync(new Trade()
            {
                Date = command.Date,
                Price = command.Price,
                SellVolume = command.OrderType == OrderType.Sell ? command.Volume : 0,
                BuyVolume = command.OrderType == OrderType.Sell ? 0 : command.Volume,
                OrderType = command.OrderType,
                OrderId = command.OrderId,
                CreatedById = userId
            });
        }
    }
}
