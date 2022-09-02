using IAlgoTrader.Back.Infrastructure.Enums;
using IAlgoTrader.Back.Message.Validators.AlgorithmValidators;

namespace IAlgoTrader.Back.Message.Commands.AlgorithmCommands
{
    public class PovOrderCommand : ICommandBase
    {
        public Guid SymbolId { get; set; }
        public double Volume { get; set; }
        public int Percentage { get; set; }
        public OrderType OrderType { get; set; }
        public void Validate() => new PovOrderCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}
