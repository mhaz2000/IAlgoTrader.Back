using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using IAlgoTrader.Back.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAlgoTrader.Back.Repository.Implementation.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context) { }

        public async Task CreateOrderFromITAAsync(ItaOrderCommand command, Guid userId)
        {
            var symbol = await Context.Symbols.FindAsync(command.SymbolId);

            if (command.SellCommand is not null)
                await Context.Orders.AddAsync(new()
                {
                    Symbol = symbol,
                    OrderDailyVolume = command.SellCommand.DailyShares,
                    OrderVolume = command.SellCommand.MaximumShares,
                    OrderType = OrderType.Sell,
                    OrderStopLimit = command.SellCommand.StopLimit,
                    OrderStartLimit = command.SellCommand.StartLimit,
                    AlgorithmType = AlgorithmType.ITA,
                    Number = GetLastNumber() + 1,
                    CreatedById = userId
                });

            if (command.BuyCommand is not null)
                await Context.Orders.AddAsync(new()
                {
                    Symbol = symbol,
                    OrderDailyVolume = command.BuyCommand.DailyShares,
                    OrderVolume = command.BuyCommand.MaximumShares,
                    OrderType = OrderType.Buy,
                    OrderStopLimit = command.BuyCommand.StopLimit,
                    OrderStartLimit = command.BuyCommand.StartLimit,
                    AlgorithmType = AlgorithmType.ITA,
                    Number = GetLastNumber() + 1,
                    CreatedById = userId
                });
        }

        public async Task CreateOrderFromPOVAsync(PovOrderCommand command, Guid userId)
        {
            var symbol = await Context.Symbols.FindAsync(command.SymbolId);
            await Context.Orders.AddAsync(new Order()
            {
                OrderType = command.OrderType,
                OrderVolume = command.Volume,
                Symbol = symbol,
                VolumePercentage = command.Percentage,
                AlgorithmType = AlgorithmType.POV,
                Number = GetLastNumber() + 1,
                CreatedById = userId
            });
        }

        public async Task CreateOrderFromTWAPAsync(TwapOrderCommand command, Guid userId)
        {
            var symbol = await Context.Symbols.FindAsync(command.SymbolId);
            await Context.Orders.AddAsync(new Order()
            {
                OrderAmount = command.Price,
                OrderLength = command.TimePeriod,
                OrderType = command.OrderType,
                OrderVolume = command.SharesNumber,
                Symbol = symbol,
                AlgorithmType = AlgorithmType.TWAP,
                Number = GetLastNumber() + 1,
                CreatedById = userId
            });
        }

        public async Task CreateOrderFromVWAPAsync(VwapOrderCommand command, Guid userId)
        {
            var symbol = await Context.Symbols.FindAsync(command.SymbolId);
            await Context.Orders.AddAsync(new()
            {
                OrderType = command.OrderType,
                Symbol = symbol,
                OrderVolume = command.Volume,
                VolumePercentage = command.Percentage,
                AlgorithmType = AlgorithmType.VWAP,
                Number = GetLastNumber() + 1,
                CreatedById = userId
            });
        }

        public async Task<ICollection<Order>> GetWithIncludeAsync()
        {
            return await Context.Orders.Include(c => c.Symbol).ToListAsync();
        }

        private int GetLastNumber()
        {
            return Context.Orders.OrderByDescending(c => c.Number).FirstOrDefault()?.Number ?? 0;
        }
    }
}
