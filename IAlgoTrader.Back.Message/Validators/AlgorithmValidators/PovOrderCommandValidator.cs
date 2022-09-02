using FluentValidation;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Message.Validators.AlgorithmValidators
{
    public class PovOrderCommandValidator : AbstractValidator<PovOrderCommand>
    {
        public PovOrderCommandValidator()
        {
            RuleFor(c => c.Percentage).NotNull().NotEmpty().WithMessage("درصد حجم معاملات نمی تواند خالی باشد.");
            RuleFor(c => c.Volume).NotNull().NotEmpty().WithMessage("حجم معاملات نمی تواند خالی باشد.");
            RuleFor(c => c.SymbolId).NotNull().NotEmpty().WithMessage("نماد نمی تواند خالی باشد.");
            RuleFor(c => c.OrderType).NotNull().NotEmpty().WithMessage("نوع سفارش نمی تواند خالی باشد.");
        }
    }
}
