using IAlgoTrader.Back.Message.Validators.AlgorithmValidators;

namespace IAlgoTrader.Back.Message.Commands.AlgorithmCommands
{
    public class ItaOrderCommand : ICommandBase
    {
        public Guid SymbolId { get; set; }
        public ItaBuyCommand? BuyCommand { get; set; }
        public ItaSellCommand? SellCommand { get; set; }

        public void Validate() => new ItaOrderCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }

    public class ItaSellCommand 
    {
        public double? StartLimit { get; set; }
        public double? StopLimit { get; set; }
        public double? DailyShares { get; set; }
        public double? MaximumShares { get; set; }

        public void Validate() => new ItaSellCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }

    public class ItaBuyCommand : ICommandBase
    {
        public double? StartLimit { get; set; }
        public double? StopLimit { get; set; }
        public double? DailyShares { get; set; }
        public double? MaximumShares { get; set; }

        public void Validate() => new ItaBuyCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}