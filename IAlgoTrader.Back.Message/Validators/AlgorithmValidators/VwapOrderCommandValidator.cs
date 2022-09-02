using FluentValidation;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Message.Validators.AlgorithmValidators
{
    public class VwapOrderCommandValidator : AbstractValidator<VwapOrderCommand>
    {
        public VwapOrderCommandValidator()
        {
            RuleFor(c => c.OrderType).NotEmpty().NotNull().WithMessage("نوع سفارش نمی تواند خالی باشد.");
            RuleFor(c => c.Percentage).NotEmpty().NotNull().WithMessage("درصد حجم سهم نمی تواند خالی باشد.");
            RuleFor(c => c.Volume).NotEmpty().NotNull().WithMessage("حجم سهم نمی تواند خالی باشد.");
            RuleFor(c => c.SymbolId).NotEmpty().NotNull().WithMessage("نماد نمی تواند خالی باشد.");
        }
    }
}
