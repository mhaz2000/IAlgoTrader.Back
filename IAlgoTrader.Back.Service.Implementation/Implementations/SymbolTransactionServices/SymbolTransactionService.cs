using IAlgorTrader.Back.Service.Interfaces.SymbolTransactionService;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Message.Commands.TransactionCommands;
using IAlgoTrader.Back.Message.DTOs.SymbolDTOs;
using IAlgoTrader.Back.Message.DTOs.TransactionDTOs;
using IAlgoTrader.Back.Repository;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.SymbolTransactionServices
{
    public class SymbolTransactionService : ISymbolTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SymbolTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateTransactions()
        {
            Random random = new Random();
            var transactions = await _unitOfWork.SymbolTransactionRepository.GetAllTransactionsAsync().ToListAsync();

            var lastDate = transactions.OrderByDescending(c => c.CreatedAt).First();
            if (lastDate.CreatedAt.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) == 0)
                return;

            var newTransactions = transactions.OrderByDescending(t => t.Date).GroupBy(c => c.SymbolId).Select(s => new CreateTransactionCommand()
            {
                SymbolId = s.Key,
                ClosePrice = random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.ClosePrice ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.ClosePrice ?? 0) * ((double)random.Next(0, 6) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.ClosePrice ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.ClosePrice ?? 0) * ((double)random.Next(0, 6) / 100)),
                LastPrice = random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.LastPrice ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.LastPrice ?? 0) * ((double)random.Next(0, 6) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.LastPrice ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.LastPrice ?? 0) * ((double)random.Next(0, 6) / 100)),
                PriceFirst = random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceFirst ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceFirst ?? 0) * ((double)random.Next(0, 6) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceFirst ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceFirst ?? 0) * ((double)random.Next(0, 6) / 100)),
                PriceMax = random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMax ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMax ?? 0) * ((double)random.Next(0, 6) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMax ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMax ?? 0) * ((double)random.Next(0, 6) / 100)),
                PriceMin = random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMin ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMin ?? 0) * ((double)random.Next(0, 6) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMin ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMin ?? 0) * ((double)random.Next(0, 6) / 100)),
                NumberTrade = (int)(random.Next(0, 2) == 0 ?
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.NumberTrade ?? 0) - (s.FirstOrDefault(c => c.NumberTrade != 0)?.NumberTrade ?? 0) * ((double)random.Next(0, 16) / 100)) :
                    Math.Floor((s.FirstOrDefault(c => c.NumberTrade != 0)?.NumberTrade ?? 0) + (s.FirstOrDefault(c => c.NumberTrade != 0)?.NumberTrade ?? 0) * ((double)random.Next(0, 16) / 100))),
                Date = s.FirstOrDefault().Date.AddDays(1)
            }).ToList();

            newTransactions.ForEach(c =>
            {
                var pmax = c.PriceMax;
                var pmin = c.PriceMin;
                c.PriceMax = Math.Max(pmax, pmin);
                c.PriceMin = Math.Min(pmax, pmin);

                pmax = c.PriceMax;
                pmin = c.ClosePrice;
                c.ClosePrice = Math.Min(pmax, pmin);
                c.PriceMax = Math.Max(pmax, pmin);


                if (c.LastPrice > c.PriceMax)
                    c.PriceMax = c.LastPrice;

                if (c.PriceFirst > c.PriceMax)
                    c.PriceMax = c.PriceFirst;
            });

            await _unitOfWork.SymbolTransactionRepository.GenerateTransaction(newTransactions);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ICollection<TransactionDto>> GetLastTransacitons(string? search = "")
        {
            try
            {
                var transactions = await _unitOfWork.SymbolTransactionRepository.GetAllTransactionsAsync().ToListAsync();
                return transactions.OrderByDescending(t => t.Date).GroupBy(c => c.SymbolId).Select(s => new TransactionDto()
                {
                    ClosePrice = s.FirstOrDefault(c => c.NumberTrade != 0)?.ClosePrice ?? 0,
                    Date = s.FirstOrDefault(c => c.NumberTrade != 0) != null ?
                        ConvertDateToString(s.FirstOrDefault(c => c.NumberTrade != 0)?.Date ?? DateTime.Now) : string.Empty,
                    Id = s.FirstOrDefault(c => c.NumberTrade != 0)?.Id ?? Guid.Empty,
                    LastPrice = s.FirstOrDefault(c => c.NumberTrade != 0)?.LastPrice ?? 0,
                    NumberTrade = s.FirstOrDefault(c => c.NumberTrade != 0)?.NumberTrade ?? 0,
                    PriceFirst = s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceFirst ?? 0,
                    PriceMax = s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMax ?? 0,
                    PriceMin = s.FirstOrDefault(c => c.NumberTrade != 0)?.PriceMin ?? 0,
                    SymbolId = s.Key,
                    SymbolName = s.FirstOrDefault(c => c.NumberTrade != 0)?.Symbol?.Name ?? ""
                }).ToList();
            }
            catch (Exception)
            {
                throw new ManagedException("در هنگام دریافت اطلاعات معاملات خطایی رخ داده است.");
            }
        }

        public async Task<ICollection<SymbolDto>> GetSymbols()
        {
            return (await _unitOfWork.SymbolRepository.GetAllAsync()).Select(s => new SymbolDto()
            {
                Id = s.Id,
                SymbolName = s.Name
            }).ToList();
        }

        public async Task<IEnumerable<TransactionDto>> GetSymbolTransactions(Guid id)
        {
            var pc = new PersianCalendar();
            var transactions = await _unitOfWork.SymbolTransactionRepository.GetIncludedTransactionBySymbolId(id);
            return transactions.Select(transaction => new TransactionDto()
            {
                ClosePrice = transaction.ClosePrice,
                Date = $"{pc.GetYear(transaction.Date)}/{pc.GetMonth(transaction.Date)}/{pc.GetDayOfMonth(transaction.Date)}",
                Id = id,
                LastPrice = transaction.LastPrice,
                NumberTrade = transaction.NumberTrade,
                PriceFirst = transaction.PriceFirst,
                PriceMax = transaction.PriceMax,
                PriceMin = transaction.PriceMin,
                SymbolId = transaction.SymbolId,
            });
        }

        private string ConvertDateToString(DateTime date)
        {
            var pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
        }
    }
}
