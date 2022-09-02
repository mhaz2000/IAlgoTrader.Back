using FluentValidation;
using IAlgoTrader.Back.Message.Commands.AlgorithmCommands;

namespace IAlgoTrader.Back.Message.Validators.AlgorithmValidators
{
    public class TWAPCommandValidator : AbstractValidator<TWAPOrderCommand>
    {
        public TWAPCommandValidator()
        {
            RuleFor(c => c.SymbolId).NotNull().NotEmpty().WithMessage("شناسه نماد نمی تواند خالی باشد.");
            RuleFor(c => c.TransactionDays).NotNull().NotEmpty().WithMessage("تعداد روز های معامله نمی تواند خالی باشد.");
        }
    }
}
