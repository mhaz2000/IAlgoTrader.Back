using IAlgorTrader.Back.Service.Interfaces.TradeService;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Infrastructure.Helpers;
using IAlgoTrader.Back.Message.Commands.TradeCommands;
using IAlgoTrader.Back.Message.DTOs.TradeDTOs;
using IAlgoTrader.Back.Repository;
using System.Globalization;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.TradeServices
{
    public class TradeService : ITradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterTrades()
        {
            var orders = _unitOfWork.OrderRepository.Include(c => c.Symbol).Where(c => c.IsActive);

            var twapOrders = orders.Where(c => c.AlgorithmType == AlgorithmType.TWAP);
            var vwapOrders = orders.Where(c => c.AlgorithmType == AlgorithmType.VWAP);
            var itaOrders = orders.Where(c => c.AlgorithmType == AlgorithmType.ITA);
            var povOrders = orders.Where(c => c.AlgorithmType == AlgorithmType.POV);

            await RegisterVwapTrades(vwapOrders);
            await RegisterTwapTraders(twapOrders);
            await RegisterPovTrades(povOrders);
            await RegisterItaTraders(itaOrders);

            await _unitOfWork.CommitAsync();
        }

        private async Task RegisterItaTraders(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                if ((await _unitOfWork.TradeRepository.GetAllAsync())
                    .Any(c => c.OrderId == order.Id && c.Date.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) == 0))
                    continue;

                var previousTrades = await _unitOfWork.TradeRepository.FindAsync(c => c.OrderId == order.Id);
                var transaction = _unitOfWork.SymbolTransactionRepository.OrderByDescending(c => c.Date)
                    .FirstOrDefault(c => c.NumberTrade != 0 && c.SymbolId == order.Symbol.Id);

                var totalVolume = order.OrderType == OrderType.Sell ? previousTrades.Sum(c => c.SellVolume) : previousTrades.Sum(c => c.BuyVolume);

                if (order.OrderVolume <= totalVolume)
                {
                    order.IsActive = false;
                    order.IsCompleted = true;
                    continue;
                }

                if (transaction.ClosePrice > order.OrderStartLimit && transaction.ClosePrice < order.OrderStopLimit)
                {
                    await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                    {
                        Date = DateTime.Now,
                        OrderId = order.Id,
                        OrderType = order.OrderType,
                        Price = transaction.ClosePrice,
                        Volume = order.OrderDailyVolume.Value
                    }, order.CreatedById);
                }
            }
        }

        private async Task RegisterTwapTraders(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                if ((await _unitOfWork.TradeRepository.GetAllAsync())
                    .Any(c => c.OrderId == order.Id && c.Date.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) == 0))
                    continue;

                var previousTrades = await _unitOfWork.TradeRepository.FindAsync(c => c.OrderId == order.Id);
                var totalVolume = order.OrderType == OrderType.Sell ? previousTrades.Sum(c => c.SellVolume) : previousTrades.Sum(c => c.BuyVolume);
                await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                {
                    Date = DateTime.Now,
                    OrderId = order.Id,
                    OrderType = order.OrderType,
                    Price = order.OrderAmount.Value,
                    Volume = Math.Min(Math.Floor(order.OrderVolume.Value / order.OrderLength.Value), (order.OrderVolume.Value - totalVolume))
                }, order.CreatedById);

                if (Math.Floor(order.OrderVolume.Value / order.OrderLength.Value) >= (order.OrderVolume.Value - totalVolume))
                {
                    order.IsActive = false;
                    order.IsCompleted = true;
                }
            }
        }

        private async Task RegisterPovTrades(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                if ((await _unitOfWork.TradeRepository.GetAllAsync())
                    .Any(c => c.OrderId == order.Id && c.Date.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) == 0))
                    continue;

                var previousTrades = await _unitOfWork.TradeRepository.FindAsync(c => c.OrderId == order.Id);
                var totalVolume = order.OrderType == OrderType.Sell ? previousTrades.Sum(c => c.SellVolume) : previousTrades.Sum(c => c.BuyVolume);

                var transaction = _unitOfWork.SymbolTransactionRepository.OrderByDescending(c => c.Date)
                    .FirstOrDefault(c => c.NumberTrade != 0 && c.SymbolId == order.Symbol.Id);

                await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                {
                    Date = DateTime.Now,
                    OrderId = order.Id,
                    OrderType = order.OrderType,
                    Price = order.OrderAmount.Value,
                    Volume = Math.Min(Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100)),
                        Math.Abs(totalVolume - order.OrderVolume.Value))
                }, order.CreatedById);

                if (Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100)) <= Math.Abs(totalVolume - order.OrderVolume.Value))
                {
                    order.IsActive = false;
                    order.IsCompleted = true;
                }
            }

        }

        private async Task RegisterVwapTrades(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                if ((await _unitOfWork.TradeRepository.GetAllAsync())
                    .Any(c => c.OrderId == order.Id && c.Date.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) == 0))
                    continue;

                var transactions = await _unitOfWork.SymbolTransactionRepository.FindAsync(c => c.SymbolId == order.Symbol.Id);
                var orderedTransactions = transactions.Where(t => t.NumberTrade != 0).OrderByDescending(c => c.Date).Take(20);
                var lastClosePrice = orderedTransactions.FirstOrDefault().ClosePrice;
                var vwapAmount = VWAPCalculating(orderedTransactions.Reverse());

                var previousTrades = await _unitOfWork.TradeRepository.FindAsync(c => c.OrderId == order.Id);
                if (previousTrades is null || !previousTrades.Any())
                {
                    await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                    {
                        Date = DateTime.Now,
                        Price = vwapAmount,
                        OrderId = order.Id,
                        OrderType = vwapAmount >= lastClosePrice ? OrderType.Sell : OrderType.Buy,
                        Volume = Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100))
                    }, order.CreatedById);
                }
                else
                {
                    var totalSellVolume = previousTrades.Sum(c => c.SellVolume);
                    var totalBuyVolume = previousTrades.Sum(c => c.BuyVolume);

                    if (vwapAmount >= lastClosePrice)
                    {
                        if (Math.Abs(totalSellVolume - totalBuyVolume) <= order.OrderVolume)
                            await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                            {
                                Date = DateTime.Now,
                                Price = vwapAmount,
                                OrderType = OrderType.Sell,
                                OrderId = order.Id,
                                Volume = Math.Min(Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100)), Math.Abs(totalSellVolume - totalBuyVolume))
                            }, order.CreatedById);
                    }
                    else
                    {
                        if (Math.Abs(totalSellVolume - totalBuyVolume) <= order.OrderVolume)
                            await _unitOfWork.TradeRepository.CreateTrade(new CreateTradeCommand()
                            {
                                Date = DateTime.Now,
                                Price = vwapAmount,
                                OrderType = OrderType.Buy,
                                OrderId = order.Id,
                                Volume = Math.Min(Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100)),
                                    order.OrderVolume.Value - Math.Abs(totalSellVolume - totalBuyVolume))
                            }, order.CreatedById);
                    }

                    if (order.OrderVolume.Value - Math.Abs(totalSellVolume - totalBuyVolume) <= Math.Floor(order.OrderVolume.Value * (order.VolumePercentage.Value / 100)))
                    {
                        order.IsActive = false;
                        order.IsCompleted = true;
                    }
                }

            }
        }

        public async Task<IEnumerable<TradeDto>> GetTrades(Guid userId)
        {
            var pc = new PersianCalendar();
            var trades = await _unitOfWork.TradeRepository.GetAllAsync();
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            return trades.Where(c => c.CreatedById == userId).Select(s => new TradeDto()
            {
                BuyVolume = s.BuyVolume,
                Date = $"{pc.GetYear(s.Date)}/{pc.GetMonth(s.Date)}/{pc.GetDayOfMonth(s.Date)}",
                Id = s.Id,
                OrderType = s.OrderType.GetDescription(),
                Price = s.Price,
                SellVolume = s.SellVolume,
                AlgorithmType = orders.FirstOrDefault(c => c.Id == s.OrderId).AlgorithmType.GetDescription(),
                OrderNumber = orders.FirstOrDefault(c => c.Id == s.OrderId).Number
            }).OrderByDescending(s => s.Date);
        }

        public async Task<TradesInfoDto> GetTradesInfo()
        {
            var allTrades = await _unitOfWork.TradeRepository.GetAllAsync();
            var tradesCount = allTrades.Count();
            var tradesVolumes = allTrades.Sum(s => s.SellVolume + s.BuyVolume);
            var tradesPrice = allTrades.Sum(s => s.Price);

            return new TradesInfoDto
            {
                TradesCount = tradesCount,
                TradesVolumes = tradesVolumes,
                TradesPrice = tradesPrice
            };
        }

        private double VWAPCalculating(IEnumerable<Transaction> transactions)
        {
            var tradeNumbers = 0;
            var cumulativeTotal = 0.0;
            var finalResult = 0.0;

            foreach (var transaction in transactions)
            {
                tradeNumbers += transaction.NumberTrade;
                var averagePrice = (transaction.PriceMax + transaction.PriceMin + transaction.ClosePrice) / 3;
                var currentCumulativeTotal = averagePrice * transaction.NumberTrade;
                cumulativeTotal += currentCumulativeTotal;
                finalResult = cumulativeTotal / tradeNumbers;
            }

            return finalResult;
        }

    }
}