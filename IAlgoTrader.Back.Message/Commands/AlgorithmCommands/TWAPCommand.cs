
using IAlgoTrader.Back.Message.Validators.AlgorithmValidators;

namespace IAlgoTrader.Back.Message.Commands.AlgorithmCommands
{
    public class TWAPOrderCommand : ICommandBase
    {
        public Guid SymbolId { get; set; }
        public int TransactionDays { get; set; }
        public void Validate() => new TWAPCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}
