using FluentValidation;
using IAlgoTrader.Back.Message.Commands.ContactUsCommands;

namespace IAlgoTrader.Back.Message.Validators.ContactUsValidators
{
    public class AddCotactUsFormCommandValidator : AbstractValidator<AddCotactUsFormCommand>
    {
        public AddCotactUsFormCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().NotNull().WithMessage("ایمیل نمی‌تواند خالی باشد.");
            RuleFor(c => c.Title).NotEmpty().NotNull().WithMessage("عنوان نمی‌تواند خالی باشد.");
            RuleFor(c => c.Description).NotEmpty().NotNull().WithMessage("توضیحات نمی‌تواند خالی باشد.");
        }
    }
}
