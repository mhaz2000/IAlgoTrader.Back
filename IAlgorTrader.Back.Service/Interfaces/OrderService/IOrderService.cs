using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using IAlgoTrader.Back.Message.DTOs.OrderDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.OrderService
{
    public interface IOrderService
    {
        Task VwapOrder(VwapOrderCommand command, Guid userId);
        Task TwapOrder(TwapOrderCommand command, Guid userId);
        Task PovOrder(PovOrderCommand command, Guid userId);
        Task ItaOrder(ItaOrderCommand command, Guid userId);
        Task<IEnumerable<OrderDto>> GetOrders(Guid userId);
    }
}
