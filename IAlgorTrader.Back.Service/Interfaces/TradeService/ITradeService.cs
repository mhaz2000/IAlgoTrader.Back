using IAlgoTrader.Back.Message.DTOs.TradeDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.TradeService
{
    public interface ITradeService
    {
        Task RegisterTrades();

        Task<IEnumerable<TradeDto>> GetTrades(Guid userId);
    }
}
