using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Repository.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task CreateOrderFromTWAPAsync(TwapOrderCommand command, Guid userId);
        Task CreateOrderFromVWAPAsync(VwapOrderCommand command, Guid userId);
        Task CreateOrderFromPOVAsync(PovOrderCommand command, Guid userId);
        Task CreateOrderFromITAAsync(ItaOrderCommand command, Guid userId);
        Task<ICollection<Order>> GetWithIncludeAsync();
    }
}
