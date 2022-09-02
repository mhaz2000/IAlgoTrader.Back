using IAlgorTrader.Back.Service.Interfaces.AlgorithmService;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using IAlgoTrader.Back.Message.DTOs.AlgorithmDTOs;
using IAlgoTrader.Back.Repository;
using System.Globalization;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.AlgorithmServices
{
    public class AlgorithmService : IAlgorithmService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AlgorithmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TradeNumberDto> GetTradeNumbers(Guid id)
        {
            var pc = new PersianCalendar();

            var transactions = await _unitOfWork.SymbolTransactionRepository.FindAsync(c => c.SymbolId == id);
            var orderedTransactions = transactions.Where(t => t.NumberTrade != 0).OrderByDescending(c => c.Date).Take(30);
            return new TradeNumberDto()
            {
                TradeNumbers = orderedTransactions.Select(s => new TradeNumberDetailDto()
                {
                    Date = $"{pc.GetYear(s.Date)}/{pc.GetMonth(s.Date)}/{pc.GetDayOfMonth(s.Date)}",
                    TradeNumber = s.NumberTrade
                })
            };
        }

        public async Task<TWAPDto> TWAPCalculation(TWAPOrderCommand command)
        {
            var pc = new PersianCalendar();

            var transactions = await _unitOfWork.SymbolTransactionRepository.FindAsync(c => c.SymbolId == command.SymbolId);
            var selectedTransactions = transactions.OrderByDescending(c => c.Date).Take(command.TransactionDays)
                .Select(s => new TWAPAverageDto()
                {
                    Amount = (s.PriceFirst + s.PriceMax + s.PriceMin + s.LastPrice) / 4,
                    Date = $"{pc.GetYear(s.Date)}/{pc.GetMonth(s.Date)}/{pc.GetDayOfMonth(s.Date)}"
                });

            return new TWAPDto()
            {
                Averages = selectedTransactions.ToList(),
                TwapAmount = selectedTransactions.Sum(c => c.Amount) / command.TransactionDays
            };
        }

        public async Task<VWAPDto> VWAPCalculation(Guid id)
        {
            var transactions = await _unitOfWork.SymbolTransactionRepository.FindAsync(c => c.SymbolId == id);
            var orderedTransactions = transactions.Where(t => t.NumberTrade != 0).OrderByDescending(c => c.Date).Take(20);
            return VWAPCalculating(orderedTransactions.Reverse());
        }

        private VWAPDto VWAPCalculating(IEnumerable<Transaction> transactions)
        {
            var pc = new PersianCalendar();
            VWAPDto vWAPDto = new VWAPDto();
            vWAPDto.VWAPs = new List<VWAPDetailDto>();

            var tradeNumbers = 0;
            var cumulativeTotal = 0.0;

            foreach (var transaction in transactions)
            {
                tradeNumbers += transaction.NumberTrade;
                var averagePrice = (transaction.PriceMax + transaction.PriceMin + transaction.ClosePrice) / 3;
                var currentCumulativeTotal = averagePrice * transaction.NumberTrade;
                cumulativeTotal += currentCumulativeTotal;
                vWAPDto.VWAPs.Add(new VWAPDetailDto()
                {
                    Amount = cumulativeTotal / tradeNumbers,
                    Date = $"{pc.GetYear(transaction.Date)}/{pc.GetMonth(transaction.Date)}/{pc.GetDayOfMonth(transaction.Date)}",
                    ClosePrice = transaction.ClosePrice,
                });
            }

            return vWAPDto;
        }
    }
}
