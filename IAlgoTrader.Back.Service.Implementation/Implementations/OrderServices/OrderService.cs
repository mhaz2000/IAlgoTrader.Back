using IAlgorTrader.Back.Service.Interfaces.OrderService;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Infrastructure.Base;
using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Infrastructure.Helpers;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using IAlgoTrader.Back.Message.DTOs.OrderDTOs;
using IAlgoTrader.Back.Repository;
using System.Globalization;

namespace IAlgoTrader.Back.Service.Implementation.Implementations.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(Guid userId)
        {
            var pc = new PersianCalendar();
            var orders = await _unitOfWork.OrderRepository.GetWithIncludeAsync();
            return orders.Where(c => c.CreatedById == userId).Select(s => new OrderDto()
            {
                Id = s.Id,
                IsActive = s.IsActive,
                OrderType = s.OrderType.GetDescription(),
                Date = $"{pc.GetYear(s.RegisteredDate)}/{pc.GetMonth(s.RegisteredDate)}/{pc.GetDayOfMonth(s.RegisteredDate)}",
                AlgorithmType = s.AlgorithmType.GetDescription(),
                SymbolName = s.Symbol.Name,
                OrderNumber = s.Number,
                IsCompleted = s.IsCompleted
            });
        }

        public async Task ItaOrder(ItaOrderCommand command, Guid userId)
        {
            CheckItaConflicts(command);
            await _unitOfWork.OrderRepository.CreateOrderFromITAAsync(command, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task PovOrder(PovOrderCommand command, Guid userId)
        {
            await _unitOfWork.OrderRepository.CreateOrderFromPOVAsync(command, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task TwapOrder(TwapOrderCommand command, Guid userId)
        {
            await _unitOfWork.OrderRepository.CreateOrderFromTWAPAsync(command, userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task VwapOrder(VwapOrderCommand command, Guid userId)
        {
            await _unitOfWork.OrderRepository.CreateOrderFromVWAPAsync(command, userId);
            await _unitOfWork.CommitAsync();
        }

        private async void CheckItaConflicts(ItaOrderCommand command)
        {
            if (command.SellCommand is not null && command.BuyCommand is not null)
            {
                if (command.SellCommand.StartLimit > command.BuyCommand.StartLimit && command.SellCommand.StartLimit < command.BuyCommand.StopLimit ||
                    command.SellCommand.StopLimit > command.BuyCommand.StartLimit && command.SellCommand.StopLimit < command.BuyCommand.StopLimit)
                    throw new ManagedException("حدود خرید و فروش با هم تداخل دارند.");
            }

            var orders = await _unitOfWork.OrderRepository.FindAsync(c => c.AlgorithmType == AlgorithmType.ITA && c.Symbol.Id == command.SymbolId);

            if (command.SellCommand is not null)
            {
                if (orders.Any(c => command.SellCommand.StartLimit > c.OrderStartLimit && command.SellCommand.StartLimit < c.OrderStopLimit ||
                    command.SellCommand.StopLimit > c.OrderStartLimit && command.SellCommand.StopLimit < c.OrderStopLimit))
                    throw new ManagedException("حدود خرید و فروش با هم تداخل دارند.");
            }

            if (command.BuyCommand is not null)
            {
                if (orders.Any(c => command.BuyCommand.StartLimit > c.OrderStartLimit && command.BuyCommand.StartLimit < c.OrderStopLimit ||
                    command.BuyCommand.StopLimit > c.OrderStartLimit && command.BuyCommand.StopLimit < c.OrderStopLimit))
                    throw new ManagedException("حدود خرید و فروش با هم تداخل دارند.");
            }
        }
    }
}
