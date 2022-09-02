using FluentValidation;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Message.Validators.AlgorithmValidators
{
    public class ItaOrderCommandValidator : AbstractValidator<ItaOrderCommand>
    {
        public ItaOrderCommandValidator()
        {
            RuleFor(c => c.SymbolId).NotNull().NotEmpty().WithMessage("نماد نمی تواند خالی باشد.");
        }
    }

    public class ItaSellCommandValidator : AbstractValidator<ItaSellCommand>
    {
        public ItaSellCommandValidator()
        {
            RuleFor(c => c.StartLimit).NotNull().NotEmpty().WithMessage("حد شروع نمی تواند خالی باشد.");
            RuleFor(c => c.StopLimit).NotNull().NotEmpty().WithMessage("حد پایان نمی تواند خالی باشد.");
        }
    }

    public class ItaBuyCommandValidator : AbstractValidator<ItaBuyCommand>
    {
        public ItaBuyCommandValidator()
        {
            RuleFor(c => c.StartLimit).NotNull().NotEmpty().WithMessage("حد شروع نمی تواند خالی باشد.");
            RuleFor(c => c.StopLimit).NotNull().NotEmpty().WithMessage("حد پایان نمی تواند خالی باشد.");
        }
    }
}
