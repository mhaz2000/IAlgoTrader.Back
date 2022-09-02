using FluentValidation;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Message.Validators.AlgorithmValidators
{
    public class TwapOrderCommandValidator : AbstractValidator<TwapOrderCommand>
    {
        public TwapOrderCommandValidator()
        {
            RuleFor(c => c.OrderType).NotEmpty().NotNull().WithMessage("نوع سفارش نمی تواند خالی باشد.");
            RuleFor(c => c.Price).NotEmpty().NotNull().WithMessage("مبلغ نمی تواند خالی باشد.");
            RuleFor(c => c.TimePeriod).NotEmpty().NotNull().WithMessage("مدت زمان معامله نمی تواند خالی باشد.");
            RuleFor(c => c.SharesNumber).NotEmpty().NotNull().WithMessage("تعداد سهم نمی تواند خالی باشد.");
            RuleFor(c => c.SymbolId).NotEmpty().NotNull().WithMessage("نماد نمی تواند خالی باشد.");
        }
    }
}
