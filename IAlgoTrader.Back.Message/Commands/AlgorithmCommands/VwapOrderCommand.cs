using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Message.Validators.AlgorithmValidators;

namespace IAlgoTrader.Back.Message.Commands.AlgorithmCommands
{
    public class VwapOrderCommand : ICommandBase
    {
        public Guid SymbolId { get; set; }
        public OrderType OrderType { get; set; }
        public double Volume { get; set; }
        public int Percentage { get; set; }

        public void Validate() => new VwapOrderCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}
