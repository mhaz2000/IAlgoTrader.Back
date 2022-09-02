using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;
using IAlgoTrader.Back.Message.DTOs.AlgorithmDTOs;

namespace IAlgorTrader.Back.Service.Interfaces.AlgorithmService
{
    public interface IAlgorithmService
    {
        Task<VWAPDto> VWAPCalculation(Guid id);
        Task<TWAPDto> TWAPCalculation(TWAPOrderCommand command);
        Task<TradeNumberDto> GetTradeNumbers(Guid id);
    }
}
